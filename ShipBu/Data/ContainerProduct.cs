using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class ContainerProduct
    {
        public Guid Id { get; set; }
      

        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Ton")]
        public float Ton { get; set; }

        [NotMapped]
        [Display(Name = "Toplam Tonaj")]
        public float TotalTon => (Quantity * Ton);

       public virtual ContainerGenre? ContainerGenre { get; set; }

        public Guid ContainerGenreId { get; set; }

        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();
        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();

    }
    public class ContainerProductEntityTypeConfiguration : IEntityTypeConfiguration<ContainerProduct>
    {
        public void Configure(EntityTypeBuilder<ContainerProduct> builder)
        {
            builder
           .HasMany(p => p.ProductFeatures)
           .WithOne(p => p.ContainerProduct)
           .HasForeignKey(p => p.ContainerProductId)
           .OnDelete(DeleteBehavior.Restrict);
            builder
           .HasMany(p => p.Locationinfos)
           .WithOne(p => p.ContainerProduct)
           .HasForeignKey(p => p.ContainerProductId)
           .OnDelete(DeleteBehavior.Restrict);
           
        }
    }

}
