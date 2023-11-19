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
        public Character Character { get; set; }

        public bool IsApplied { get; set; }

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
                Effect.ApplyEffect(Character);
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
    }

    /// <summary>
    /// A choice between multiple effects
    /// </summary>
    public class EffectChoice : Effect
    {
        public int ChosenIndex { get; set; }
        public List<Effect> Effects { get; private set; }

        public EffectChoice(List<Effect> effects)
        {
            Effects = effects;
        }

        public EffectChoice(List<Effect> effects, int chosenIndex)
        {
            Effects = effects;
            ChosenIndex = chosenIndex;
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
            RemoveEffect(character);
            if (ChosenIndex < Effects.Count && ChosenIndex >= 0)
            {
                CharacterEffect characterEffect = new CharacterEffect(character, Effects[ChosenIndex]);
                character.CharacterEffects.Add(characterEffect);
                characterEffect.ApplyEffect();
            }
        }

        public override void RemoveEffect(Character character)
        {
            foreach (Effect effect in Effects)
            {
                CharacterEffect? existingEffect = character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                if (existingEffect != null)
                {
                    existingEffect.RemoveEffect();
                    character.CharacterEffects.Remove(existingEffect);
                }
            }
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
    }
}
