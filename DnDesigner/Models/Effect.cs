using Microsoft.EntityFrameworkCore;
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

        public void RemoveEffect()
        {
            Effect.RemoveEffect(Character);
            Character.CharacterEffects.Remove(this);
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
            return $"Increase {Attribute} by {Value}";
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
}
