using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DoePaAdmin
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
