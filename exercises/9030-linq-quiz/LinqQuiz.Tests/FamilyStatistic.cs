using System;
using Xunit;
using LinqQuiz.Library;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LinqQuiz.Tests
{
    public class Person : IPerson
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Age { get; set; }
    }

    public class Family : IFamily
    {
        public int ID { get; set; }

        public IPerson[] PersonArray { get; set; } = Array.Empty<IPerson>();

        public IReadOnlyCollection<IPerson> Persons => new ReadOnlyCollection<IPerson>(PersonArray);
    }


    public class FamilyStatistic
    {
        [Fact]
        public void CurrectResult()
        {
            var families = new[] {
                new Family
                {
                    ID = 1,
                    PersonArray = new []
                    {
                        new Person { FirstName = "Foo", LastName = "Bar", Age = 42 },
                        new Person { FirstName = "Mad", LastName = "Max", Age = 21 }
                    }
                },
                new Family
                {
                    ID = 2,
                    PersonArray = new []
                    {
                        new Person { FirstName = "Dead", LastName = "Pool", Age = 25 }
                    }
                }
            };

            var expectedResult = new[] {
                new FamilySummary { FamilyID = 1, NumberOfFamilyMembers = 2, AverageAge = 31.5m },
                new FamilySummary { FamilyID = 2, NumberOfFamilyMembers = 1, AverageAge = 25 }
            };

            Assert.Equal(JsonConvert.SerializeObject(expectedResult),
                JsonConvert.SerializeObject(Quiz.GetFamilyStatistic(families)));
        }

        [Fact]
        public void EmptyFamily()
        {
            var families = new[] { new Family { ID = 1, PersonArray = new Person[0] } };

            var result = Quiz.GetFamilyStatistic(families);

            Assert.Single(result);
            Assert.Equal(0, result[0].NumberOfFamilyMembers);
            Assert.Equal(0, result[0].AverageAge);
        }

        [Fact]
        public void InvalidArgument()
        {
            Assert.Throws<ArgumentNullException>(() => Quiz.GetFamilyStatistic(null!));
        }
    }
}
