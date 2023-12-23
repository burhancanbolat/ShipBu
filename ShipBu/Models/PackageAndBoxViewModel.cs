using ShipBu.Data;
using ShipBu.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.ViewModels
{
    public class PackageAndBoxViewModel
    {
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
        public List<Packageprocess>? PackageProcesses { get; set; }
		public virtual AppUser? CreaterUser { get; set; }

		[Display(Name = "Kullanıcı Adı")]
		public Guid? UserId { get; set; }
		[Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Ağırlık")]
        public float Weight { get; set; }
        [NotMapped]
        [Display(Name = "Toplam Ağırlık")]
        public float TotalWeight => (Quantity * Weight);

        [Display(Name = "En")]
        public float Width { get; set; }

        [Display(Name = "Boy")]
        public float Length { get; set; }

        [Display(Name = "Yükseklik")]
        public float Height { get; set; }

        [Display(Name = "Ürün Adedi")]
        public int ProductCount { get; set; }
        public ProductFeature? ProductFeature { get; set; }
        public List<Guid>? SelectedProcessIds { get; set; }
        public List<Guid>? ProductFeatureIds { get; set; }
        public ContainerProduct? ContainerProduct { get; set; }
        public List< ContainerGenre>? ContainerGenre { get; set; }
        public PaletteProduct? PaletteProduct { get; set; }

        public List<Guid>? SelectedGenreIds { get; set; }
    }
}
