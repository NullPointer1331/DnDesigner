using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDesigner.Models
{
    [PrimaryKey("CharacterId", "EffectId")]
    public class CharacterEffect
    {
        [ForeignKey("EffectId")]
        public Effect Effect { get; set; }

        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Character Character { get; set; }

        /// <summary>
        /// Has this effect been applied to the character?
        /// </summary>
        public bool IsApplied { get; set; }

        /// <summary>
        /// An int to store any optional values to pass into the effect
        /// </summary>
        public int? Value { get; set; }

        public CharacterEffect(Character character, Effect effect)
        {
            Effect = effect;
            Character = character;
            IsApplied = false;
        }
        private CharacterEffect() { }

        public void ApplyEffect()
        {
            if (!IsApplied)
            {
                if (Value != null && Effect is EffectChoice effectChoice)
                {
                    effectChoice.ApplyEffect(Character, (int)Value);
                }
                else
                {
                    Effect.ApplyEffect(Character);
                }
                IsApplied = true;
            }
        }
        public void RemoveEffect()
        {
            if (IsApplied)
            {
                Effect.RemoveEffect(Character);
                IsApplied = false;
            }
        }
    }
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
    /// A choice between multiple effects
    /// </summary>
    public class EffectChoice : Effect
    {
        public List<Effect> Effects { get; private set; }

        public EffectChoice(List<Effect> effects)
        {
            Effects = effects;
        }

        private EffectChoice() { }

        /// <summary>
        /// Creates a new EffectChoice from a preset
        /// </summary>
        /// <param name="preset">The choice preset, 
        /// Options: "ASI" - Increase an ability score by 1, (TODO, implement more presets)</param>
        public EffectChoice(string preset)
        {
            Effects = new List<Effect>();
            switch (preset)
            {
                case "ASI":
                    Effects.Add(new ModifyAttribute("str", 1));
                    Effects.Add(new ModifyAttribute("dex", 1));
                    Effects.Add(new ModifyAttribute("con", 1));
                    Effects.Add(new ModifyAttribute("int", 1));
                    Effects.Add(new ModifyAttribute("wis", 1));
                    Effects.Add(new ModifyAttribute("cha", 1));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates a new EffectChoice to choose from a list of proficiencies
        /// </summary>
        /// <param name="proficiencies"></param>
        public EffectChoice(List<Proficiency> proficiencies)
        {
            Effects = new List<Effect>();
            foreach (Proficiency proficiency in proficiencies)
            {
                Effects.Add(new GrantProficiencies(proficiency, false));
            }
        } 

        public override void ApplyEffect(Character character)
        {
            ApplyEffect(character, 0);
        }

        public void ApplyEffect(Character character, int chosenIndex)
        {
            RemoveEffect(character);
            if (chosenIndex < Effects.Count && chosenIndex >= 0)
            {
                CharacterEffect characterEffect = new CharacterEffect(character, Effects[chosenIndex]);
                character.CharacterEffects.Add(characterEffect);
                characterEffect.ApplyEffect();
            }
        }

        public override void RemoveEffect(Character character)
        {
            List<CharacterEffect> toRemove = new List<CharacterEffect>();
            foreach (Effect effect in Effects)
            {
                CharacterEffect? existingEffect = character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                if (existingEffect != null)
                {
                    existingEffect.RemoveEffect();
                    toRemove.Add(existingEffect);
                }
            }
            foreach (CharacterEffect characterEffect in toRemove)
            {
                character.CharacterEffects.Remove(characterEffect);
            }
        }

        public override string ToString()
        {
            string str = "Choose one of the following: ";
            foreach (Effect effect in Effects)
            {
                str += effect.ToString() + ", ";
            }
            return str.Substring(0, str.Length - 2);
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
