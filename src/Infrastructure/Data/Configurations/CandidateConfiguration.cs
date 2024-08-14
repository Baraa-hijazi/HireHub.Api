using HireHub.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireHub.Api.Infrastructure.Data.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.Property(t => t.FirstName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(t => t.LinkedInUrl)
            .HasMaxLength(500);

        builder.Property(t => t.GitHubUrl)
            .HasMaxLength(500);

        builder.Property(t => t.Notes)
            .HasMaxLength(1000);
    }
}
