using System;
using Employee.Domain;
using EmployeeAPI.Mapper;
using FluentAssertions;
using NUnit.Framework;

namespace EmployeeAPI.Tests.Mappers
{
    [TestFixture]
    public class DomainToContractTest
    {
        [Test]
        public void Employee_ToContract_Null()
        {
            Employee.Domain.Employee domain = null!;
            var result = domain.ToContract();
            result.Should().BeNull();
        }

        [Test]
        public void Employee_ToContract_NotNull()
        {
            var domain = Employee.Domain.Employee.Create(Guid.NewGuid(), "name", "surname", Region.Create(1));
            var result = domain.ToContract();
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be("name");
            result.Surname.Should().Be("surname");
            result.Region.Should().NotBeNull();
            result.Region.Id.Should().Be(1);
        }

        [Test]
        public void Region_ToContract_Null()
        {
            Employee.Domain.Region domain = null!;
            var result = domain.ToContract();
            result.Should().BeNull();
        }

        [Test]
        public void Region_ToContract_NotNull()
        {
            var domain = Employee.Domain.Region.Create(1, "name", null!);
            var result = domain.ToContract();
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("name");
        }
    }
}