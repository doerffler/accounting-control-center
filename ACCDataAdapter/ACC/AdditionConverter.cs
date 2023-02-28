using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Text.Json;

namespace ACCDataAdapter.ACC
{
    public class AdditionConverter : ValueConverter<Dictionary<string, string>, string>
    {

        public AdditionConverter() : base(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null))
        {
        }

    }
}
