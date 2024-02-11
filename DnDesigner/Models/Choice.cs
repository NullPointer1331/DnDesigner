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

        /// <summary>
        /// The previous choice value
        /// </summary>
        public int PreviousChoiceValue { get; private set; }

        public bool IsApplied { get; set; }

        public CharacterChoice(CharacterFeature characterFeature, Choice choice)
        {
            CharacterFeature = characterFeature;
            Choice = choice;
            ChoiceValue = Choice.DefaultChoice;
            PreviousChoiceValue = ChoiceValue;
            IsApplied = false;
        }

        private CharacterChoice() { }

        /// <summary>
        /// Applies the choice to the character
        /// </summary>
        public void ApplyChoice()
        {
            CheckUnapplied();
            Choice.ApplyChoice(CharacterFeature.Character, this);
            IsApplied = true;
            PreviousChoiceValue = ChoiceValue;
        }

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        public void RemoveChoice()
        {
            CheckUnapplied();
            Choice.RemoveChoice(CharacterFeature.Character, this);
            IsApplied = false;
        }

        /// <summary>
        /// Checks if the choice is no longer applied, and sets IsApplied to false if so.
        /// It doesn't do the opposite because it's harder to check if a choice is applied than if it's not.
        /// </summary>
        public void CheckUnapplied()
        {
            if (IsApplied && Choice.NotApplied(CharacterFeature.Character, this))
            {
                IsApplied = false;
            }
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
        /// <param name="characterChoice">The CharacterChoice calling this</param>
        public abstract void ApplyChoice(Character character, CharacterChoice characterChoice);

        /// <summary>
        /// Removes the choice from the character
        /// </summary>
        /// <param name="character">The character to remove the choice from</param>
        /// <param name="characterChoice">The CharacterChoice calling this</param>
        public abstract void RemoveChoice(Character character, CharacterChoice characterChoice);

        /// <summary>
        /// Checks if the choice is not applied to the character
        /// </summary>
        /// <param name="character">The character being checked</param>
        /// <param name="characterChoice">The CharacterChoice calling this</param>
        /// <returns>True if the choice is definitely not applied to the character, false otherwise</returns>
        public abstract bool NotApplied(Character character, CharacterChoice characterChoice);
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
        public override void ApplyChoice(Character character, CharacterChoice characterChoice)
        {
            RemoveChoice(character, characterChoice);
            Effect? effect = Effects.Find(e => e.EffectId == characterChoice.ChoiceValue);
            if (effect != null)
            {
                CharacterEffect characterEffect = new CharacterEffect(character, effect);
                character.CharacterEffects.Add(characterEffect);
            }
        }

        public override void RemoveChoice(Character character, CharacterChoice characterChoice)
        {
            if (characterChoice.IsApplied)
            {
                CharacterEffect? characterEffect = character.CharacterEffects.Find(e => e.Effect.EffectId == characterChoice.ChoiceValue);
                characterEffect?.RemoveEffect();
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

        public override bool NotApplied(Character character, CharacterChoice characterChoice)
        {
            return !character.CharacterEffects.Where(e => e.Effect.EffectId == characterChoice.ChoiceValue).Any();
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

        public override void ApplyChoice(Character character, CharacterChoice characterChoice)
        {
            if (characterChoice.IsApplied)
            {
                if (characterChoice.ChoiceValue != characterChoice.PreviousChoiceValue)
                {
                    RemoveChoice(character, characterChoice);
                    characterChoice.IsApplied = false;
                }
                else
                {
                    return;
                }
            }
            if (!characterChoice.IsApplied)
            {
                Feature? feature = Features.Find(f => f.FeatureId == characterChoice.ChoiceValue);
                if (feature != null &&
                    (!character.Features.Where(f => f.Feature.FeatureId == feature.FeatureId).Any()
                    || (feature is Feat feat && feat.Repeatable)))
                { // Apply the feature if it is not already applied or if it is a repeatable feat
                    CharacterFeature characterFeature = new CharacterFeature(character, feature);
                    character.Features.Add(characterFeature);
                }
            }
        }

        public override void RemoveChoice(Character character, CharacterChoice characterChoice)
        {
            if (characterChoice.IsApplied)
            {
                CharacterFeature? characterFeature = character.Features.Find(f => f.Feature.FeatureId == characterChoice.ChoiceValue);
                if (characterFeature != null)
                {
                    characterFeature.RemoveEffects();
                    character.Features.Remove(characterFeature);
                }
            }
        }

        public override bool NotApplied(Character character, CharacterChoice characterChoice)
        {
            return !character.Features.Where(f => f.Feature.FeatureId == characterChoice.ChoiceValue).Any();
        }
    }
}
