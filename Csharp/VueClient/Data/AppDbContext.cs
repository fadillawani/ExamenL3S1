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
    public virtual DbSet<Complement> complement { get; set; }
    public virtual DbSet<LivraisonAffectation> livraison_affectation { get; set; }
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
    entity.ToTable("commande");

    entity.HasOne(c => c.Panier)
          .WithMany()
          .HasForeignKey(c => c.PanierId)
          .OnDelete(DeleteBehavior.Cascade);

    entity.Property(c => c.Etat)
          .HasColumnName("etat")
          .HasColumnType("statut_commande");

    entity.Property(c => c.DateCommande)
          .HasColumnName("date_commande")
          .HasColumnType("timestamptz");
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
    entity.HasKey(e => e.id).HasName("livraison_affectation_pkey");
    entity.HasIndex(e => e.commande_id, "livraison_affectation_commande_id_key").IsUnique();

    entity.Property(e => e.Statut)
        .HasColumnName("statut")
        .HasColumnType("statut_livraison");

    entity.HasOne(d => d.commande).WithOne(p => p.LivraisonAffectation)
        .HasForeignKey<LivraisonAffectation>(d => d.commande_id)
        .HasConstraintName("livraison_affectation_commande_id_fkey");

    entity.HasOne(d => d.livreur).WithMany(p => p.livraison_affectation)
        .HasForeignKey(d => d.livreur_id)
        .HasConstraintName("livraison_affectation_livreur_id_fkey");

    entity.HasOne(d => d.zone).WithMany(p => p.livraison_affectation)
        .HasForeignKey(d => d.zone_id)
        .HasConstraintName("livraison_affectation_zone_id_fkey");
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
          .HasColumnName("date")
          .HasColumnType("timestamptz") 
          .HasDefaultValueSql("now()");

    entity.Property(e => e.ref_transaction).HasMaxLength(255);

    entity.HasOne(d => d.commande).WithOne(p => p.Paiement) 
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

      

// =====================
        // PANIER
        // =====================
        modelBuilder.Entity<Panier>(entity =>
        {
            entity.ToTable("panier");
            entity.HasKey(e => e.id);

            entity.Property(e => e.client_id).HasColumnName("client_id");
            entity.Property(e => e.created_at)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamptz"); 
            entity.Property(e => e.updated_at)
                             .HasColumnName("updated_at")
                             .HasColumnType("timestamptz");
            entity.Property(e => e.is_validated).HasColumnName("is_validated");

            entity.HasOne(e => e.client)
                .WithMany(u => u.panier)
                .HasForeignKey(e => e.client_id)
                .HasConstraintName("panier_client_id_fkey");
        });

        // =====================
        // PANIER_ITEM
        // =====================
       modelBuilder.Entity<PanierItem>(entity =>
{
    entity.ToTable("panier_item");
    entity.HasKey(e => e.id);

    entity.Property(e => e.panier_id).HasColumnName("panier_id");
    entity.Property(e => e.burger_id).HasColumnName("burger_id");
    entity.Property(e => e.menu_id).HasColumnName("menu_id");
    entity.Property(e => e.complement_id).HasColumnName("complement_id");
    entity.Property(e => e.quantite).HasColumnName("quantite");
    entity.Property(e => e.prix_total).HasColumnName("prix_total");

    entity.HasOne(e => e.panier)
        .WithMany(p => p.panier_item)
        .HasForeignKey(e => e.panier_id)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("panier_item_panier_id_fkey");

    entity.HasOne(e => e.burger)
        .WithMany(b => b.panier_item)
        .HasForeignKey(e => e.burger_id)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("panier_item_burger_id_fkey");

    entity.HasOne(e => e.menu)
        .WithMany()
        .HasForeignKey(e => e.menu_id)
        .HasConstraintName("panier_item_menu_id_fkey");

    entity.HasOne(e => e.complement)
        .WithMany()
        .HasForeignKey(e => e.complement_id)
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
