using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediLabo.Patient.Database.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Domain.Patient>
{
    public void Configure(EntityTypeBuilder<Domain.Patient> builder)
    {
        builder.Property(p => p.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Address).HasMaxLength(128);
        builder.Property(p => p.PhoneNumber).HasMaxLength(32);
    }
}