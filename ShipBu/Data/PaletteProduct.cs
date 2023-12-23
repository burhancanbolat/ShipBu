using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class PaletteProduct
    {
        public Guid Id { get; set; }

        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Ağırlık")]
        public float Weight { get; set; }

        [NotMapped]
        [Display(Name = "Toplam Ağırlık")]
        public float TotalWeight => (Quantity * Weight);

        [Display(Name = "En")]
        public float Width { get; set; }

        [Display(Name = "Boy")]
        public float Length { get; set; }

        [Display(Name = "Yükseklik")]
        public float Height { get; set; }





        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();
        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();

    }
    public class PaletteProductEntityTypeConfiguration : IEntityTypeConfiguration<PaletteProduct>
    {
        public void Configure(EntityTypeBuilder<PaletteProduct> builder)
        {
            builder
           .HasMany(p => p.ProductFeatures)
           .WithOne(p => p.PaletteProduct)
           .HasForeignKey(p => p.PaletteProductId)
           .OnDelete(DeleteBehavior.Restrict);
            builder
     .HasMany(p => p.Locationinfos)
     .WithOne(p => p.PaletteProduct)
     .HasForeignKey(p => p.PaletteProductId)
     .OnDelete(DeleteBehavior.Restrict);
     
        }
    }

}
