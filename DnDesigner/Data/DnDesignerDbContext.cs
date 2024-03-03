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
            builder.Entity<Item>()
                .HasOne(e => e.SourceBook)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Class>()
                .HasOne(c => c.SourceBook)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Subclass>()
                .HasOne(c => c.SourceBook)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Subclass>()
                .HasOne(c => c.Class)
				.WithMany(c => c.Subclasses)
				.OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Race>()
				.HasOne(r => r.SourceBook)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Background>()
                .HasOne(b => b.SourceBook)
				.WithMany()
				.OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Feature>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<Feature>()
                .HasMany(e => e.Choices)
                .WithOne();
            builder.Entity<Feature>()
                .HasOne(e => e.SourceBook)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SubclassFeature>()
                .HasOne(f => f.Subclass)
                .WithMany(s => s.Features)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<CharacterFeature>()
                .HasOne(f => f.Feature)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<FeatureChoice>()
                .HasMany(f => f.Features)
                .WithMany();
            builder.Entity<EffectChoice>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<GroupedEffect>()
                .HasMany(e => e.Effects)
                .WithOne();
            builder.Entity<ModifyAttribute>();
            builder.Entity<ModifyArmorClass>();
            builder.Entity<SetArmorClass>();
            builder.Entity<GrantProficiencies>()
                .HasMany(e => e.Proficiencies)
                .WithMany();
            builder.Entity<GrantAction>()
                .HasOne(e => e.Action)
                .WithMany();
            builder.Entity<GrantResource>()
                .HasOne(e => e.Resource)
                .WithMany();
            builder.Entity<Spellcasting>()
                .HasMany(e => e.LearnableSpells)
                .WithMany(e => e.LearnedBy);
            builder.Entity<CharacterSpellcasting>()
                .HasMany(e => e.PreparedSpells)
                .WithMany();
            base.OnModelCreating(builder);
        }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Effect> Effects { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<SelectableFeature> SelectableFeatures { get; set; }
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
        public DbSet<Resource> Resources { get; set; }
        public DbSet<SpellSlot> SpellSlots { get; set; }
        public DbSet<CharacterResource> CharacterResources { get; set; }
    }
}