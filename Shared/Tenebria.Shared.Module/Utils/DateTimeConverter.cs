using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tenebria.Shared.Module.Utils;

public sealed class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly TimeZoneInfo _appTz;

    private const string Format = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

    public DateTimeConverter(TimeZoneInfo appTz)
    {
        _appTz = appTz ?? throw new ArgumentNullException(nameof(appTz));
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var s = reader.GetString()!;
            var dto = DateTimeOffset.Parse(
                s,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);

            var converted = TimeZoneInfo.ConvertTime(dto, _appTz);

            return DateTime.SpecifyKind(converted.DateTime, DateTimeKind.Unspecified);
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            var ms = reader.GetInt64();
            var dto = DateTimeOffset.FromUnixTimeMilliseconds(ms);

            var converted = TimeZoneInfo.ConvertTime(dto, _appTz);

            return DateTime.SpecifyKind(converted.DateTime, DateTimeKind.Unspecified);
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Value = business time (Unspecified)
        var offset = _appTz.GetUtcOffset(value);

        var dto = new DateTimeOffset(value, offset);

        var text = dto.ToString(Format, CultureInfo.InvariantCulture);

        writer.WriteStringValue(text);
    }
}

// Nullable converter
public sealed class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    private readonly DateTimeConverter _inner;

    public NullableDateTimeConverter(TimeZoneInfo appTz)
    {
        _inner = new DateTimeConverter(appTz);
    }

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        return _inner.Read(ref reader, typeof(DateTime), options);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteNullValue();
            return;
        }

        _inner.Write(writer, value.Value, options);
    }
}
