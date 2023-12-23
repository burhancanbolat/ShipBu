using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShipBu.Data;

namespace ShipBu.Models
{
    public class OfferSubmitModel
    {
        public List<OfferViewModel> Items { get; set; }
        public List<OfferProcessModel> SelectedProcesses { get; set; }
        public IFormFile Photo { get; set; }

    }
    //neye bağlayacaz bunu

    public class OfferProcessModel
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
    }
}
