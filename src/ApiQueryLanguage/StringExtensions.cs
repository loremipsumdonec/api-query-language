// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text;

namespace ApiQueryLanguage
{
    public static class StringExtensions
    {
        public static IEnumerable<string> AdvancedSplit(
            this string value,
            char separator,
            char? onlyAfter = null,
            char quote = '"',
            char? endQuote = null,
            bool keepQuote = false,
            StringSplitOptions options = StringSplitOptions.None
        )
        {
            List<string> splitted = new();

            if (string.IsNullOrEmpty(value))
            {
                return splitted;
            }

            endQuote ??= quote;
            bool insideQuotes = false;
            int depth = 0;
            var word = new StringBuilder();

            for (int index = 0; index < value.Length; index++)
            {
                char? prev = index > 0 ? value[index - 1] : null;
                char current = value[index];

                if (current == quote && depth == 0)
                {
                    insideQuotes = true;
                    depth++;

                    if (keepQuote)
                    {
                        word.Append(current);
                    }

                    continue;
                }
                else if (current == endQuote && depth == 1)
                {
                    insideQuotes = false;
                    depth--;

                    if (keepQuote)
                    {
                        word.Append(current);
                    }

                    continue;
                }
                else if (current == quote)
                {
                    depth++;
                }
                else if (current == endQuote)
                {
                    depth--;
                }

                if (
                    (
                        (current == separator && onlyAfter.HasValue && onlyAfter == prev)
                        || (current == separator && onlyAfter == null)
                    )
                    && !insideQuotes)
                {
                    splitted.Add(word.ToString());
                    word.Clear();
                }
                else
                {
                    word.Append(current);
                }
            }

            splitted.Add(word.ToString());
            splitted = ApplyStringOptions(options, splitted);

            return splitted;
        }

        private static List<string> ApplyStringOptions(StringSplitOptions options, List<string> splitted)
        {
            if (options.HasFlag(StringSplitOptions.TrimEntries))
            {
                splitted = splitted.Select(s => s.Trim()).ToList();
            }

            if (options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
            {
                splitted = splitted.Where(s => !string.IsNullOrEmpty(s)).ToList();
            }

            return splitted;
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }

        public static string ToLowerFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            char[] letters = source.ToCharArray();
            letters[0] = char.ToLower(letters[0]);

            return new string(letters);
        }
    }
}
