using System;

namespace SponsorshipBot
{
    public static class DecimalEx
    {
        public static decimal Parse(string s)
        {
            s = s.StartsWith("£") ? s.Substring(1) : s;
            return Decimal.Parse(s);
        }

        public static decimal? ParseNullable(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            s = s.StartsWith("£") ? s.Substring(1) : s;
            return Decimal.Parse(s);
        }
    }
}
