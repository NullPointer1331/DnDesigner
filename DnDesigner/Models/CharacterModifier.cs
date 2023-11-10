using System.Text.Json.Serialization;

namespace DnDesigner.Models
{
    /// <summary>
    /// An abstract super class for classes that modify a character
    /// </summary>
    [JsonDerivedType(typeof(CharacterModifierChoice), typeDiscriminator: "Choice")]
    [JsonDerivedType(typeof(ModifyAttribute), typeDiscriminator: "Attribute")]
    [JsonDerivedType(typeof(GrantProficiencies), typeDiscriminator: "Proficiencies")]
    public abstract class CharacterModifier
    {
        /// <summary>
        /// Has this modifier been applied?
        /// </summary>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Apply this modifier to the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public abstract void Apply(Character character);

        /// <summary>
        /// Remove this modifier from the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public abstract void Remove(Character character);
    }

    /// <summary>
    /// A choice between multiple modifiers
    /// </summary>
    public class CharacterModifierChoice : CharacterModifier
    {
        public int ChosenIndex { get; set; }
        public List<CharacterModifier> Modifiers { get; private set; }

        public CharacterModifierChoice(List<CharacterModifier> modifiers)
        {
            Modifiers = modifiers;
        }

        [JsonConstructor]
        public CharacterModifierChoice(List<CharacterModifier> modifiers, int chosenIndex)
        {
            Modifiers = modifiers;
            ChosenIndex = chosenIndex;
        }

        /// <summary>
        /// Creates a new CharacterModifierChoice from a preset
        /// </summary>
        /// <param name="preset">The choice preset, 
        /// Options: "ASI" - Increase an ability score by 1, (TODO, implement more presets)</param>
        public CharacterModifierChoice(string preset)
        {
            Modifiers = new List<CharacterModifier>();
            switch (preset)
            {
                case "ASI":
                    Modifiers.Add(new ModifyAttribute("str", 1));
                    Modifiers.Add(new ModifyAttribute("dex", 1));
                    Modifiers.Add(new ModifyAttribute("con", 1));
                    Modifiers.Add(new ModifyAttribute("int", 1));
                    Modifiers.Add(new ModifyAttribute("wis", 1));
                    Modifiers.Add(new ModifyAttribute("cha", 1));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates a new CharacterModifierChoice to choose from a list of proficiencies
        /// </summary>
        /// <param name="proficiencies"></param>
        public CharacterModifierChoice(List<Proficiency> proficiencies)
        {
            Modifiers = new List<CharacterModifier>();
            foreach (Proficiency proficiency in proficiencies)
            {
                Modifiers.Add(new GrantProficiencies(proficiency, false));
            }
        } 

        public override void Apply(Character character)
        {
            if(!IsApplied && Modifiers.ElementAt(ChosenIndex) != null)
            {
                foreach (CharacterModifier modifier in Modifiers)
                {
                    modifier.Remove(character);
                }
                Modifiers[ChosenIndex].Apply(character);
                IsApplied = true;
            }
        }

        public override void Remove(Character character)
        {
            if (IsApplied && Modifiers.ElementAt(ChosenIndex) != null)
            {
                foreach (CharacterModifier modifier in Modifiers)
                {
                    modifier.Remove(character);
                }
                IsApplied = false;
            }
        }
    }

    /// <summary>
    /// When applied, this modifier will modify a character's attribute
    /// </summary>
    public class ModifyAttribute : CharacterModifier
    {
        public string Attribute { get; set; }
        public int Value { get; set; }

        [JsonConstructor]
        public ModifyAttribute(string attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }

        public override void Apply(Character character)
        {
            if (!IsApplied)
            {
                character.ModifyAttribute(Attribute, Value);
                IsApplied = true;
            }
        }

        public override void Remove(Character character)
        {
            if (IsApplied)
            {
                character.ModifyAttribute(Attribute, -Value);
                IsApplied = false;
            }
        }
    }

    /// <summary>
    /// When applied, this modifier will grant a character specific proficiencies
    /// </summary>
    public class GrantProficiencies : CharacterModifier
    {
        public List<Proficiency> Proficiencies { get; set; }
        public bool Expertise { get; set; }

        [JsonConstructor]
        public GrantProficiencies(List<Proficiency> proficiencies, bool expertise)
        {
            Proficiencies = proficiencies;
            Expertise = expertise;
        }

        public GrantProficiencies(Proficiency proficiency, bool expertise)
        {
            Proficiencies = new List<Proficiency>
            {
                proficiency
            };
            Expertise = expertise;
        }

        public override void Apply(Character character)
        {
            if (!IsApplied)
            {
                foreach (Proficiency proficiency in Proficiencies)
                {
                    character.GrantProficiency(proficiency, Expertise);
                }
                IsApplied = true;
            }
        }

        public override void Remove(Character character)
        {
            if (IsApplied)
            {
                foreach (Proficiency proficiency in Proficiencies)
                {
                    character.RemoveProficiency(proficiency);
                }
                IsApplied = false;
            }
        }
    }
}
