using ShipBu.Data;

namespace ShipBu.Areas.Admin.Models
{
    public class WareHouseModel
    {
        public List<WareHouse>? wareHouses { get; set; }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public List<Country>? Country { get; set; }
        public List<State> States { get; set; } = new List<State>();
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<Guid>? SelectedIdsCountry { get; set; }
        public List<Guid>? SelectedIdsState { get; set; }

        // Diğer özellikler...
    }
}
