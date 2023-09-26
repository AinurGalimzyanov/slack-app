using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saritasa.SlackApp.Infrastructure.Abstractions.Converters;

/// <summary>
/// Takes the first element in the array.
/// </summary>
/// <typeparam name="T">Type of work.</typeparam>
public class FirstElementArrayConverter<T> : JsonConverter<T>
{
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        var jsonArray = new JArray
        {
            value
        };

        writer.WriteRawValue(JsonConvert.SerializeObject(jsonArray));
    }

    /// <inheritdoc />
    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonArray = JArray.Load(reader);

        var firstElement = jsonArray.FirstOrDefault();

        return firstElement is null
            ? default
            : firstElement.ToObject<T>();
    }
}
