using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipBu.Data
{
    public class PriceEdit
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public Guid? UserId { get; set; }
        public virtual AppUser? CreaterUser { get; set; }
        public Guid? CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public Guid? StateId { get; set; }
        public virtual State? State { get; set; }
        public Guid? WareHouseId { get; set; }
        public virtual WareHouse? WareHouse { get; set; }
        public Guid? SendingGenreId { get; set; }
        public virtual SendingGenre? SendingGenre { get; set; }
        public int PrimaryKG { get; set; }
        public int SecondaryKg { get; set; }

        [NotMapped]
        public string KG
        {
            get
            {
                return $"{PrimaryKG} kg - {SecondaryKg} kg";
            }
        }

        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

    }
    public class PriceEditTypeConfiguration : IEntityTypeConfiguration<PriceEdit>
    {
        public void Configure(EntityTypeBuilder<PriceEdit> builder)
        {

            builder
               .HasMany(p => p.Payments)
               .WithOne(p => p.PriceEdit)
               .HasForeignKey(p => p.PriceEditId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
