using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Data
{
    public class ContainerGenre
    {
        public Guid Id { get; set; }

        [Display(Name = "Konteyner Türü")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Genre { get; set; }

        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }
        public virtual ICollection<ContainerProduct> ContainerProducts { get; set; } = new HashSet<ContainerProduct>();
    }
    public class ContainerGenreEntityTypeConfiguration : IEntityTypeConfiguration<ContainerGenre>
    {
        public void Configure(EntityTypeBuilder<ContainerGenre> builder)
        {
            builder
            .HasMany(p => p.ContainerProducts)
            .WithOne(p => p.ContainerGenre)
            .HasForeignKey(p => p.ContainerGenreId)
            .OnDelete(DeleteBehavior.Restrict);



        }
    }

}
