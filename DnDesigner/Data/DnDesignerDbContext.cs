using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DnDesigner.Models;

namespace DnDesigner.Data
{
    public class DnDesignerDbContext : IdentityDbContext
    {
        public DnDesignerDbContext(DbContextOptions<DnDesignerDbContext> options)
            : base(options)
        {
        }

        DbSet<Proficiency> Proficiencies { get; set; }
        DbSet<Background> Backgrounds { get; set; }
        DbSet<BackgroundFeature> BackgroundFeatures { get; set; }
        DbSet<BackgroundProficiency> BackgroundProficiencies { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<ClassFeature> ClassFeatures { get; set; }
        DbSet<ClassProficiency> ClassProficiencies { get; set; }
        DbSet<Subclass> Subclasses { get; set; }
        DbSet<SubclassFeature> SubclassFeatures { get; set; }
        DbSet<Race> Races { get; set; }
        DbSet<RaceFeature> RaceFeatures { get; set; }
        DbSet<RaceProficiency> RaceProficiencies { get; set; }
        DbSet<Spellcasting> Spellcasting { get; set; }
        DbSet<Spell> Spells { get; set; }
        DbSet<LearnableSpell> LearnableSpells { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<CharacterProficiency> CharacterProficiencies { get; set; }
        DbSet<CharacterClass> CharacterClasses { get; set; }
        DbSet<CharacterSpellcasting> CharacterSpellcasting { get; set;}
        DbSet<KnownSpell> KnownSpells { get; set; }
        DbSet<Inventory> Inventory { get; set; }
        DbSet<InventoryItem> InventoryItems { get; set; } 
    }
}