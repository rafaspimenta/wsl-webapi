using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Services;

public class TestUsersServices {
    
    [Fact]
    public async Task GetAllUsers_OnSuccess_ReturnsNonEmptyListOfUsers() 
    {
        // arrange
        var _usersService = new UsersService();
        var users = UsersFixture.GetTestUsers();
        var userCount = users.Count;
        foreach (var user in users)
        {
            await _usersService.AddUser(user);
        }

        //act
        var result = await _usersService.GetAllUsers();


        //assert
        result.Should().HaveCount(userCount);
    }

}