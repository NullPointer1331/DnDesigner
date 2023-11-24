using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DnDesigner.Models;
using System.Text.Json;

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
            builder.Entity<Item>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<BackgroundFeature>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<CharacterFeature>()
                .HasMany(e => e.Effects)
                .WithMany();
            builder.Entity<ClassFeature>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<SubclassFeature>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<RaceFeature>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<EffectChoice>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<ModifyAttribute>();
            builder.Entity<GrantProficiencies>()
                .HasMany(e => e.Proficiencies)
                .WithMany();
            builder.Entity<GrantAction>()
                .HasOne(e => e.Action)
                .WithMany();
            builder.Entity<Spellcasting>()
                .HasMany(e => e.LearnableSpells)
                .WithMany(e => e.LearnedBy);
            builder.Entity<CharacterSpellcasting>()
                .HasMany(e => e.PreparedSpells)
                .WithMany();
            base.OnModelCreating(builder);
        }
        public DbSet<Effect> Effects { get; set; }
        public DbSet<CharacterEffect> CharacterEffects { get; set; }
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
        public DbSet<Item> Items { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterProficiency> CharacterProficiencies { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<CharacterSpellcasting> CharacterSpellcasting { get; set;}
        public DbSet<CharacterFeature> CharacterFeatures { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; } 
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<CharacterAction> CharacterActions { get; set; }
    }
}