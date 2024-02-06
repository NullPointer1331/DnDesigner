using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            Choice.ApplyChoice(CharacterFeature.Character, ChoiceValue, CharacterChoiceId);
        }

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        public void RemoveChoice()
        {
            Choice.RemoveChoice(CharacterFeature.Character, CharacterChoiceId);
        }

        public override string ToString()
        {
            return Choice.Options[ChoiceValue];
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
        /// A dictionary of the options for the choice
        /// </summary>
        public abstract Dictionary<int, string> Options { get; }

        /// <summary>
        /// Applies the choice to the character
        /// </summary>
        /// <param name="character">The character to apply the choice to</param>
        /// <param name="choice">The option selected</param>
        /// <param name="characterChoiceId">The Id of the CharacterChoice calling this</param>
        public abstract void ApplyChoice(Character character, int choice, int characterChoiceId);

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="characterChoiceId">The Id of the CharacterChoice calling this</param>
        public abstract void RemoveChoice(Character character, int characterChoiceId);
    }

    /// <summary>
    /// A choice between multiple effects
    /// </summary>
    public class EffectChoice : Choice
    {
        public List<Effect> Effects { get; private set; }

        public override Dictionary<int, string> Options
        {
            get
            {
                Dictionary<int, string> options = new Dictionary<int, string>();
                foreach (Effect effect in Effects)
                {
                    options.Add(effect.EffectId, effect.ToString());
                }
                return options;
            }
        }

        public EffectChoice(List<Effect> effects)
        {
            Effects = effects;
            DefaultChoice = Effects[0].EffectId;
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
            DefaultChoice = Effects[0].EffectId;
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
            DefaultChoice = Effects[0].EffectId;
        }

        /// <summary>
        /// Apply the chosen effect to the character
        /// </summary>
        /// <param name="choiceValue">The index of the chosen effect</param>
        public override void ApplyChoice(Character character, int choiceValue, int characterChoiceId)
        {
            RemoveChoice(character, characterChoiceId);
            Effect? effect = Effects.Find(e => e.EffectId == choiceValue);
            if (effect != null)
            {
                CharacterEffect characterEffect = new CharacterEffect(character, effect, characterChoiceId);
                character.CharacterEffects.Add(characterEffect);
            }
        }

        public override void RemoveChoice(Character character, int characterChoiceId)
        {
            List<CharacterEffect> toRemove = character.CharacterEffects.Where(c => c.SourceChoice != null && c.SourceChoice == characterChoiceId).ToList();
            foreach (CharacterEffect characterEffect in toRemove)
            {
                characterEffect.RemoveEffect();
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
    /// A choice between multiple features
    /// </summary>
    public class FeatureChoice : Choice
    {
        public override Dictionary<int, string> Options { 
            get { 
                Dictionary<int, string> options = new Dictionary<int, string>();
                foreach (Feature feature in Features)
                {
                    options.Add(feature.FeatureId, feature.ToString());
                }
                return options;
            }}

        public List<Feature> Features { get; set; }

        /// <summary>
        /// This indicates whether this choice should load a specific collection of features
        /// 0 - No auto load
        /// 1 - Automatically set Features to All Feats
        /// Those are the only options for now, 
        /// but in the future it may include fighting styles, warlock invocations, artificer infusions, etc.
        /// </summary>
        public int AutoLoad { get; private set; }

        public FeatureChoice(List<Feature> features)
        {
            Features = features;
            AutoLoad = 0;
            DefaultChoice = Features[0].FeatureId;
        }

        /// <summary>
        /// A constructor for a feature choice that automatically loads a specific collection of features
        /// </summary>
        /// <param name="autoLoad">0 - No auto load
        /// 1 - Automatically set Features to All Feats</param>
        public FeatureChoice(int autoLoad)
        {
            AutoLoad = autoLoad;
            DefaultChoice = 0;
        }

        private FeatureChoice() { }

        public override void ApplyChoice(Character character, int choice, int characterChoiceId)
        {
            throw new NotImplementedException();
        }

        public override void RemoveChoice(Character character, int characterChoiceId)
        {
            throw new NotImplementedException();
        }
    }
}
