using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;

using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data;

public class Packageprocess
{
    public Guid Id { get; set; }
    public string Feature { get; set; }
    public string Description { get; set; }
    [Display(Name = "Özelliğiniz Resim içeriyor mu")]
    public bool IsChecked { get; set; } = false;
    [Display(Name = "Özelliğiniz Etiket içeriyor mu")]
    public bool IsCheckedPdf { get; set; } = false;
    [Display(Name = "Eğer eklenecek  özellik 'Ürününüz fabrikadan teslim alınsın mı ?' ise burayı seçiniz  ")]
    public bool IsCheckedWare { get; set; } = false;
    public decimal? Price { get; set; }


    public virtual AppUser? CreaterUser { get; set; }

    [Display(Name = "Kullanıcı Adı")]
    public virtual Guid UserId { get; set; }
    public virtual ICollection<ProductFeature> ProductFeatures { get; set; } = new HashSet<ProductFeature>();








}
public class PackageprocessEntityTypeConfiguration : IEntityTypeConfiguration<Packageprocess>
{
    public void Configure(EntityTypeBuilder<Packageprocess> builder)
    {
       

        builder
            .HasMany(p => p.ProductFeatures)
            .WithOne(p => p.Packageprocess)
            .HasForeignKey(p => p.PackageprocessId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
