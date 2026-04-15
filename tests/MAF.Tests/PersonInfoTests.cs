using MAF.SimpleAgent.StructuredApproach;
using Xunit;

namespace MAF.Tests;

public class PersonInfoTests
{
    [Fact]
    public void PersonInfo_DefaultValues_AreNull()
    {
        var person = new PersonInfo();

        Assert.Null(person.Name);
        Assert.Null(person.Age);
        Assert.Null(person.Occupation);
    }

    [Fact]
    public void PersonInfo_SetName_CanBeRetrieved()
    {
        var person = new PersonInfo { Name = "John Smith" };
        Assert.Equal("John Smith", person.Name);
    }

    [Fact]
    public void PersonInfo_SetAge_CanBeRetrieved()
    {
        var person = new PersonInfo { Age = 35 };
        Assert.Equal(35, person.Age);
    }

    [Fact]
    public void PersonInfo_SetOccupation_CanBeRetrieved()
    {
        var person = new PersonInfo { Occupation = "Software Engineer" };
        Assert.Equal("Software Engineer", person.Occupation);
    }

    [Fact]
    public void PersonInfo_AllProperties_SetAndRetrieved()
    {
        var person = new PersonInfo
        {
            Name = "Jane Doe",
            Age = 28,
            Occupation = "Data Scientist"
        };

        Assert.Equal("Jane Doe", person.Name);
        Assert.Equal(28, person.Age);
        Assert.Equal("Data Scientist", person.Occupation);
    }

    [Fact]
    public void PersonInfo_AgeIsNullable_AcceptsNull()
    {
        var person = new PersonInfo { Age = null };
        Assert.Null(person.Age);
    }
}
