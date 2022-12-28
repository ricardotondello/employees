using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Application.Interfaces.Repositories;
using Employee.Infrastructure.DataBase;
using Employee.Infrastructure.DataBase.Repository;
using Employees.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;

namespace Employee.Tests.Infrastructure;

[TestFixture]
public class RegionRepositoryTests
{
    private static IRegionRepository _regionRepository;
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
        
        _regionRepository = new RegionRepository(_dataBaseCtx);
    }
    
    [Test]
    public async Task WhenRegionExistsAndGetByIdIsCalled_ShouldReturnSome()
    {
        //Arrange
        await _dataBaseCtx.Regions.AddAsync(new Region(1, "region 1", null));
        await _dataBaseCtx.Regions.AddAsync(new Region(2, "region 2", 1));
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var maybeRegion = await _regionRepository.GetByIdAsync(2);

        //Assert
        maybeRegion.IsSome().Should().BeTrue();
        maybeRegion.Value().Id.Should().Be(2);
        maybeRegion.Value().Name.Should().Be("region 2");
        maybeRegion.Value().Parent.Should().NotBeNull();
        maybeRegion.Value().Parent.Id.Should().Be(1);
        maybeRegion.Value().Parent.Name.Should().Be("region 1");
    }
    
    [Test]
    public async Task WhenRegionDoesntExistsAndGetByIdIsCalled_ShouldReturnNone()
    {
        //Arrange
        
        //Act
        var maybeRegion = await _regionRepository.GetByIdAsync(99);

        //Assert
        maybeRegion.IsSome().Should().BeFalse();
    }
    
    [Test]
    public async Task WhenGetAllAsyncIsCalled_ShouldReturn()
    {
        //Arrange
        await _dataBaseCtx.Regions.AddAsync(new Region(1, "region 1", null));
        await _dataBaseCtx.Regions.AddAsync(new Region(2, "region 2", 1));
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var regions = (await _regionRepository.GetAllAsync()).ToList();

        //Assert
        regions.Should().HaveCount(2);
        regions.Should().BeEquivalentTo(
            new List<Employee.Domain.Region>()
            {
                Domain.Region.Create(1, "region 1", null),
                Domain.Region.Create(2, "region 2", Domain.Region.Create(1, "region 1", null))
            });
    }
    
    [Test]
    public async Task WhenCreatingNewRegion_ShouldReturnSome()
    {
        //Arrange
        var region = Domain.Region.Create(1, "region 1", null);
        
        //Act
        var maybeRegion = await _regionRepository.CreateRegionAsync(region);

        //Assert
        maybeRegion.IsSome().Should().BeTrue();
        maybeRegion.Value().Should().BeEquivalentTo(Domain.Region.Create(1, "region 1", null));
    }
    
    [Test]
    public async Task WhenCreatingExistingRegion_ShouldReturnSome()
    {
        //Arrange
        var region = Domain.Region.Create(1, "region 1", null);
        await _dataBaseCtx.Regions.AddAsync(new Region(1, "region 1", null));
        await _dataBaseCtx.SaveChangesAsync();
        
        //Act
        var maybeRegion = await _regionRepository.CreateRegionAsync(region);

        //Assert
        maybeRegion.IsSome().Should().BeTrue();
        maybeRegion.Value().Should().BeEquivalentTo(Domain.Region.Create(1, "region 1", null));
    }
}