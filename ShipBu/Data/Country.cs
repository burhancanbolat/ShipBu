using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; // ICollection için ekledik
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data
{
    public class Country
    {
        public Guid Id { get; set; }

        [Display(Name = "Ülke Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public string? Name { get; set; } // required kelimesini düzelttik
    
        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }

        
        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();
        public virtual ICollection<State> States { get; set; } = new HashSet<State>();
        public virtual ICollection<WareHouse> WareHouses { get; set; } = new HashSet<WareHouse>();
        public virtual ICollection<PriceEdit> PriceEdits { get; set; } = new HashSet<PriceEdit>();

    }

    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasMany(p => p.Locationinfos)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasMany(p => p.WareHouses)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.States)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
             .HasMany(p => p.PriceEdits)
             .WithOne(p => p.Country)
             .HasForeignKey(p => p.CountryId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
