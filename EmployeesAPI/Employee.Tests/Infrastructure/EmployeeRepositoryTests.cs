using System;
using System.Linq;
using System.Threading.Tasks;
using Employee.Application.Interfaces.Repositories;
using Employee.Infrastructure.DataBase;
using Employee.Infrastructure.DataBase.Mappers;
using Employee.Infrastructure.DataBase.Repository;
using Employees.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using EmployeeEntity = Employees.Entities.Employee;

namespace Employee.Tests.Infrastructure;

[TestFixture]
public class EmployeeRepositoryTests
{
    private static IEmployeeRepository _employeeRepository;
    private static DataBaseCtx _dataBaseCtx;
    
    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<DataBaseCtx>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        _dataBaseCtx = new DataBaseCtx(options);
        
        await _dataBaseCtx.Database.EnsureCreatedAsync();
        
        _employeeRepository = new EmployeeRepository(_dataBaseCtx);
    }
    
    [Test]
    public async Task WhenEmployeeExistsAndGetByIdIsCalled_ShouldReturnSome()
    {
        //Arrange
        var employee = new EmployeeEntity(Guid.NewGuid(), "name", "surname", 1);
        
        await _dataBaseCtx.Regions.AddAsync(new Region(1, "region 1", null));
        await _dataBaseCtx.Employees.AddAsync(employee);
        
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var maybeResult = await _employeeRepository.GetByIdAsync(employee.Id);

        //Assert
        maybeResult.IsSome().Should().BeTrue();
        maybeResult.Value().Should().BeEquivalentTo(employee.ToDomain());
    }
    
    [Test]
    public async Task WhenEmployeeDoesntExistsAndGetByIdIsCalled_ShouldReturnNone()
    {
        //Arrange
        
        //Act
        var maybeResult = await _employeeRepository.GetByIdAsync(Guid.NewGuid());

        //Assert
        maybeResult.IsSome().Should().BeFalse();
    }
    
    [Test]
    public async Task WhenGetAllAsyncIsCalled_ShouldReturn()
    {
        //Arrange
        await _dataBaseCtx.Employees.AddAsync(new EmployeeEntity(Guid.NewGuid(), "name 1", "surname 1", null));
        await _dataBaseCtx.Employees.AddAsync(new EmployeeEntity(Guid.NewGuid(), "name 2", "surname 2", null));
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var employees = (await _employeeRepository.GetAllAsync()).ToList();

        //Assert
        employees.Should().HaveCount(2);
    }
    
    [Test]
    public async Task WhenCreatingNewEmployee_ShouldReturnSome()
    {
        //Arrange
        var region = new Region(1, "region 1", null);
        await _dataBaseCtx.Regions.AddAsync(region);
        var domainRegion = region.ToDomain();
        var employee = Domain.Employee.Create(Guid.NewGuid(), "name 1", "surname", domainRegion);
        
        //Act
        var maybeEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

        //Assert
        maybeEmployee.IsSome().Should().BeTrue();
        maybeEmployee.Value().Should().BeEquivalentTo(Domain.Employee.Create(employee.Id, "name 1", "surname", domainRegion));
    }
    
    [Test]
    public async Task WhenCreatingExistingEmployee_ShouldReturnSome()
    {
        //Arrange
        var region = new Region(1, "region 1", null);
        await _dataBaseCtx.Regions.AddAsync(region);
        await _dataBaseCtx.SaveChangesAsync();
        
        var employeeEntity = new EmployeeEntity(Guid.NewGuid(), "name 1", "surname 1", region.Id);
        var employee = Domain.Employee.Create(employeeEntity.Id, "name 1", "surname 1", region.ToDomain());
        await _dataBaseCtx.Employees.AddAsync(employeeEntity);
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var maybeEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

        //Assert
        maybeEmployee.IsSome().Should().BeTrue();
        maybeEmployee.Value().Should().BeEquivalentTo(employee);
    }
    
    [Test]
    public async Task WhenGetEmployeesByRegionAsync_ShouldReturnNoneWhenRegionDoestExists()
    {
        //Arrange
        
        //Act
        var employees = await _employeeRepository.GetEmployeesByRegionAsync(2);

        //Assert
        employees.Should().BeEmpty();
    }

}