using MG.BuildingBlock.Application.Bus;
using MG.BuildingBlock.Domain.Interfaces;
using MG.BuildingBlock.Domain.SeedWork;

namespace MG.BuildingBlock.Application.Features.Commands;

public class CommandHandler(IUnitOfWork uow, IInMemoryBus bus)
{
    protected async Task<bool> Commit()
    {
        return await uow.SaveChangesAsync();
        // _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
    }
    
}