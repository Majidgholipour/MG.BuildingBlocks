using MG.BuildingBlock.Domain.Events;
using MG.BuildingBlock.Infra.EF.EntityConfiguration.Applicator;
using Microsoft.EntityFrameworkCore;

namespace MG.BuildingBlock.Infra.EF.Context;

public abstract class BaseContext : DbContext
{
    public BaseContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyAllConfigurations(GetType());
        base.OnModelCreating(modelBuilder);
    }
}