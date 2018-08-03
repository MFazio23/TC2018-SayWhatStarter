using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SayWhatStarterWebhook.Extensions
{
    public static class StringExtensions
    {
        public static bool Equivalent(this string s, string o)
        {
            return (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(o)) || (s?.ToLower()?.Equals(o?.ToLower()) ?? false);
        }
    }
}