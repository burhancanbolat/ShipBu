using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data
{
    public class State
    {
        public Guid Id { get; set; }
     


        [Display(Name = "Eyalet Adı")]
        public string? Name { get; set; }

        [Display(Name = "Ülke Adı")]
        public Guid? CountryId { get; set; }
        public virtual Country? Country { get; set; }
		public virtual ICollection<WareHouse> WareHouses { get; set; } = new HashSet<WareHouse>();
        public virtual ICollection<PriceEdit> PriceEdits { get; set; } = new HashSet<PriceEdit>();

        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();

    }
    public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {

            builder
                .HasMany(p => p.Locationinfos)
                .WithOne(p => p.state)
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
               .HasMany(p => p.PriceEdits)
               .WithOne(p => p.State)
               .HasForeignKey(p => p.StateId)
               .OnDelete(DeleteBehavior.Restrict);
            builder
			  .HasMany(p => p.WareHouses)
			  .WithOne(p => p.State)
			  .HasForeignKey(p => p.StateId)
			  .OnDelete(DeleteBehavior.Restrict);
		}
    }
}
