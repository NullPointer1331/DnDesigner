namespace DnDesigner.Models
{
    /// <summary>
    /// The information of the character
    /// </summary>
    public class Character
    {
        /// The Characters name
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The characters current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The characters class
        /// </summary>
        public string Class { get; set; } = null!;

        /// <summary>
        /// The characters subclass
        /// </summary>
        public string Subclass { get; set; } = null!;

        /// <summary>
        /// The characters current proficiency bonus
        /// </summary>
        public int ProficiencyBonus { get; set; }

        /// <summary>
        /// The characters maximum health points
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// The characters current health points
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// The characters temporary health points
        /// </summary>
        public int TempHealth { get; set; }

        /// <summary>
        /// The characters available hit dice
        /// </summary>
        public int AvailableHitDice { get; set; }

        /// <summary>
        /// The characters hit die type
        /// </summary>
        public string HitDieType { get; set; }

        /// <summary>
        /// The characters current walking speed
        /// </summary>
        public int WalkingSpeed { get; set; }

        /// <summary>
        /// The characters Strength stat
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// The characters Dexterity stat
        /// </summary>
        public int Dexterity { get; set; }

        /// <summary>
        /// The characters Constitution stat
        /// </summary>
        public int Constitution { get; set; }

        /// <summary>
        /// The characters Intelligence stat
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// The characters Wisdom stat
        /// </summary>
        public int Wisdom { get; set; }

        /// <summary>
        /// The characters Charisma stat
        /// </summary>
        public int Charisma { get; set; }

        /// <summary>
        /// The characters resistances
        /// </summary>
        public string Resistant { get; set; } = null!;

        /// <summary>
        /// The characters immunities
        /// </summary>
        public string Immune { get; set; } = null!;

        /// <summary>
        /// The characters vulnerabilities
        /// </summary>
        public string Vulnerable { get; set; } = null!;

        /// <summary>
        /// The weapon and armor types the character is proficient with
        /// </summary>
        public List<CharacterProficiency> EquipmentProficiencies { get; set; } = null!;

        /// <summary>
        /// The tool and instrument types the character is proficient with
        /// </summary>
        public List<CharacterProficiency> ToolProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of the languages the character is proficient in
        /// </summary>
        public List<CharacterProficiency> LanguageProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of saving throws and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SavingThrowProficiencies { get; set; } = null!;

        /// <summary>
        /// A list of skills and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SkillProficiencies { get; set; } = null!;

        /* Classes
         * 
         * Classes would need to be a separate class 
         * with a name, list of features, something to indicate spellcasting type, and hit dice type
         * also a spell list if they're a spellcaster, though maybe that should be done in the spell
         * 
         * Subclasses would be another class
         * with a name, list of features, a reference to the class it's a subclass of, and something to indicate spellcasting type
         * 
         * I have no idea how we would make the features work
         * 
         * We would need to consolidate them potentially into yet another class
         * keeping track of the class, subclass, and level, and keeping a list of those here
         * 
         * Ideally classes and subclasses could be imported with a 5etools style json file
         */

        /* Spellcasting
         * 
         * Spellcasting would be a separate class
         * with a spellcasting ability score, spell save DC, spell attack bonus, spellcasting level, and a list of spells
         * 
         * Spells would be another class
         * containing a name, level, school, casting time, range, components, duration, and description
         * they should probably also have an associated action
         * 
         * Ideally spells could be imported with a 5etools style json file
         */


        /* Inventory
         * 
         * A list of items, which would be another class
         * probably a parent class with subclasses for different types of items
         * 
         * Keep track of coins and weight
         * 
         * Also keep track equipment slots, and what's in them
         * main hand, offhand, armor, and (by base) 3 attunement slots
         * 
         * Ideally items could be imported with a 5etools style json file
         */

    }
}
