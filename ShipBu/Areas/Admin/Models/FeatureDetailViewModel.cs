using ShipBu.Data;

namespace ShipBu.Areas.Admin.Models
{
    public class FeatureDetailViewModel
    {
        public List<string> Features { get; set; }
        public List<decimal?> FeaturePrices { get; set; }
        public List< AppUser?> Users { get; set; }
        public float TotalWeight { get; set; }
        public float TotalDesi { get; set; }
        public int Quantity { get; set; }
        public float Variable { get; set; }
        public float Result { get; set; }
        public string CountryName { get; set; }
        public decimal? CountryPrice { get; set; }
        public string? StateName { get; set; }
        public decimal? StatePrice { get; set; }
        public string? wareHouseName { get; set; }
        public decimal? wareHousePrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public List< SendingGenre?> SendingGenre { get; set; }

        public List<Payment?> PaymentInfo { get; set; }

       
    }

}
