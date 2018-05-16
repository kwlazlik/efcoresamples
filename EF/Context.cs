using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF
{
   public class Context : DbContext
   {
      public DbSet<Package> Packages { get; set; }

     // public DbSet<Box> Boxes { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);

         optionsBuilder
            .UseSqlServer(@"Server=komputerek\sqlexpress;Database=testdb;Trusted_Connection=True;")
            .ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.QueryClientEvaluationWarning))
            .EnableSensitiveDataLogging()
            ;
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<Package>(etb =>
         {
            etb.Property(p => p.Test).UsePropertyAccessMode(PropertyAccessMode.Property);
            etb.OwnsOne(x => x.Status).Property(ps => ps.Value).IsRequired();
            etb.OwnsOne(p => p.Number).UsePropertyAccessMode(PropertyAccessMode.Field).Property(pn => pn.Value);
         });

//         modelBuilder.Entity<Box>(builder =>
//         {
//            builder.OwnsEnumeration(p => p.Status).IsRequired();
//            builder.OwnsOne(b => b.Cid).Property(c => c.Value).HasColumnName(nameof(Box.Cid));
//
//           // builder.Property(b => b.Cid).HasConversion(cid => cid.Value, str => CidNumber.New(str).Value);
//           // builder.Property(b => b.Cid).HasConversion<string>();
//         });

//         modelBuilder.Owned<CidNumber>();
//         modelBuilder.Owned<PackageStatus>();
//         modelBuilder.Owned<BoxStatus>();
      }
   }
}