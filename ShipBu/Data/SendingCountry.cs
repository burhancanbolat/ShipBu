using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data
{
	public class SendingCountry
	{
		public Guid Id { get; set; }
		[Display(Name = "Ülke Adı")]
		[Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
		public required string Name { get; set; }

		
        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }
        public virtual ICollection<Locationinfo> Locationinfos { get; set; } = new HashSet<Locationinfo>();

    }
    public class SendingCountryEntityTypeConfiguration : IEntityTypeConfiguration<SendingCountry>
    {
        public void Configure(EntityTypeBuilder<SendingCountry> builder)
        {
			builder
				.HasMany(p => p.Locationinfos)
				.WithOne(p => p.SendingCountry)
				.HasForeignKey(p => p.SendingCountryId)
				.OnDelete(DeleteBehavior.Restrict);

        }
    }
}
