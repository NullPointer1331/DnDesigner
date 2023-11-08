using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DnDesigner.Models;
using System.Text.Json;
using NuGet.Protocol;

namespace DnDesigner.Data
{
    public class DnDesignerDbContext : IdentityDbContext
    {
        public DnDesignerDbContext(DbContextOptions<DnDesignerDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            builder
               .Entity<ClassFeature>()
               .Property(e => e.CharacterModifiers)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, options),
                   v => JsonSerializer.Deserialize<List<CharacterModifier>>(v, options));
            builder
               .Entity<SubclassFeature>()
               .Property(e => e.CharacterModifiers)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, options),
                   v => JsonSerializer.Deserialize<List<CharacterModifier>>(v, options));
            builder
               .Entity<RaceFeature>()
               .Property(e => e.CharacterModifiers)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, options),
                   v => JsonSerializer.Deserialize<List<CharacterModifier>>(v, options));
            builder
               .Entity<BackgroundFeature>()
               .Property(e => e.CharacterModifiers)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, options),
                   v => JsonSerializer.Deserialize<List<CharacterModifier>>(v, options));
            builder
               .Entity<CharacterFeature>()
               .Property(e => e.CharacterModifiers)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, options),
                   v => JsonSerializer.Deserialize<List<CharacterModifier>>(v, options));
            base.OnModelCreating(builder);
        }
        public DbSet<Proficiency> Proficiencies { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<BackgroundFeature> BackgroundFeatures { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassFeature> ClassFeatures { get; set; }
        public DbSet<Subclass> Subclasses { get; set; }
        public DbSet<SubclassFeature> SubclassFeatures { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceFeature> RaceFeatures { get; set; }
        public DbSet<Spellcasting> Spellcasting { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<LearnableSpell> LearnableSpells { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterProficiency> CharacterProficiencies { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<CharacterSpellcasting> CharacterSpellcasting { get; set;}
        public DbSet<CharacterFeature> CharacterFeatures { get; set; }
        public DbSet<KnownSpell> KnownSpells { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; } 
    }
}