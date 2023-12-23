using ShipBu.Data;


namespace ShipBu.Models
{
    public class LocationInfoModel
    {
        public Locationinfo? locationInfo { get; set; }
        public List<SendingCountry>? SendingCountry { get; set; }
        public List<Country>? Country { get; set; }
        public List<State> States { get; set; } = new List<State>();
        public List<Guid>? SelectedIds { get; set; }
        public List<Guid>? SelectedIdsCountry { get; set; }
        public List<Guid>? SelectedIdsState { get; set; }
        public List<Guid>? SelectedIdsWareHouse { get; set; }
        public Guid? BoxId { get; set; }
        public Guid? PaletteId { get; set; }
        public Guid? PFId { get; set; }
        public Guid? PFPaletteId { get; set; }
        public Guid? ContainerId { get; set; }
        public List<Packageprocess>? Packageprocesses { get; set; } = new List<Packageprocess>();






    }
}
