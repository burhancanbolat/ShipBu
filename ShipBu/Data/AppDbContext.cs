using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Net;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShipBu.Data;

namespace ShipBu.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
		public virtual DbSet<SendingCountry> SendingCountries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<PriceEdit> PriceEdits { get; set; }

        public virtual DbSet<Country> Countries { get; set; } 
        public virtual DbSet<Academy> Academies { get; set; }
        public virtual DbSet<ProductFeature> ProductFeatures { get; set; }
        public virtual DbSet<BoxProduct> BoxProducts { get; set; }
        public virtual DbSet<ContainerProduct> ContainerProducts { get; set; }
        public virtual DbSet<Locationinfo> Locationinfos { get; set; }
        public virtual DbSet<PaletteProduct> PaletteProducts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Packageprocess> Packageprocesses { get; set; }
        public virtual DbSet<SendingGenre> SendingGenres { get; set; }

        public virtual DbSet<CalculationVariable> CalculationVariables { get; set; }

        public virtual DbSet<WareHouse> WareHouses { get; set; }

        public virtual DbSet<ContainerGenre> ContainerGenres { get; set; }
    }
}
