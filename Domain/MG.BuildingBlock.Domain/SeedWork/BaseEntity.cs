using MG.BuildingBlock.Domain.Models;

namespace MG.BuildingBlock.Domain.SeedWork;

public class BaseEntity<TPrimaryKey> : Entity
{
    public TPrimaryKey Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public Guid CreatorUserId { get; set; }
}