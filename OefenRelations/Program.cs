using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OefenRelations
{
    class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestRelations;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Student> Studenten { get; set; }
    }
    class Student
    {
        public int StudentId { get; set; }
        public string StudentNaam { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    class Country
    {
        public int CountryId { get; set; }
        public string CountryNaam { get; set; }

        public List<Student> Studenten { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                Student st = context.Studenten.First();
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
