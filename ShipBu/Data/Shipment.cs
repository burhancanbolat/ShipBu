using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ShipBu.Data
{
    public class Shipment
    {
        public Guid Id { get; set; }

        [Display(Name = "Sevkiyat Numarası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string ShipmentNo { get; set; }

        [Display(Name = "Talep Tarihi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public DateTime Date { get; set; }= DateTime.Now;

        [Display(Name = "Gönderici Ülke")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string SenderCountry { get; set; }

        [Display(Name = "Varış Ülkesi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string RecipientCountry { get; set; }

        [Display(Name = "Alıcı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string Buyer { get; set;}

       
        public decimal Price { get; set; }

        [NotMapped]
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [RegularExpression(@"^[0-9.]+(,[0-9]{2})?", ErrorMessage = "Lütfen geçerli bir fiyat yazınız!")]
        public required string PriceText { get; set; }



        [Display(Name = "Takip Numarası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string TrackingId { get; set; }

        [Display(Name = "Takip Durumu")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string RequestStatu { get; set; }

        [Display(Name = "Statü")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public required string TrackingStatu { get; set;}

        public virtual AppUser? CreaterUser { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public Guid UserId { get; set; }
    }
}
