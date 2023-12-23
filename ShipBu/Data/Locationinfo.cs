using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class Locationinfo
    {
        public Guid Id { get; set; }
        [Display(Name = "Açık Adres")]
        public string? Adress { get; set; }

        [Display(Name = "Adı-Soyadı")]
        public string? CommunicationName { get; set; }

        [Display(Name = "Telefon")]
        public string? TelephoneNumber { get; set; }
        
        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }

        public Guid? CountryId { get; set; }
        public Guid? BoxProductId { get; set; }
        public Guid? PaletteProductId { get; set; }
        public Guid? ContainerProductId { get; set; }
		public Guid? SendingCountryId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? WareHouseId { get; set; }

        public Guid? ProductFeatureId { get; set; }

        public virtual State? state { get; set; }
        public virtual BoxProduct? BoxProduct { get; set; }
        public virtual Country? Country { get; set; }
		public virtual SendingCountry? SendingCountry { get; set; }
        public virtual WareHouse? WareHouse { get; set; }
        public virtual PaletteProduct? PaletteProduct { get; set; }
        public virtual ContainerProduct? ContainerProduct { get; set; }
        public virtual ProductFeature? ProductFeature { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
    public class LocationinfoEntityTypeConfiguration : IEntityTypeConfiguration<Locationinfo>
    {
        public void Configure(EntityTypeBuilder<Locationinfo> builder)
        {
            builder
                     .HasMany(p => p.Payments)
                     .WithOne(p => p.locationInfo)
                     .HasForeignKey(p => p.LocationInfoId)
                     .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
