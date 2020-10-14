using System.Collections.Generic;

namespace LinqQuiz.Library
{
    public interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        decimal Age { get; }
    }

    public interface IFamily
    {
        int ID { get; }
        IReadOnlyCollection<IPerson> Persons { get; }
    }

    public class FamilySummary
    {
        public int FamilyID { get; set; }
        public int NumberOfFamilyMembers { get; set; }
        public decimal AverageAge { get; set; }
    }
}
