using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;
public class TestUsersController
{

    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // arrange
        var mockUsersService = new Mock<IUsersService>();
        var sut = new UsersController(mockUsersService.Object);
        mockUsersService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(new List<User>() 
            {
                new User() 
                { 
                    Id = 1, Name = "Rafael"
                }
            });

        //act
        var result = (ObjectResult) await sut.GetUsers();

        //assert 
        result.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Get_OnSucess_InvokesUsersServiceExactlyOnce()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.GetUsers();

        // Assert
        await Task.Factory.StartNew(
            () => mockUsersService.Verify(s => s.GetAllUsers(), Times.Once()));
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUsersService.Object);

        // act
        var result = await sut.GetUsers();

        // assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult) result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        // arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // act
        var result = await sut.GetUsers();

        // assert
        result.Should().BeOfType<NotFoundResult>();
    }
}