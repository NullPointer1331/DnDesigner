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
    public class CharacterFeature : Feature
    {
        /// <summary>
        /// The character that has the feature
        /// </summary>
        [ForeignKey("CharacterId")]
        [JsonIgnore]
        public Character Character { get; set; }

        /// <summary>
        /// Constructor converting an existing feature to a character feature
        /// </summary>
        /// <param name="character">The character that has the feature</param>
        /// <param name="feature">The feature given to the character</param>
        public CharacterFeature(Character character, Feature feature) : base(feature.Name, feature.Description, feature.Level, feature.Source)
        {
            Character = character;
            Effects = feature.Effects;
        }

        private CharacterFeature() : base("", "", 0) { }

        public void ApplyEffect() {             
            foreach (Effect effect in Effects)
            {
                CharacterEffect? existingEffect = Character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                if(existingEffect != null)
                {
                    existingEffect.ApplyEffect();
                }
                else
                {
                    CharacterEffect characterEffect = new CharacterEffect(Character, effect);
                    Character.CharacterEffects.Add(characterEffect);
                    characterEffect.ApplyEffect();
                }
            }
        }
        public void RemoveEffect()
        {
            foreach (Effect effect in Effects)
            {
                CharacterEffect? existingEffect = Character.CharacterEffects.Find(e => e.Effect.EffectId == effect.EffectId);
                if (existingEffect != null)
                {
                    existingEffect.RemoveEffect();
                    Character.CharacterEffects.Remove(existingEffect);
                }
            }
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
}
