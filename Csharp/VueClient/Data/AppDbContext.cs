using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VueClient.Models;
using VueClient.Models.Enum;

namespace VueClient.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Burger> burger { get; set; }
    public virtual DbSet<BurgerCategorie> burger_categorie { get; set; }
    public virtual DbSet<Commande> commande { get; set; }
    public virtual DbSet<CommandeItem> commande_item { get; set; }
    public virtual DbSet<Complement> complement { get; set; }
    public virtual DbSet<LivraisonAffectation> livraison_affection { get; set; }
    public virtual DbSet<Menu> menu { get; set; }
    public virtual DbSet<MenuBurger> menu_burger { get; set; }
    public virtual DbSet<MenuComplement> menu_complement { get; set; }
    public virtual DbSet<Paiement> paiement { get; set; }
    public virtual DbSet<Quartier> quartier { get; set; }
    public virtual DbSet<Users> users { get; set; }
    public virtual DbSet<Zone> zone { get; set; }
    public DbSet<Panier> panier { get; set; }
public DbSet<PanierItem> panier_item { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapper explicitement les enums PostgreSQL
        modelBuilder.HasPostgresEnum<TypeComplement>("public", "type_complement");
        modelBuilder.HasPostgresEnum<StatutCommande>("public", "statut_commande");
        modelBuilder.HasPostgresEnum<TypeRetrait>("public", "type_retrait");
        modelBuilder.HasPostgresEnum<MoyenPaiement>("public", "moyen_paiement");
        modelBuilder.HasPostgresEnum<RoleUser>("public", "role_user");
        modelBuilder.HasPostgresEnum<StatutLivraison>("public", "statut_livraison");

        // Configuration des entités
        modelBuilder.Entity<Burger>(entity =>
        {
            entity.HasKey(e => e.id).HasName("burger_pkey");
            entity.Property(e => e.image_url).HasMaxLength(255);
            entity.Property(e => e.is_archived).HasDefaultValue(false);
            entity.Property(e => e.libelle).HasMaxLength(150);

            entity.HasOne(d => d.burger_categorie).WithMany(p => p.burger)
                .HasForeignKey(d => d.burger_categorie_id)
                .HasConstraintName("burger_burger_categorie_id_fkey");
        });

        modelBuilder.Entity<BurgerCategorie>(entity =>
        {
            entity.HasKey(e => e.id).HasName("burger_categorie_pkey");
            entity.HasIndex(e => e.nom, "burger_categorie_nom_key").IsUnique();
            entity.Property(e => e.nom).HasMaxLength(150);
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.id).HasName("commande_pkey");
            entity.HasIndex(e => e.client_id, "idx_commande_client");
            entity.HasIndex(e => e.created_at, "idx_commande_date");

            entity.Property(e => e.Statut)
                .HasColumnName("statut")
                .HasColumnType("statut_commande");

            entity.Property(e => e.TypeRetrait)
                .HasColumnName("type_retrait")
                .HasColumnType("type_retrait");

            entity.Property(e => e.created_at)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.is_paid).HasDefaultValue(false);
            entity.Property(e => e.updated_at)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.client).WithMany(p => p.commande)
                .HasForeignKey(d => d.client_id)
                .HasConstraintName("commande_client_id_fkey");

            entity.HasOne(d => d.quartier).WithMany(p => p.commande)
                .HasForeignKey(d => d.quartier_id)
                .HasConstraintName("commande_quartier_id_fkey");
        });

        modelBuilder.Entity<CommandeItem>(entity =>
        {
            entity.HasKey(e => e.id).HasName("commande_item_pkey");
            entity.HasIndex(e => e.commande_id, "idx_item_commande");

            entity.HasOne(d => d.burger).WithMany(p => p.commande_item)
                .HasForeignKey(d => d.burger_id)
                .HasConstraintName("commande_item_burger_id_fkey");

            entity.HasOne(d => d.commande).WithMany(p => p.commande_item)
                .HasForeignKey(d => d.commande_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("commande_item_commande_id_fkey");

            entity.HasOne(d => d.complement).WithMany(p => p.commande_item)
                .HasForeignKey(d => d.complement_id)
                .HasConstraintName("commande_item_complement_id_fkey");

            entity.HasOne(d => d.menu).WithMany(p => p.commande_item)
                .HasForeignKey(d => d.menu_id)
                .HasConstraintName("commande_item_menu_id_fkey");
        });

        modelBuilder.Entity<Complement>(entity =>
        {
            entity.HasKey(e => e.id).HasName("complement_pkey");
            entity.Property(e => e.TypeComplement)
                .HasColumnName("type_complement")
                .HasColumnType("type_complement");
            entity.Property(e => e.image_url).HasMaxLength(255);
            entity.Property(e => e.is_archived).HasDefaultValue(false);
            entity.Property(e => e.libelle).HasMaxLength(150);
        });

        modelBuilder.Entity<LivraisonAffectation>(entity =>
        {
            entity.HasKey(e => e.id).HasName("livraison_affection_pkey");
            entity.HasIndex(e => e.commande_id, "livraison_affection_commande_id_key").IsUnique();

            entity.Property(e => e.Statut)
                .HasColumnName("statut")
                .HasColumnType("statut_livraison");

            entity.HasOne(d => d.commande).WithOne(p => p.livraison_affection)
                .HasForeignKey<LivraisonAffectation>(d => d.commande_id)
                .HasConstraintName("livraison_affection_commande_id_fkey");

            entity.HasOne(d => d.livreur).WithMany(p => p.livraison_affection)
                .HasForeignKey(d => d.livreur_id)
                .HasConstraintName("livraison_affection_livreur_id_fkey");

            entity.HasOne(d => d.zone).WithMany(p => p.livraison_affection)
                .HasForeignKey(d => d.zone_id)
                .HasConstraintName("livraison_affection_zone_id_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.id).HasName("menu_pkey");
            entity.Property(e => e.image_url).HasMaxLength(255);
            entity.Property(e => e.is_archived).HasDefaultValue(false);
            entity.Property(e => e.libelle).HasMaxLength(150);
        });

        modelBuilder.Entity<MenuBurger>(entity =>
        {
            entity.HasKey(e => e.id).HasName("menu_burger_pkey");
            entity.HasIndex(e => e.menu_id, "idx_menu_burger_menu");

            entity.HasOne(d => d.burger).WithMany(p => p.menu_burger)
                .HasForeignKey(d => d.burger_id)
                .HasConstraintName("menu_burger_burger_id_fkey");

            entity.HasOne(d => d.menu).WithMany(p => p.menu_burger)
                .HasForeignKey(d => d.menu_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("menu_burger_menu_id_fkey");
        });

        modelBuilder.Entity<MenuComplement>(entity =>
        {
            entity.HasKey(e => e.id).HasName("menu_complement_pkey");

            entity.HasOne(d => d.complement).WithMany(p => p.menu_complement)
                .HasForeignKey(d => d.complement_id)
                .HasConstraintName("menu_complement_complement_id_fkey");

            entity.HasOne(d => d.menu).WithMany(p => p.menu_complement)
                .HasForeignKey(d => d.menu_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("menu_complement_menu_id_fkey");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.id).HasName("paiement_pkey");
            entity.HasIndex(e => e.commande_id, "paiement_commande_id_key").IsUnique();

            entity.Property(e => e.MoyenPaiement)
                .HasColumnName("moyen_paiement")
                .HasColumnType("moyen_paiement");

            entity.Property(e => e.date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.ref_transaction).HasMaxLength(255);

            entity.HasOne(d => d.commande).WithOne(p => p.paiement)
                .HasForeignKey<Paiement>(d => d.commande_id)
                .HasConstraintName("paiement_commande_id_fkey");
        });

        modelBuilder.Entity<Quartier>(entity =>
        {
            entity.HasKey(e => e.id).HasName("quartier_pkey");
            entity.HasIndex(e => e.zone_id, "idx_quartier_zone");

            entity.Property(e => e.nom).HasMaxLength(150);

            entity.HasOne(d => d.zone).WithMany(p => p.quartier)
                .HasForeignKey(d => d.zone_id)
                .HasConstraintName("quartier_zone_id_fkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");
            entity.HasIndex(e => e.email, "users_email_key").IsUnique();
            entity.HasIndex(e => e.tel, "users_tel_key").IsUnique();

            entity.Property(e => e.role)
                .HasColumnType("role_user");

            entity.Property(e => e.created_at)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.is_archived).HasDefaultValue(false);
            entity.Property(e => e.nom).HasMaxLength(100);
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.prenom).HasMaxLength(100);
            entity.Property(e => e.tel).HasMaxLength(20);
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.id).HasName("zone_pkey");
            entity.Property(e => e.nom).HasMaxLength(150);
        });

        modelBuilder.Entity<Panier>(entity =>
{
    entity.HasKey(e => e.id).HasName("panier_pkey");

    entity.Property(e => e.created_at)
        .HasDefaultValueSql("now()")
        .HasColumnType("timestamp without time zone");

    entity.Property(e => e.updated_at)
        .HasDefaultValueSql("now()")
        .HasColumnType("timestamp without time zone");

    entity.Property(e => e.is_validated)
        .HasDefaultValue(false);

    entity.HasOne(d => d.client)
        .WithMany(p => p.panier)
        .HasForeignKey(d => d.client_id)
        .HasConstraintName("panier_client_id_fkey");
});

modelBuilder.Entity<PanierItem>(entity =>
{
    entity.HasKey(e => e.id).HasName("panier_item_pkey");

    entity.HasOne(d => d.panier)
        .WithMany(p => p.panier_item)
        .HasForeignKey(d => d.panier_id)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("panier_item_panier_id_fkey");

    entity.HasOne(d => d.burger)
        .WithMany()
        .HasForeignKey(d => d.burger_id)
        .HasConstraintName("panier_item_burger_id_fkey");

    entity.HasOne(d => d.menu)
        .WithMany()
        .HasForeignKey(d => d.menu_id)
        .HasConstraintName("panier_item_menu_id_fkey");

    entity.HasOne(d => d.complement)
        .WithMany()
        .HasForeignKey(d => d.complement_id)
        .HasConstraintName("panier_item_complement_id_fkey");

    entity.HasCheckConstraint(
        "CK_panier_item_one_product",
        "(CASE WHEN burger_id IS NOT NULL THEN 1 ELSE 0 END + " +
        "CASE WHEN menu_id IS NOT NULL THEN 1 ELSE 0 END + " +
        "CASE WHEN complement_id IS NOT NULL THEN 1 ELSE 0 END) = 1"
    );
});



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
