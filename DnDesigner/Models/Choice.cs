using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    public class CharacterChoice
    {
        [Key]
        public int CharacterChoiceId { get; set; }
        public CharacterFeature CharacterFeature { get; set; }

        public Choice Choice { get; set; }

        public int ChoiceValue { get; set; }

        public CharacterChoice(CharacterFeature characterFeature, Choice choice)
        {
            CharacterFeature = characterFeature;
            Choice = choice;
            ChoiceValue = Choice.DefaultChoice;
        }

        private CharacterChoice() { }

        public void ApplyChoice()
        {
            Choice.ApplyChoice(CharacterFeature.Character, ChoiceValue);
        }
        public void RemoveChoice()
        {
            Choice.RemoveChoice(CharacterFeature.Character);
        }
    }

    public abstract class Choice
    {
        [Key]
        public int ChoiceId { get; set; }
        public int DefaultChoice { get; set; }
        public abstract void ApplyChoice(Character character);
        public abstract void ApplyChoice(Character character, int choice);
        public abstract void RemoveChoice(Character character);
        public abstract void RemoveChoice(Character character, int choice);
    }

    /// <summary>
    /// A choice between multiple effects
    /// </summary>
    public class EffectChoice : Choice
    {
        public List<Effect> Options { get; private set; }

        public EffectChoice(List<Effect> effects)
        {
            Options = effects;
            DefaultChoice = Options[0].EffectId;
        }

        private EffectChoice() { }

        /// <summary>
        /// Creates a new EffectChoice from a preset
        /// </summary>
        /// <param name="preset">The choice preset, 
        /// Options: "ASI" - Increase an ability score by 1, (TODO, implement more presets)</param>
        public EffectChoice(string preset)
        {
            Options = new List<Effect>();
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
            DefaultChoice = Options[0].EffectId;
        }

        /// <summary>
        /// Creates a new EffectChoice to choose from a list of proficiencies
        /// </summary>
        /// <param name="proficiencies"></param>
        public EffectChoice(List<Proficiency> proficiencies)
        {
            Options = new List<Effect>();
            foreach (Proficiency proficiency in proficiencies)
            {
                Options.Add(new GrantProficiencies(proficiency, false));
            }
            DefaultChoice = Options[0].EffectId;
        }

        public override void ApplyChoice(Character character)
        {
            ApplyChoice(character, DefaultChoice);
        }

        /// <summary>
        /// Apply the chosen effect to the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        /// <param name="choiceValue">The index of the chosen effect</param>
        public override void ApplyChoice(Character character, int choiceValue)
        {
            RemoveChoice(character);
            Effect? effect = Options.Find(e => e.EffectId == choiceValue);
            if (effect != null)
            {
                CharacterEffect characterEffect = new CharacterEffect(character, effect);
                character.CharacterEffects.Add(characterEffect);
                characterEffect.ApplyEffect();
            }
        }

        public override void RemoveChoice(Character character)
        {
            List<CharacterEffect> toRemove = new List<CharacterEffect>();
            foreach (Effect effect in Options)
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

        public override void RemoveChoice(Character character, int choiceValue)
        {
            CharacterEffect? existingEffect = character.CharacterEffects.Find(e => e.Effect.EffectId == Options[choiceValue].EffectId);
            if (existingEffect != null)
            {
                existingEffect.RemoveEffect();
                character.CharacterEffects.Remove(existingEffect);
            }
        }

        public override string ToString()
        {
            string str = "Choose one of the following: ";
            foreach (Effect effect in Options)
            {
                str += effect.ToString() + ", ";
            }
            return str.Substring(0, str.Length - 2);
        }
    }
}
