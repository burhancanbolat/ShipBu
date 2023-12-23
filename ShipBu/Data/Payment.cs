using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShipBu.Data;

public class Payment
{
    public Guid Id { get; set; }
    public virtual AppUser? CreaterUser { get; set; }

    [Display(Name = "Kullanıcı Adı")]
    public Guid UserId { get; set; }
  
    public Guid? ProductFeatureId { get; set; }
    public virtual ProductFeature? ProductFeature { get; set; }

    public virtual Locationinfo? locationInfo { get; set; }
    public Guid LocationInfoId { get; set; }
    public Guid? CalculationVariableId { get; set; }
    public virtual CalculationVariable? CalculationVariable { get; set; }
    public Guid? SendingGenreId { get; set; }
    public virtual SendingGenre? SendingGenre { get; set; }
    public Guid? PriceEditId { get; set; }
    public virtual PriceEdit? PriceEdit { get; set; }



}
public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {


       
    }
}
