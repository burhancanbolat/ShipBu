using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShipBu.Data;

namespace ShipBu.Models
{
    public class PackageViewModel
    {
        public Guid? BoxProductId { get; set; }

        public Guid? PackageprocessId { get; set; }
    }

}
