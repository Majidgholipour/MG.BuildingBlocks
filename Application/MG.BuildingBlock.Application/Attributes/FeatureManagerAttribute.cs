using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace MG.BuildingBlock.Application.Attributes;

public class FeatureManagerAttribute(string featureKey) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var featureManager = (IFeatureManager)context.HttpContext.RequestServices.GetRequiredService(typeof(IFeatureManager));

        if (!await featureManager.IsEnabledAsync(featureKey))
        {
            throw new Exceptions.NotImplementedException(Resources.Messages.NotImplemented);
        }

        await next();
    }
}