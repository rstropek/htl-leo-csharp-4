using System;
using Xunit;
using LinqQuiz.Library;

namespace LinqQuiz.Tests
{
    public class TextStatistic
    {
        [Fact]
        public void CurrectResult()
        {
            var expectedResult = new (char letter, int numberOfOccurrences)[] { ('A', 1), ('B', 2), ('Z', 1) };
            Assert.Equal(expectedResult, Quiz.GetLetterStatistic("ABBZ"));
        }

        [Fact]
        public void WhitespacesIgnored()
        {
            var expectedResult = new(char letter, int numberOfOccurrences)[] { ('A', 1) };
            Assert.Equal(expectedResult, Quiz.GetLetterStatistic(" \nA\t"));
        }

        [Fact]
        public void EmptyResult()
        {
            Assert.Empty(Quiz.GetLetterStatistic("-1'"));
        }
    }
}
