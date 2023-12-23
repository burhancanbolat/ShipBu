using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Models
{
	public class BoxProductModel
	{
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

    }
}
