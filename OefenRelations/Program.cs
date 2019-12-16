using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OefenRelations
{
    class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestRelations;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * Zonder Fluent API weet EntityFramework Core niet
             * wat de primary key in de tussentabel moet zijn.
             * (want worden 2 Id's meegegeven)
            */
            modelBuilder.Entity<FilmActeurs>()
                .HasKey(p => new { p.ActeurId, p.FilmId }); // FilmActeurs heeft 2 pk's

            // 2 One-to-many relaties definieren
            modelBuilder.Entity<FilmActeurs>()
                .HasOne(a => a.Acteur)
                .WithMany(fa => fa.FilmActeurs)
                .HasForeignKey(a => a.ActeurId);

            modelBuilder.Entity<FilmActeurs>()
                .HasOne(f => f.Film)
                .WithMany(fa => fa.FilmActeurs)
                .HasForeignKey(f => f.FilmId);
        }


        public DbSet<Acteur> Acteurs { get; set; }
        public DbSet<Film> Films { get; set; }

        public DbSet<Product> Producten { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<Student> Studenten { get; set; }
    }
    class Student
    {
        public int StudentId { get; set; }
        public string StudentNaam { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
    }

    class Country
    {
        public int CountryId { get; set; }
        public string CountryNaam { get; set; }

        public List<Student> Studenten { get; set; }
    }

    // Many-to-many
    class Film
    {
        public int FilmId { get; set; }
        public string FilmTitel { get; set; }
        public int Jaar { get; set; }

        public List<FilmActeurs> FilmActeurs { get; set; }
    }

    class Acteur
    {
        public int ActeurId { get; set; }
        public string ActeurNaam { get; set; }

        public List<FilmActeurs> FilmActeurs { get; set; }
    }

    // Join-table van Film en Acteurs
    class FilmActeurs
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }
    
        public int ActeurId { get; set; }
        public Acteur Acteur { get; set; }
    }
    
    class Product
    {
        public int ProductId { get; set; }
        public string ProductNaaam { get; set; }

        public virtual ProductDetails ProductDetails { get; set; }
    }

    class ProductDetails
    {
        [ForeignKey("Product")]
        public int ProductDetailsId { get; set; }
        public DateTime VervalDatum { get; set; }

        public virtual Product Product { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                // student die je (bijv) wilt hebben
                Student st = context.Studenten.First();
                // Explicit loaden van data voor een later tijdstip
                context.Entry(st).Reference(x => x.Country).Load(); 
                

                foreach (Student s in context.Studenten.Include(s => s.Country)) 
                {
                    Console.WriteLine(String.Format("De student {0} met id {1} woont in {2}",
                        s.StudentNaam, s.StudentId, s.Country.CountryNaam));
                }            
               
            }
        }
    }
}
