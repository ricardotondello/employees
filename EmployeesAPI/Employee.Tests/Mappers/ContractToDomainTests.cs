using EmployeeAPI.Mapper;
using FluentAssertions;
using NUnit.Framework;

namespace Employee.Tests.Mappers;

[TestFixture]
public class ContractToDomainTests
{
    [Test]
    public void Employee_ToDomain_Null()
    {
        Employee.Contracts.Input.Employee contract = null!;
        var result = contract.ToDomain();
        result.Should().BeNull();
    }

    [Test]
    public void Employee_ToDomain_NotNull()
    {
        var contract = Employee.Contracts.Input.Employee.Create("name", "surname", 1);
        var result = contract.ToDomain();
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("name");
        result.Surname.Should().Be("surname");
    }

    [Test]
    public void Region_ToDomain_Null()
    {
        Employee.Contracts.Input.Region contract = null!;
        var result = contract.ToDomain();
        result.Should().BeNull();
    }

    [Test]
    public void Region_ToDomain_NotNull()
    {
        var contract = Employee.Contracts.Input.Region.Create(1, "name", 1);
        var result = contract.ToDomain();
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("name");
    }
}