using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data;

public class WareHouse
{
    public Guid Id { get; set; }
    [Display(Name = "Amazon Deposu Adı")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public required string Name { get; set; }
   

    public virtual AppUser? CreaterUser { get; set; }

    [Display(Name = "Kullanıcı Adı")]
    public Guid UserId { get; set; }

    public Guid? CountryId { get; set; }
	public Guid? StateId { get; set; }
	public virtual State? State { get; set; }
	public virtual Country?  Country { get; set; }
    public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();
    public virtual ICollection<PriceEdit> PriceEdits { get; set; } = new HashSet<PriceEdit>();

}

public class WareHouseyEntityTypeConfiguration : IEntityTypeConfiguration<WareHouse>
{
    public void Configure(EntityTypeBuilder<WareHouse> builder)
    {
        builder
                      .HasMany(p => p.Locationinfos)
                      .WithOne(p => p.WareHouse)
                      .HasForeignKey(p => p.WareHouseId)
                      .OnDelete(DeleteBehavior.Restrict);
        builder
                            .HasMany(p => p.PriceEdits)
                            .WithOne(p => p.WareHouse)
                            .HasForeignKey(p => p.WareHouseId)
                            .OnDelete(DeleteBehavior.Restrict);

    }
}