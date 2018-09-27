namespace ModPanel.Utilities
{
    using System.Linq;

    public static class StringUtillities
    {
        public static string Capitalize(this string input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            var first = input.First();
            var rest = input.Substring(1);

            return $"{char.ToUpper(first)}{rest}";
        }
    }
}