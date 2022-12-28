using System;
using Employee.Infrastructure.DataBase.Mappers;
using FluentAssertions;
using NUnit.Framework;

namespace Employee.Tests.Mappers;

[TestFixture]
public class EntityToDomainTests
{
    [Test]
    public void Employee_EntityToDomain_Null()
    {
        Employees.Entities.Employee entity = null!;
        var result = entity.ToDomain();
        result.Should().BeNull();
    }

    [Test]
    public void Employee_EntityToDomain_NotNull()
    {
        var entity = new Employees.Entities.Employee(Guid.NewGuid(), "name", "surname", 1);
        var result = entity.ToDomain();
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("name");
        result.Surname.Should().Be("surname");
    }

    [Test]
    public void Region_EntityToDomain_Null()
    {
        Employees.Entities.Region entity = null!;
        var result = entity.ToDomain();
        result.Should().BeNull();
    }

    [Test]
    public void Region_EntityToDomain_NotNull()
    {
        var entity = new Employees.Entities.Region(1, "name", 1);
        var result = entity.ToDomain();
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("name");
    }
}