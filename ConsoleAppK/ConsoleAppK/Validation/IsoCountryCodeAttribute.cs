using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConsoleAppK.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IsoCountryCodeAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> Codes = new HashSet<string>();

        static IsoCountryCodeAttribute()
        {
            // CultureInfo use rfc4646
            foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    RegionInfo rf = new RegionInfo(item.LCID);
                    Codes.Add(rf.TwoLetterISORegionName);
                }
                catch { }
            }
        }

        public override bool IsValid(object value)
        {
            if (value is string s)
            {
                if (s.Length != 2)
                {
                    return false;
                }

                if (Codes.Contains(s))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
