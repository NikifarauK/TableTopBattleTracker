using Microsoft.EntityFrameworkCore;
using Npgsql;
using TableTopBattleTracker.Model;


namespace TableTopBattleTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

#region DbSets
        public DbSet<Model.Action>? Actions { get; set; }
        public DbSet<ActionDamageType>? ActionDamageTypes { get; set; }
        public DbSet<ActionDCType>? ActionDCTypes { get; set; }
        public DbSet<AreaOfEffect>? AreaOfEffects { get; set; }
        public DbSet<CastingComponent>? CastingComponents { get; set; }
        public DbSet<CastRange>? CastRanges { get; set; }
        public DbSet<CastTime>? CastTimes { get; set; }
        public DbSet<Character>? Characters { get; set; }
        public DbSet<Characteristic>? Characteristics { get; set; }
        public DbSet<Condition>? Conditions { get; set; }
        public DbSet<ConditionImmunity>? ConditionImmunities { get; set; }
        public DbSet<DamageImmunitie>? DamageImmunities { get; set; }
        public DbSet<DamageResistance>? DamageResistances { get; set; }
        public DbSet<DamageType>? DamageTypes { get; set; }
        public DbSet<DamageVulnerabilitie>? DamageVulnerabilities { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<LegendaryAction>? LegendaryActions { get; set; }
        public DbSet<Monster>? Monsters { get; set; }
        public DbSet<MonsterAction>? MonsterActions { get; set; }
        public DbSet<MonsterLanguage>? MonsterLanguages { get; set; }
        public DbSet<MonsterProficiency>? MonsterProficiencies { get; set; }
        public DbSet<MonsterSense>? MonsterSenses { get; set; }
        public DbSet<MonsterSize>? MonsterSizes { get; set; }
        public DbSet<MonsterSpeed>? MonsterSpeeds { get; set; }
        public DbSet<MonsterType>? MonsterTypes { get; set; }
        public DbSet<MultiAction>? MultiActions { get; set; }
        public DbSet<Proficiency>? Proficiencies { get; set; }
        public DbSet<Sense>? Senses { get; set; }
        public DbSet<Slot>? Slots{ get; set; }
        public DbSet<SpecialAbility>? SpecialAbilities{ get; set; }
        public DbSet<SpeedType>? SpeedTypes { get; set; }
        public DbSet<Spell>? Spells { get; set; }
        public DbSet<Spellcasting>? Spellcastings { get; set; }
        public DbSet<SpellCastingComponent>? SpellCastingComponents { get; set; }
        public DbSet<SpellcastingSlot>? SpellcastingSlots { get; set; }
        public DbSet<SpellcastingSpell>? SpellcastingsSpells { get; set; }
        public DbSet<SpellDamage>? SpellDamages { get; set; }
        public DbSet<SpellDamageValue>? SpellDamageValues { get; set; }
        public DbSet<SpellSchool>? SpellSchools { get; set; }
        public DbSet<Usage>? Usages { get; set; }

#endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Action>()
                .HasMany(a => a.DamageType)
                .WithMany(d => d.Actions)
                .UsingEntity<ActionDamageType>();
            modelBuilder.Entity<Model.Action>()
                .HasMany(a => a.DCTypes)
                .WithMany(c => c.Actions)
                .UsingEntity<ActionDCType>();
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Languages)
                .WithMany(la => la.Characters)
                .UsingEntity<MonsterLanguage>();
            modelBuilder.Entity<Spell>()
                .HasMany(s => s.CastingComponents)
                .WithMany(cc => cc.Spells)
                .UsingEntity<SpellCastingComponent>();
            modelBuilder.Entity<Spell>()
                .HasOne(s => s.SpellSchool)
                .WithMany();
            modelBuilder.Entity<Spell>()
                .HasOne(s => s.CastRange)
                .WithMany();
            modelBuilder.Entity<Spell>()
                .HasOne(s=> s.CastTime)
                .WithMany();

            modelBuilder.Entity<ActionDamageType>()
                .HasKey(adt => new { adt.ActionId, adt.DamagetypeId });
            modelBuilder.Entity<ActionDCType>()
                .HasKey(at => new { at.ActionId, at.CharacteristicId });

            modelBuilder.Entity<ConditionImmunity>()
                .HasKey(ci => new { ci.CharacterId, ci.ConditionId } );
            modelBuilder.Entity<DamageImmunitie>()
                .HasKey(di => new { di.CharacterId, di.DamageTypeId });
            modelBuilder.Entity<DamageResistance>()
                .HasKey(dr => new { dr.CharacterId, dr.DamageTypeId });
            modelBuilder.Entity<DamageVulnerabilitie>()
                .HasKey(dv => new { dv.CharacterId, dv.DamageTypeId });
            modelBuilder.Entity<LegendaryAction>()
                .HasKey(la => new { la.CharacterId, la.ActionId });
            modelBuilder.Entity<MonsterAction>()
                .HasKey(ma => new { ma.CharacterId, ma.ActionId });
            modelBuilder.Entity<MonsterLanguage>()
                .HasKey(ml => new { ml.CharacterId, ml.LanguageId });
            modelBuilder.Entity<MonsterProficiency>()
                .HasKey(ml => new { ml.CharacterId, ml.ProficiencyId });
            modelBuilder.Entity<MonsterSense>()
                .HasKey(ms => new { ms.CharacterId, ms.SenseId });
            modelBuilder.Entity<MonsterSpeed>()
                .HasKey(ms => new { ms.CharacterId, ms.SpeedTypeId });
            modelBuilder.Entity<MultiAction>()
                .HasKey(ma => new { ma.CharacterId, ma.MonsterActionId });
            modelBuilder.Entity<SpellCastingComponent>()
                .HasKey(sc => new { sc.SpellId, sc.CastingComponentId });
            modelBuilder.Entity<SpellcastingSlot>()
                .HasKey(ss => new { ss.SpellcastingId, ss.SlotId });
            modelBuilder.Entity<SpellcastingSpell>()
                .HasKey(ss => new { ss.SpellId, ss.SpellcastingId });

            modelBuilder.Entity<SpellDamage>()
                .HasKey(sd => new { sd.SpellId, sd.DamageTypeId });
            modelBuilder.Entity<SpellDamageValue>()
                .HasKey(sv => new { sv.SpellId,sv.DamageTypeId, sv.Level });
        }

    }
}
