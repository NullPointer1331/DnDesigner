namespace DnDesigner.Models
{
    /// <summary>
    /// An interface for classes that modify a character
    /// </summary>
    public interface ICharacterModifier
    {
        /// <summary>
        /// Has this modifier been applied?
        /// </summary>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Apply this modifier to the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public void Apply(Character character);

        /// <summary>
        /// Remove this modifier from the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public void Remove(Character character);
    }

    /// <summary>
    /// A choice between multiple modifiers
    /// </summary>
    public class CharacterModifierChoice : ICharacterModifier
    {
        public bool IsApplied { get; set; }
        public int ChosenIndex { get; set; }
        public List<ICharacterModifier> Options { get; private set; }

        public CharacterModifierChoice(List<ICharacterModifier> options)
        {
            Options = options;
        }

        /// <summary>
        /// Creates a new CharacterModifierChoice from a preset
        /// </summary>
        /// <param name="preset">The choice preset, 
        /// can be "ASI", (TODO, implement more presets)</param>
        public CharacterModifierChoice(string preset)
        {
            Options = new List<ICharacterModifier>();
            switch (preset)
            {
                case "ASI":
                    Options.Add(new ModifyAttribute("str", 1));
                    Options.Add(new ModifyAttribute("dex", 1));
                    Options.Add(new ModifyAttribute("con", 1));
                    Options.Add(new ModifyAttribute("int", 1));
                    Options.Add(new ModifyAttribute("wis", 1));
                    Options.Add(new ModifyAttribute("cha", 1));
                    break;
                default:
                    break;
            }
        }

        public void Apply(Character character)
        {
            if(!IsApplied && Options.ElementAt(ChosenIndex) != null)
            {
                Options[ChosenIndex].Apply(character);
                IsApplied = true;
            }
        }

        public void Remove(Character character)
        {
            if (IsApplied && Options.ElementAt(ChosenIndex) != null)
            {
                Options[ChosenIndex].Remove(character);
                IsApplied = false;
            }
        }
    }

    /// <summary>
    /// When applied, this modifier will modify a character's attribute
    /// </summary>
    public class ModifyAttribute : ICharacterModifier
    {
        public bool IsApplied { get; set; }
        private string Attribute { get; set; }
        private int Value { get; set; }

        public ModifyAttribute(string attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }

        public void Apply(Character character)
        {
            if (!IsApplied)
            {
                character.ModifyAttribute(Attribute, Value);
                IsApplied = true;
            }
        }

        public void Remove(Character character)
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
    public class GrantProficiencies : ICharacterModifier
    {
        public bool IsApplied { get; set; }
        private List<Proficiency> Proficiencies { get; set; }
        private bool Expertise { get; set; }

        public GrantProficiencies(List<Proficiency> proficiencies, bool expertise)
        {
            Proficiencies = proficiencies;
            Expertise = expertise;
        }

        public void Apply(Character character)
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

        public void Remove(Character character)
        {
            if (IsApplied)
            {
                foreach (Proficiency proficiency in Proficiencies)
                {
                    character.RemoveProficiency(character.GetProficiency(proficiency.Name));
                }
                IsApplied = false;
            }
        }
    }
}
