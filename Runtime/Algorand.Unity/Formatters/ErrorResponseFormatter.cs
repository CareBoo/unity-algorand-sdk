using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public class ErrorResponseFormatter : IAlgoApiFormatter<ErrorResponse>
    {
        private const string MessageKey = "message";
        private const string DataKey = "data";
        private const string IsErrorKey = "error";
        private const string CodeKey = "statusCode";

        public ErrorResponse Deserialize(ref JsonReader reader)
        {
            ErrorResponse result = default;
            if (!reader.TryRead(JsonToken.ObjectBegin))
                return result;

            var stringFormatter = AlgoApiFormatterCache<string>.Formatter;

            while (reader.Peek() != JsonToken.ObjectEnd
                && reader.Peek() != JsonToken.None
                && !FoundAllFields(result))
            {
                var key = stringFormatter.Deserialize(ref reader);
                switch (key)
                {
                    case CodeKey:
                        result.Code = AlgoApiFormatterCache<int>.Formatter.Deserialize(ref reader);
                        break;
                    case MessageKey:
                        result.Message = stringFormatter.Deserialize(ref reader);
                        break;
                    case DataKey:
                        result.Data = stringFormatter.Deserialize(ref reader);
                        break;
                    case IsErrorKey:
                        reader.Skip();
                        break;
                    default:
                        throw new ArgumentException(
                            $"Found unexpected key when deserializing {nameof(ErrorResponse)}: {key}",
                            nameof(reader)
                        );
                }
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader);
            return result;
        }

        public ErrorResponse Deserialize(ref MessagePackReader reader)
        {
            ErrorResponse result = default;
            var stringFormatter = AlgoApiFormatterCache<string>.Formatter;
            var count = reader.ReadMapHeader();
            for (var i = 0; i < count; i++)
            {
                var key = stringFormatter.Deserialize(ref reader);
                switch (key)
                {
                    case CodeKey:
                        result.Code = AlgoApiFormatterCache<int>.Formatter.Deserialize(ref reader);
                        break;
                    case MessageKey:
                        result.Message = stringFormatter.Deserialize(ref reader);
                        break;
                    case DataKey:
                        result.Data = stringFormatter.Deserialize(ref reader);
                        break;
                    case IsErrorKey:
                        reader.Skip();
                        break;
                    default:
                        throw new ArgumentException(
                            $"Found unexpected key when deserializing {nameof(ErrorResponse)}: {key}",
                            nameof(reader)
                        );
                }
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, ErrorResponse value)
        {
            throw new NotSupportedException($"{nameof(ErrorResponse)} is read only");
        }

        public void Serialize(ref MessagePackWriter writer, ErrorResponse value)
        {
            throw new NotSupportedException($"{nameof(ErrorResponse)} is read only");
        }

        private bool FoundAllFields(ErrorResponse error)
        {
            return error.Message != null && error.Data != null;
        }
    }
}
