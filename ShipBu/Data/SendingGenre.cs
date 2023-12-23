using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShipBu.Data;

namespace ShipBu.Data;

public class SendingGenre
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? DeliveryTıme { get; set; }
    public Guid UserId { get; set; }
    public virtual AppUser CreaterUser { get; set; }

    public virtual ICollection<PriceEdit> PriceEdits { get; set; } = new HashSet<PriceEdit>();

    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();



}
public class SendingGenreoEntityTypeConfiguration : IEntityTypeConfiguration<SendingGenre>
{
public void Configure(EntityTypeBuilder<SendingGenre> builder)
{
       
        builder
                .HasMany(p => p.PriceEdits)
                .WithOne(p => p.SendingGenre)
                .HasForeignKey(p => p.SendingGenreId)
                .OnDelete(DeleteBehavior.Restrict);
        builder
                .HasMany(p => p.Payments)
                .WithOne(p => p.SendingGenre)
                .HasForeignKey(p => p.SendingGenreId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}


