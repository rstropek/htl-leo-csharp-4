using System;
using Xunit;
using LinqQuiz.Library;

namespace LinqQuiz.Tests
{
    public class EvenNumbers
    {
        [Fact]
        public void CurrectResult()
        {
            Assert.Equal(new[] { 2, 4, 6, 8 }, Quiz.GetEvenNumbers(10));
        }

        [Fact]
        public void EmptyResult()
        {
            Assert.Empty(Quiz.GetEvenNumbers(1));
        }

        [Fact]
        public void InvalidArgument()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Quiz.GetEvenNumbers(0));
        }
    }
}
