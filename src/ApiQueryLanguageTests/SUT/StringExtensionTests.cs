using ApiQueryLanguage;

namespace ApiQueryLanguageTests.SUT
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_WithSeperator_ReturnsExpectedString(char seperator)
        {
            string[] expected = new string[] { "a", "b", "c" };

            var splitted = string.Join(seperator, expected)
                .AdvancedSplit(seperator);

            Assert.Equal(expected, splitted.ToArray());
        }

        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_WithStartingWhiteSpace_StringIsNotTrimmed(char seperator)
        {
            string[] expected = new string[] { "a", "b", " c" };

            var splitted = string.Join(seperator, expected)
                .AdvancedSplit(seperator);

            Assert.Equal(expected, splitted.ToArray());
        }

        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_WithStringSplitOptionsTrimeEntries_EntriesTrimmed(char seperator)
        {
            string[] input = new string[] { "a", "b", " c" };

            var splitted = string.Join(seperator, input)
                .AdvancedSplit(seperator, options: System.StringSplitOptions.TrimEntries);

            Assert.Equal(
                new string[] { input[0], input[1], input[2].Trim() },
                splitted.ToArray()
            );
        }

        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_WithEmptyEntry_EmptyEntryNotRemoved(char seperator)
        {
            string[] expected = new string[] { "a", "b", "" };

            var splitted = string.Join(seperator, expected)
                .AdvancedSplit(seperator);

            Assert.Equal(expected, splitted.ToArray());
        }

        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_WithStringSplitOptionsRemoveEmptyEntries_EmptyEntryRemoved(char seperator)
        {
            string[] input = new string[] { "a", "b", "" };

            var splitted = string.Join(seperator, input)
                .AdvancedSplit(seperator, options: System.StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(
                new string[] { input[0], input[1] },
                splitted.ToArray()
            );
        }

        [Theory]
        [InlineData(',')]
        [InlineData(';')]
        public void AdvancedSplit_SeperatorInsideQuotedString_StringSplitted(char seperator)
        {
            string[] expected = new string[] { "a", "b", $"\"c{seperator}d\"" };

            var splitted = string.Join(seperator, expected)
                .AdvancedSplit(seperator);

            expected[2] = $"c{seperator}d";
            Assert.Equal(expected, splitted.ToArray());
        }

        [Fact]
        public void AdvancedSplit_WithOnlyAfter_StringSplitted()
        {
            string input = "(a,b),(c,d)";
            string expected = "(a,b);(c,d)";

            var splitted = input
                .AdvancedSplit(',', onlyAfter: ')', options: StringSplitOptions.TrimEntries);

            Assert.Equal(
                expected,
                string.Join(";", splitted)
            );
        }

        [Fact]
        public void AdvancedSplit_WithOnlyAfterAndInQuotes_StringSplitted()
        {
            string input = "\"(a,b),\"(c,d),(e,f)";
            string expected = "(a,b),(c,d);(e,f)";

            var splitted = input
                .AdvancedSplit(',', onlyAfter: ')', options: StringSplitOptions.TrimEntries);

            Assert.Equal(
                expected,
                string.Join(";", splitted)
            );
        }

        [Fact]
        public void AdvancedSplit_WithEndQuote_StringSplitted()
        {
            string input = "((a,b),(c,d)),((e,f),(g,h))";
            string expected = "(a,b),(c,d);(e,f),(g,h)";

            var splitted = input
                .AdvancedSplit(
                    ',',
                    quote: '(',
                    endQuote: ')',
                    options: StringSplitOptions.TrimEntries
                );

            Assert.Equal(
                expected,
                string.Join(";", splitted)
            );
        }

        [Theory]
        [InlineData("\"a,b\",\"c,d\"", "\"a,b\";\"c,d\"")]
        [InlineData("a\"b\",c\"d\"", "a\"b\";c\"d\"")]
        public void AdvancedSplit_WithKeepQuoteTrueString_SplittedHasQuote(string input, string expected)
        {
            var splitted = input
                .AdvancedSplit(
                    ',',
                    keepQuote: true,
                    options: StringSplitOptions.TrimEntries
                );

            Assert.Equal(
                expected,
                string.Join(";", splitted)
            );
        }
    }
}
