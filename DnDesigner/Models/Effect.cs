using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDesigner.Models
{
    /// <summary>
    /// An abstract super class for classes that modify a character
    /// </summary>
    public abstract class Effect
    {
        [Key]
        public int EffectId { get; set; }

        /// <summary>
        /// Apply this effect to the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public abstract void ApplyEffect(Character character);

        /// <summary>
        /// Remove this effect from the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public abstract void RemoveEffect(Character character);

        public abstract override string ToString();
    }

    /// <summary>
    /// An effect applied to a character
    /// </summary>
    public class CharacterEffect
    {
        [Key]
        public int CharacterEffectId { get; set; }

        [ForeignKey("EffectId")]
        public Effect Effect { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Character Character { get; set; }

        public CharacterEffect(Character character, Effect effect)
        {
            Effect = effect;
            Character = character;
            Effect.ApplyEffect(Character);
        }

        private CharacterEffect() { }

        /// <summary>
        /// Remove this effect from the character
        /// </summary>
        public void RemoveEffect()
        {
            Character.CharacterEffects.Remove(this);
            Effect.RemoveEffect(Character);
        }
    }

    /// <summary>
    /// When applied, this effect will apply multiple effects as a group
    /// This is useful for applying multiple effects from a single choice
    /// </summary>
    public class GroupedEffect : Effect
    {
        public List<Effect> Effects { get; set; }

        public GroupedEffect(List<Effect> effects)
        {
            Effects = effects;
        }

        private GroupedEffect() { }

        public override void ApplyEffect(Character character)
        {
            foreach (Effect effect in Effects)
            {
                effect.ApplyEffect(character);
            }
        }

        public override void RemoveEffect(Character character)
        {
            foreach (Effect effect in Effects)
            {
                effect.RemoveEffect(character);
            }
        }

        public override string ToString()
        {
            string str = "This applies the following: ";
            foreach (Effect effect in Effects)
            {
                str += effect.ToString() + ", ";
            }
            return str;
        }
    }

    /// <summary>
    /// When applied, this effect will modify a character's attribute
    /// </summary>
    public class ModifyAttribute : Effect
    {
        public string Attribute { get; set; }
        public int Value { get; set; }
 
        public ModifyAttribute(string attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }

        private ModifyAttribute() { }

        public override void ApplyEffect(Character character)
        {
            character.ModifyAttribute(Attribute, Value);
        }

        public override void RemoveEffect(Character character)
        {
            character.ModifyAttribute(Attribute, -Value);
        }

        public override string ToString()
        {
            if (Value < 0)
            {
                return $"Decrease {Attribute} by {Value}";
            }
            else
            {
                return $"Increase {Attribute} by {Value}";
            }
        }
    }

    /// <summary>
    /// When applied, this effect will grant a character specific proficiencies
    /// </summary>
    public class GrantProficiencies : Effect
    {
        public List<Proficiency> Proficiencies { get; set; }
        public bool Expertise { get; set; }

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

        private GrantProficiencies() { }

        public override void ApplyEffect(Character character)
        {
            foreach (Proficiency proficiency in Proficiencies)
            {
                character.GrantProficiency(proficiency, Expertise);
            }
        }

        public override void RemoveEffect(Character character)
        {
            foreach (Proficiency proficiency in Proficiencies)
            {
                character.RemoveProficiency(proficiency);
            }
        }

        public override string ToString()
        {
            string str = "Grant ";
            if (Expertise)
            {
                str += "expertise in ";
            }
            else
            {
                str += "proficiency in ";
            }
            foreach (Proficiency proficiency in Proficiencies)
            {
                str += proficiency.Name + ", ";
            }
            return str.Substring(0, str.Length - 2);
        }
    }

    /// <summary>
    /// When applied, this effect will add an action to a character
    /// </summary>
    public class GrantAction : Effect
    {
        public Action Action { get; set; }

        public GrantAction(Action action)
        {
            Action = action;
        }

        private GrantAction(){}

        public override void ApplyEffect(Character character)
        {
            character.Actions.Add(new CharacterAction(character, Action));
        }

        public override void RemoveEffect(Character character)
        {
            CharacterAction? characterAction = character.GetAction(Action.Name);
            if (characterAction != null)
            {
                character.Actions.Remove(characterAction);
            }
        }

        public override string ToString()
        {
            return $"Grant action: {Action.Name}";
        }
    }

    /// <summary>
    /// When applied, this effect will grant a character a resource
    /// </summary>
    public class GrantResource : Effect
    {
        /// <summary>
        /// The resource to grant
        /// </summary>
        public Resource Resource { get; set; }

        /// <summary>
        /// The maximum amount of the resource to grant, if null, the resource's formula will be used instead
        /// </summary>
        public int? Max { get; set; }

        public GrantResource(Resource resource, int? max = null)
        {
            Resource = resource;
            Max = max;
        }

        private GrantResource() { }

        public override void ApplyEffect(Character character)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEffect(Character character)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Grant Resource: {Resource.Name}";
        }
    }

    /// <summary>
    /// When applied, this effect will set a character's armor class
    /// </summary>
    public class SetArmorClass : Effect
    {
        /// <summary>
        /// The formula to calculate the character's armor class
        /// </summary>
        public string ArmorClassFormula { get; set; }

        /// <summary>
        /// If true, this effect will be applied regardless of the character's current armor class
        /// If false, this effect will only be applied if the character's current armor class is lower than the new armor class
        /// </summary>
        public bool Override { get; set; }

        /// <summary>
        /// If true, this effect will only be applied if the character is not wearing armor
        /// </summary>
        public bool NoArmor { get; set; }

        /// <summary>
        /// If true, this effect will only be applied if the character is not using a shield
        /// </summary>
        public bool NoShield { get; set; }

        public SetArmorClass(string armorClassFormula, bool @override, bool noArmor, bool noShield)
        {
            ArmorClassFormula = armorClassFormula;
            Override = @override;
            NoArmor = noArmor;
            NoShield = noShield;
        }

        public SetArmorClass(int armorClass, bool @override, bool noArmor, bool noShield) 
            : this(armorClass.ToString(), @override, noArmor, noShield) { }

        public SetArmorClass(string armorClassFormula) 
            : this(armorClassFormula, false, false, false) { }

        public SetArmorClass(int armorClass) 
            : this(armorClass.ToString(), false, false, false) { }


        public override void ApplyEffect(Character character)
        {
            bool valid = true;
            if (NoArmor && character.Inventory.Armor != null)
            {
                valid = false;
            }
            if (NoShield && character.Inventory.OffHand != null)
            {
                Item offHand = character.Inventory.OffHand.Item;
                if (offHand.Traits.Contains("Shield"))
                {
                    valid = false;
                }
            }
            if (valid)
            {
                int AC = 0;
                if (!int.TryParse(ArmorClassFormula, out AC))
                {
                    AC = character.Calculate(ArmorClassFormula);
                }
                if (Override || AC > character.BaseArmorClass)
                {
                    character.BaseArmorClass = AC;
                }
            }
        }

        public override void RemoveEffect(Character character)
        {
            character.SetBaseArmorClass();
        }

        public override string ToString()
        {
            string ret = $"Set base armor class to {ArmorClassFormula}";
            if (!Override)
            {
                ret += " if higher than current base armor class";
            }
            return ret;
        }
    }

    /// <summary>
    /// When applied, this effect will modify a character's bonus armor class
    /// </summary>
    public class ModifyArmorClass : Effect
    {
        /// <summary>
        /// The amount to modify the character's armor class by
        /// </summary>
        public int Value { get; set; }

        public ModifyArmorClass(int value)
        {
            Value = value;
        }

        public override void ApplyEffect(Character character)
        {
            character.BonusArmorClass += Value;
        }

        public override void RemoveEffect(Character character)
        {
            character.BonusArmorClass -= Value;
        }

        public override string ToString()
        {
            if (Value < 0)
            {
                return $"Decrease armor class by {Value}";
            }
            else
            {
                return $"Increase armor class by {Value}";
            }
        }
    }
}
