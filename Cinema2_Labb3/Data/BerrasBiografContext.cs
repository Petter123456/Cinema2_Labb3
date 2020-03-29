using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Cinema_Labb3.Models;

namespace Cinema_Labb3.Models
{
    public partial class BerrasBiografContext : DbContext
    {
        public BerrasBiografContext()
        {
        }

        public BerrasBiografContext(DbContextOptions<BerrasBiografContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaccinoSalon> PaccinoSalon { get; set; }
        public virtual DbSet<DeNiroSalon> DeNiroSalon { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Cinema4;Trusted_Connection=True;");
            }
        }
        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            for (int i = 1; i <= 150; i++)
            {
                modelBuilder.Entity<PaccinoSalon>().HasData(

                    new PaccinoSalon
                    {
                        Id = i,
                        Availible = true,
                        
                    }
                );
            };

            for (int i = 1; i <= 300; i++)
            {
               
                modelBuilder.Entity<DeNiroSalon>().HasData(

                    new DeNiroSalon
                    {
                        Id = i,
                        Availible = true,
                    }
                );
            };

            modelBuilder.Entity<Movies>().HasData(
               new Movies
               {
                   Id = 2,
                   Name = "Scarface",
                   Price = 160,
                   Image = ReadFile("Images/scarface.jpg"),
                   Time = "16.00",
                   Salon = "Paccino-Salon"
                   //  Salon = "Paccino-Salon"

               },
               new Movies
               {
                   Id = 3,
                   Name = "Scarface",
                   Price = 160,
                   Image = ReadFile("Images/scarface.jpg"),
                   Time = "19.00",
                   Salon = "Paccino-Salon"
                   //  Salon = "Paccino-Salon"

               },
               new Movies
               {
                   Id = 4,
                   Name = "Scarface",
                   Price = 160,
                   Image = ReadFile("Images/scarface.jpg"),
                   Time = "21.30",
                   Salon = "Paccino-Salon"
                   //  Salon = "Paccino-Salon"

               },
              
               new Movies
               {
                   Id = 5,
                   Name = "The-GodFather-Part-2",
                   Price = 170,
                   Image = ReadFile("Images/theGodFatherTwo.jpg"),
                   Time = "16.00",
                   Salon = "De-Niro-Salon"
                   // Salon = "De-Niro-Salon"

               },
                new Movies
                {
                    Id = 6,
                    Name = "The-GodFather-Part-2",
                    Price = 170,
                    Image = ReadFile("Images/theGodFatherTwo.jpg"),
                    Time = "19.00",
                    Salon = "De-Niro-Salon"
                    // Salon = "De-Niro-Salon"

                },
                 new Movies
                 {
                     Id = 25,
                     Name = "The-GodFather-Part-2",
                     Price = 170,
                     Image = ReadFile("Images/theGodFatherTwo.jpg"),
                     Time = "21.30",
                     Salon = "De-Niro-Salon"
                     // Salon = "De-Niro-Salon"

                 }
           );





            modelBuilder.Entity<Orders>().HasData(
                    new Orders
                    {
                        Id = Guid.NewGuid(),
                        Name = "petter fagerlund",
                        Email = "petter.fagerlund@gmail.com",
                        CreditCardNumber = "XXXXXXXX",
                        ExpiryDate = "xx/xx",
                        Cvc = "XXX",
                        NumberOfSeats = 2,
                        TotalPrice = 400,
                        Movie = "scarface",

                    }
                );

            modelBuilder.Entity<PaccinoSalon>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Availible).HasColumnType("bit");


            });

            modelBuilder.Entity<DeNiroSalon>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Availible).HasColumnType("bit");


            });


            modelBuilder.Entity<Movies>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreditCardNumber)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Cvc)
                    .HasColumnName("CVC")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ExpiryDate)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
