using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class Academy
    {
        public Guid Id { get; set; }

        [Display(Name = "Video Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]

        public required string Name { get; set; }

        [Display(Name = "Video ")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Video { get; set; }

        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }
    }
    public class AcademyFeatureEntityTypeConfiguration : IEntityTypeConfiguration<Academy>
    {
        public void Configure(EntityTypeBuilder<Academy> builder)
        {

        }

    }
}
