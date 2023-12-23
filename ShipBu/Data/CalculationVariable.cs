using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data
{
    public class CalculationVariable
    {
        public Guid  Id { get; set; }
        [Display(Name = "Ürün Fiyat Hesaplama Değeri")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public float? Variable { get; set; }
        [Display(Name = "Adrese Gönderim Fiyatı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public decimal? PrivateAdressPrice { get; set; }
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

        

    }
    public class CalculationVariableEntityTypeConfiguration : IEntityTypeConfiguration<CalculationVariable>
    {
        public void Configure(EntityTypeBuilder<CalculationVariable> builder)
        {


            builder
                .HasMany(p => p.Payments)
                .WithOne(p => p.CalculationVariable)
                .HasForeignKey(p => p.CalculationVariableId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
