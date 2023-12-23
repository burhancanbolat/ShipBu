using ShipBu.Data;

namespace ShipBu.Areas.Admin.Models
{
    public class PriceEditModel
    {
        public List<WareHouse>? wareHouses { get; set; }

        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid WareHouseId { get; set; }
        public List<Country>? Country { get; set; }
        public List<State> States { get; set; } = new List<State>();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PrimaryKg { get; set; }
        public int SecondaryKg { get; set; }
        public List<SendingGenre>? sendingGenres { get; set; }

        public List<Guid>? SelectedIdsCountry { get; set; }
        public List<Guid>? SelectedIdsState { get; set; }
        public List<Guid>? SelectedIdsWareHouse { get; set; }
        public List<Guid>? SelectedIdsGenre { get; set; }

        // Diğer özellikler...
    }
}
