using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture {
    public static IList<User> GetTestUsers() 
    {
        var users = new List<User>() 
        {
            new User() 
            { 
                Name = "Rafael",
                Email = "rafael@gmail.com",
                Address = new Address()
                {
                    Street = "Pio IX",
                    City = "Betim",
                    ZipCode = "32620"
                }

            },
            new User() 
            { 
                Name = "Lucas",
                Email = "lucas@gmail.com",
                Address = new Address()
                {
                    Street = "Alenquer",
                    City = "Betim",
                    ZipCode = "31222"
                }
            },
            new User() 
            { 
                Name = "Daniel",
                Email = "daniel@gmail.com",
                Address = new Address()
                {
                    Street = "Rio de Janeiro",
                    City = "Belo Horizonte",
                    ZipCode = "33333"
                }
            }
        };

        return users;
    }
}

