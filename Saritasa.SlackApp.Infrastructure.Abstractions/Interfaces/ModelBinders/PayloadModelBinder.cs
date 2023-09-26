using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.ModelBinders;

/// <summary>
/// Home payload model binder.
/// </summary>
public class PayloadModelBinder<T> : IModelBinder
{
    /// <inheritdoc />
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;
            if (!string.IsNullOrEmpty(value))
            {
                var result = JsonConvert.DeserializeObject<T>(value);
                bindingContext.Result = ModelBindingResult.Success(result);
            }
        }

        return Task.CompletedTask;
    }
}
