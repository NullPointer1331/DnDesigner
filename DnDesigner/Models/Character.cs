namespace DnDesigner.Models
{
    public class Character
    {
        /* Character Basics
         * 
         * Str, Dex, Con, Int, Wis, Cha
         * Armor Class (will need to be flexible with how we calculate it)
         * Health, hit dice, hit die type, temp health
         * Resistances, vulnerabilities, and immunities
         * Speeds
         * Initiative
         * Level
         * Proficiency bonus
         * Classes and their levels
         */

        /// <summary>
        /// The background of the character
        /// </summary>
        public Background Background { get; set; }

        /// <summary>
        /// The weapon and armor types the character is proficient with
        /// </summary>
        public string EquipmentProficiencies { get; set; }

        /// <summary>
        /// The tool and instrument types the character is proficient with
        /// </summary>
        public string ToolProficiencies { get; set; }

        /// <summary>
        /// A list of the languages the character is proficient in
        /// </summary>
        public string LanguageProficiencies { get; set; }

        /// <summary>
        /// A list of saving throws and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SavingThrowProficiencies { get; set; }

        /// <summary>
        /// A list of skills and the character's proficiency in them
        /// </summary>
        public List<CharacterProficiency> SkillProficiencies { get; set; }

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
