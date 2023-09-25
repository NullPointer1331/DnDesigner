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

        /* Proficiencies
         * 
         * Equipment proficiencies
         * For saving throws and skills it may be easiest to make a separate class and keep a list here
         * Each would have a name, a stat, and a proficiency multiplier
         * 0 meaning not proficient, 1 meaning proficient, 2 meaning expertise, .5 meaning they're a bard
         */

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

        /* Actions
         * 
         * Yet another class
         * contains a name, description, and an action type (action, bonus action, reaction)
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

        /* Conditions
         * 
         * A list of conditions, which would be another class
         * with a name, description, and a list of effects
         * Ideally conditions could be imported with a 5etools style json file                      
         */
    }
}
