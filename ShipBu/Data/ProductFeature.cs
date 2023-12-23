using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipBu.Data
{
    public class ProductFeature
    {
        public Guid Id { get; set; }
        public Guid? BoxProductId { get; set; }
        public Guid? PaletteProductId { get; set; }
        public Guid? ContainerProductId { get; set; }

        public Guid? PackageprocessId { get; set; }

        [Display(Name = "Görsel")]
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
        [Display(Name = "PDF DOSYASI")]
        public string? Pdf { get; set; }
        [NotMapped]
        public IFormFile? PdfFile { get; set; }
        public  string? Value { get; set; }

        public bool IsCheckedWareHouse{ get; set; } = false;

        public virtual BoxProduct? BoxProduct { get; set; }
        public virtual PaletteProduct? PaletteProduct { get; set; }
        public virtual ContainerProduct? ContainerProduct { get; set; }
        public virtual Packageprocess? Packageprocess { get; set; }
        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();

        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid? UserId { get; set; }

    }
    public class ProductFeatureEntityTypeConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder
            .HasMany(p => p.Locationinfos)
            .WithOne(p => p.ProductFeature)
            .HasForeignKey(p => p.ProductFeatureId)
            .OnDelete(DeleteBehavior.Restrict);
            


        }
    }

}
