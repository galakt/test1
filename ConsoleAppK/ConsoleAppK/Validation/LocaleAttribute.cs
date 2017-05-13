using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class LocaleAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> Locales = new HashSet<string>();

        static LocaleAttribute()
        {
            // CultureInfo use rfc4646 to construct
            foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    RegionInfo rf = new RegionInfo(item.LCID);
                    Locales.Add(item.TwoLetterISOLanguageName);
                    Locales.Add(item.TwoLetterISOLanguageName + "-" + rf.TwoLetterISORegionName);
                }
                catch { }
            }
        }

        public override bool IsValid(object value)
        {
            if (value is string s)
            {
                if (Locales.Contains(s))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
