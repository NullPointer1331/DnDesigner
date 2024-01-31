using System.ComponentModel.DataAnnotations;

namespace DnDesigner.Models
{
    public class CharacterChoice
    {
        [Key]
        public int CharacterChoiceId { get; set; }

        /// <summary>
        /// The character feature that this choice is for
        /// </summary>
        public CharacterFeature CharacterFeature { get; set; }

        /// <summary>
        /// The choice that this references
        /// </summary>
        public Choice Choice { get; set; }

        /// <summary>
        /// What the player chose
        /// </summary>
        public int ChoiceValue { get; set; }

        public CharacterChoice(CharacterFeature characterFeature, Choice choice)
        {
            CharacterFeature = characterFeature;
            Choice = choice;
            ChoiceValue = Choice.DefaultChoice;
        }

        private CharacterChoice() { }

        /// <summary>
        /// Applies the choice to the character
        /// </summary>
        public void ApplyChoice()
        {
            Choice.ApplyChoice(CharacterFeature.Character, ChoiceValue);
        }

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        public void RemoveChoice()
        {
            Choice.RemoveChoice(CharacterFeature.Character);
        }

        public override string ToString()
        {
            return Choice.GetOptionDescription(ChoiceValue);
        }
    }

    public abstract class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        /// <summary>
        /// The default choice value
        /// </summary>
        public int DefaultChoice { get; set; }

        /// <summary>
        /// Applies the default choice to the character
        /// </summary>
        /// <param name="character"></param>
        public abstract void ApplyChoice(Character character);

        /// <summary>
        /// Applies the choice to the character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="choice"></param>
        public abstract void ApplyChoice(Character character, int choice);

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        /// <param name="character"></param>
        public abstract void RemoveChoice(Character character);

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="choice"></param>
        public abstract void RemoveChoice(Character character, int choice);

        /// <summary>
        /// Gets the description of the specified option
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public abstract string GetOptionDescription(int choice);
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

        public override string GetOptionDescription(int effectId)
        {
            Effect? effect = Options.Find(e => e.EffectId == effectId);
            if (effect != null)
            {
                return effect.ToString();
            }
            else
            {
                return "Effect not found";
            }
        }
    }
}
