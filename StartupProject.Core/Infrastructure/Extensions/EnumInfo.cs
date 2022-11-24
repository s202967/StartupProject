using StartupProject.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupProject.Core.Infrastructure.Extensions
{
    public static class EnumInfo
    {
        public static List<DropdownListItem> GetSelectItemList<TEnum>()
        where TEnum : struct, IConvertible, IFormattable
        {
            return ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(v => new DropdownListItem
            {
                Text = v.ToString(),
                Value = v.ToString("d", null)
            }).ToList();
        }
    }
}
