using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? InvoiceAdress { get; set; }
        public string? Adress { get; set; }

        public virtual ICollection<Country> Countries { get; set; } = new HashSet<Country>();

        public virtual ICollection<Academy> Academies { get; set; } = new HashSet<Academy>();
        public virtual ICollection<Packageprocess> Packageprocesses { get; set; } = new HashSet<Packageprocess>();
        public virtual ICollection<BoxProduct> BoxProducts { get; set; } = new HashSet<BoxProduct>();
        public virtual ICollection<ContainerProduct> ContainerProducts { get; set; } = new HashSet<ContainerProduct>();
        public virtual ICollection<PaletteProduct> PaletteProducts { get; set; } = new HashSet<PaletteProduct>();

        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

        public virtual ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();
    public virtual ICollection<PriceEdit> PriceEdits { get; set; } = new HashSet<PriceEdit>();

        public virtual ICollection<WareHouse> WareHouses { get; set; } = new HashSet<WareHouse>();
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();
        public virtual ICollection<ContainerGenre> ContainerGenres { get; set; } = new HashSet<ContainerGenre>();
        public virtual ICollection<SendingGenre> SendingGenres { get; set; } = new HashSet<SendingGenre>();

    }
    public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
           .HasIndex(p => new { p.Name })
           .IsUnique(false);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(450);


            builder
         .HasMany(p => p.Academies)
         .WithOne(p => p.CreaterUser)
         .HasForeignKey(p => p.UserId)
         .OnDelete(DeleteBehavior.Restrict);

            builder
        .HasMany(p => p.ContainerProducts)
        .WithOne(p => p.CreaterUser)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);
            builder
      .HasMany(p => p.BoxProducts)
      .WithOne(p => p.CreaterUser)
      .HasForeignKey(p => p.UserId)
      .OnDelete(DeleteBehavior.Restrict);
            builder
        .HasMany(p => p.Locationinfos)
        .WithOne(p => p.CreaterUser)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            builder
        .HasMany(p => p.PaletteProducts)
        .WithOne(p => p.CreaterUser)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);
            builder
        .HasMany(p => p.Payments)
        .WithOne(p => p.CreaterUser)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            builder
        .HasMany(p => p.Shipments)
        .WithOne(p => p.CreaterUser)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            builder
   .HasMany(p => p.Packageprocesses)
   .WithOne(p => p.CreaterUser)
   .HasForeignKey(p => p.UserId)
   .OnDelete(DeleteBehavior.Restrict);
            builder
  .HasMany(p => p.Countries)
  .WithOne(p => p.CreaterUser)
  .HasForeignKey(p => p.UserId)
  .OnDelete(DeleteBehavior.Restrict);
            builder
 .HasMany(p => p.WareHouses)
 .WithOne(p => p.CreaterUser)
 .HasForeignKey(p => p.UserId)
 .OnDelete(DeleteBehavior.Restrict);
            builder
.HasMany(p => p.ProductFeatures)
.WithOne(p => p.CreaterUser)
.HasForeignKey(p => p.UserId)
.OnDelete(DeleteBehavior.Restrict);
            builder
.HasMany(p => p.ContainerGenres)
.WithOne(p => p.CreaterUser)
.HasForeignKey(p => p.UserId)
.OnDelete(DeleteBehavior.Restrict);
            builder
.HasMany(p => p.SendingGenres)
.WithOne(p => p.CreaterUser)
.HasForeignKey(p => p.UserId)
.OnDelete(DeleteBehavior.Restrict);
            builder
.HasMany(p => p.PriceEdits)
.WithOne(p => p.CreaterUser)
.HasForeignKey(p => p.UserId)
.OnDelete(DeleteBehavior.Restrict);
        }
    }

}
