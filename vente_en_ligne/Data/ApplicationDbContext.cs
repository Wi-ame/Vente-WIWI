using Microsoft.EntityFrameworkCore;
using vente_en_ligne.Models;

namespace vente_en_ligne.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Proprietaire> Proprietaires { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<PanierPrinc> PanierPrincs { get; set; }
        public DbSet<Panier> Panier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>().HasKey(u => u.ID);

            modelBuilder.Entity<Admin>().HasKey(u => u.CIN);

            modelBuilder.Entity<Proprietaire>().HasKey(u => u.INterID);

            modelBuilder.Entity<PanierPrinc>().HasKey(p => p.PID);

            modelBuilder.Entity<Categories>().HasKey(c => c.CategorieID);

            modelBuilder.Entity<Panier>().HasKey(pa => pa.Id);


            modelBuilder.Entity<Produit>()
                .HasOne(pr => pr.Categories)
                .WithMany()
                .HasForeignKey(pr => pr.IDC)  // Utiliser la clé de la classe dérivée comme clé étrangère
                .IsRequired(false);

            modelBuilder.Entity<Produit>()
                .HasOne(pr => pr.proprietaires)
                .WithMany()
                .HasForeignKey(pr => pr.IDP)  // Utiliser la clé de la classe dérivée comme clé étrangère
                .IsRequired(false);

            modelBuilder.Entity<PanierPrinc>()
               .HasOne(pn => pn.Utilisateur)
               .WithMany()
               .HasForeignKey(pn => pn.IDU)  // Utiliser la clé de la classe dérivée comme clé étrangère
               .IsRequired(false);

            modelBuilder.Entity<Panier>()
              .HasOne(pa => pa.PanierPrinc)
              .WithMany()
              .HasForeignKey(pn => pn.IDPa)  // Utiliser la clé de la classe dérivée comme clé étrangère
              .IsRequired(false);

            modelBuilder.Entity<Panier>()
              .HasOne(pa => pa.produit)
              .WithMany()
              .HasForeignKey(pn => pn.IDPro)  // Utiliser la clé de la classe dérivée comme clé étrangère
              .IsRequired(false);



            base.OnModelCreating(modelBuilder);
        }
  
    }
}
