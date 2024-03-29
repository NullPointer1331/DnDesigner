﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DnDesigner.Models
{
    /// <summary>
    /// Represents a character feature from various sources
    /// i.e. Class, Subclass, Background
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// The feature's Unique identifier
        /// </summary>
        [Key]
        public int FeatureId { get; private set; }

        /// <summary>
        /// The feature's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The feature's source
        /// formatted as Sourcebook, Class/Subclass/Background/Race, Source name
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The feature's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The level the feature is available at
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// A list of effects that this feature applies to a character
        /// </summary>
        public List<Effect> Effects { get; set; }

        /// <summary>
        /// A list of choices for this feature
        /// </summary>
        public List<Choice> Choices { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="name">The name of the feature</param>
        /// <param name="source">The source of the feature</param>
        /// <param name="description">The feature's description</param>
        /// <param name="level">The level the feature is available at</param>
        public Feature(string name, string description, int level, string source)
        {
            Name = name;
            Source = source;
            Description = description;
            Level = level;
            Effects = new List<Effect>();
            Choices = new List<Choice>();
        }

        /// <summary>
        /// Sourceless constructor, sets all properties except source
        /// Mainly for use by child classes
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="level"></param>
        public Feature(string name, string description, int level) {
            Name = name;
            Description = description;
            Level = level;
            Effects = new List<Effect>();
            Choices = new List<Choice>();
        }

        public bool Equals(Feature other)
        {
            return Name == other.Name && Source == other.Source 
                && Description == other.Description && Level == other.Level;
        }

        public override string ToString()
        {
            string str = Name;
            if(Level > 0)
            {
                str += $", Level {Level}";
            }
            str += ", " + Source;
            return str;
        }
    }
    
    /// <summary>
    /// Represents a feature for a class
    /// </summary>
    public class ClassFeature : Feature
    {
        /// <summary>
        /// The subclass that has this feature
        /// </summary>
        [ForeignKey("ClassId")]
        [JsonIgnore]
        public Class Class { get; set; }

        /// <summary>
        /// Whether or not this feature is only available to the initial class
        /// </summary>
        public bool InitialClassOnly { get; set; }

        /// <summary>
        /// Whether or not this feature is only available to classes other than the initial class
        /// </summary>
        public bool MulticlassOnly { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="class">The subclass that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public ClassFeature(Class @class, string name, string description, int level) : base(name, description, level)
        {
            Class = @class;
            Source = $"{Class.Sourcebook}, Class, {Class.Name}";
            InitialClassOnly = false;
            MulticlassOnly = false;
        }
        private ClassFeature() : base("", "", 0) { }
    }
    public class SubclassFeature : Feature
    {
        /// <summary>
        /// The subclass that has this feature
        /// </summary>
        [ForeignKey("SubclassId")]
        [JsonIgnore]
        public Subclass Subclass { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="subclass">The subclass that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public SubclassFeature(Subclass subclass, string name, string description, int level) : base(name, description, level)
        {
            Subclass = subclass;
            Source = $"{Subclass.Sourcebook}, Subclass, {Subclass.Name}";
        }
        private SubclassFeature() : base("", "", 0) { }
    }
    public class RaceFeature : Feature
    {
        /// <summary>
        /// The race that has this feature
        /// </summary>
        [ForeignKey("RaceId")]
        [JsonIgnore]
        public Race Race { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="race">The race that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        /// <param name="level">The level the feature is available at</param>
        public RaceFeature(Race race, string name, string description, int level) : base(name, description, level)
        {
            Race = race;
            Source = $"{Race.Sourcebook}, Race, {Race.Name}";
        }
        private RaceFeature() : base("", "", 0) { }
    }
    public class BackgroundFeature : Feature
    {
        /// <summary>
        /// The background that has this feature
        /// </summary>
        [ForeignKey("BackgroundId")]
        [JsonIgnore]
        public Background Background { get; set; }

        /// <summary>
        /// Full constructor, sets all properties
        /// </summary>
        /// <param name="background">The background that has this feature</param>
        /// <param name="name">The name of the feature</param>
        /// <param name="description">The description of the feature</param>
        public BackgroundFeature(Background background, string name, string description) : base(name, description, 0)
        {
            Background = background;
            Source = $"{Background.Sourcebook}, Background, {Background.Name}";
        }
        private BackgroundFeature() : base("", "", 0) { }
    }

    /// <summary>
    /// A feature that can be selected by the player
    /// instead of being automatically given
    /// </summary>
    public class SelectableFeature : Feature
    {
        public bool Repeatable { get; set; }

        public string Prerequisites { get; set; }

        /// <summary>
        /// What type of feature this is, i.e. Fighting Style, Feat, etc.
        /// </summary>
        public string Type { get; set; }

        public SelectableFeature(string name, string description, int level, string source, 
            bool repeatable, string prerequisites, string type) 
            : base(name, description, level, source)
        {
            Repeatable = repeatable;
            Prerequisites = prerequisites;
            Type = type;
        }

        private SelectableFeature() : base("", "", 0) { }
    }

    public class CharacterFeature
    {
        public int CharacterFeatureId { get; private set; }

        /// <summary>
        /// The character that has the feature
        /// </summary>
        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Character Character { get; set; }

        /// <summary>
        /// The feature given to the character
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// The choices made for the feature
        /// </summary>
        public List<CharacterChoice> Choices { get; set; }

        public CharacterFeature(Character character, Feature feature)
        {
            Character = character;
            Feature = feature;
            Choices = new List<CharacterChoice>();
            foreach (Choice choice in feature.Choices)
            {
                Choices.Add(new CharacterChoice(this, choice));
            }
        }

        private CharacterFeature() { }

        /// <summary>
        /// Applies the feature to the character, including all effects and choices
        /// </summary>
        public void ApplyEffects()
        {
            foreach (CharacterChoice choice in Choices)
            {
                choice.ApplyChoice();
            }
            foreach (Effect effect in Feature.Effects)
            {
                CharacterEffect characterEffect = new CharacterEffect(Character, effect);
                Character.CharacterEffects.Add(characterEffect);
            }
        }

        /// <summary>
        /// Removes the effects and choices of the feature from the character
        /// </summary>
        public void RemoveEffects()
        {
            foreach (CharacterChoice choice in Choices)
            {
                choice.RemoveChoice();
            }
            foreach (Effect effect in Feature.Effects)
            {
                CharacterEffect? existingEffect = Character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                existingEffect?.RemoveEffect();
            }
        }
    }
}
