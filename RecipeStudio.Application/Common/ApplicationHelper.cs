using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Common
{
    internal static class ApplicationHelper
    {
        public static string NormalizeSpaces(this string value)
        {
            return string.Join(
                ' ',
                value.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

    }
}
