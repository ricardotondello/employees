using System;
using Employee.Domain;
using EmployeeAPI.Infrastructure.DataBase.Mappers;
using FluentAssertions;
using NUnit.Framework;

namespace EmployeeAPI.Tests.Mappers
{
    [TestFixture]
    public class DomainToEntityTests
    {
        [Test]
        public void Employee_DomainToEntity_Null()
        {
            Employee.Domain.Employee domain = null;
            var result = domain.ToEntity();
            result.Should().BeNull();
        }

        [Test]
        public void Employee_DomainToEntity_NotNull()
        {
            var domain =
                Employee.Domain.Employee.Create(Guid.NewGuid(), "name", "surname", Region.Create(1, "name", null));
            var result = domain.ToEntity();
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be("name");
            result.Surname.Should().Be("surname");
            result.RegionId.Should().Be(1);
        }

        [Test]
        public void Region_DomainToEntity_Null()
        {
            Employee.Domain.Region domain = null;
            var result = domain.ToEntity();
            result.Should().BeNull();
        }

        [Test]
        public void Region_DomainToEntity_NotNull()
        {
            var domain = Employee.Domain.Region.Create(1, "name", null);
            var result = domain.ToEntity();
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("name");
        }
    }
}