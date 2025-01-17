using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository
{
    public static class DatabaseSeed
    {
        public static void Seed(DatabaseContext context)
        {
            if (!context.Countries.Any(x => x.Name == "Bosnia and Herzegovina"))
            {
                context.Countries.Add(new Country
                {
                    Name = "Bosnia and Herzegovina",
                    DisplayName = "BiH"
                });
                context.SaveChanges();
            }

            var country = context.Countries.FirstOrDefault(x => x.Name == "Bosnia and Herzegovina");
            if (country != null)
            {
                if (!context.Cities.Any(x => x.Name == "Mostar"))
                {
                    context.Cities.Add(new City
                    {
                        Name = "Mostar",
                        DisplayName = "Mostar",
                        PostalCode = 88000,
                        CountryId = country.Id
                    });
                    context.SaveChanges();
                }
            }

            var city = context.Cities.FirstOrDefault(x => x.Name == "Mostar");
            if (city != null)
            {
                if (!context.Users.Any(x => x.FirstName == "Super"))
                {
                    context.Users.Add(new User
                    {
                        FirstName = "Super",
                        LastName = "Admin",
                        Email = "admin@mail.com",
                        PasswordHash = "E4C5BD8E1F4BADE6838CB526C8C4EC4DB23260CFD1C733FC264DA83D144CC6C60A597BDEFA17546F6B7A8589E6EEF09D349E99F9523B3060F27649198B8FAD5C",
                        PasswordSalt = "bL8ADAXR14RW3OVbEYMrntagUg+IIJzBbQHC0K53uVs+dKrLKwPGLhwFfm5GUDxz+oQ7hqye1r46TJn9cwzK0A==",
                        Gender = Core.Enums.Gender.Male,
                        BirthDate = new DateTime(2002, 8, 15),
                        Phone = "063123456",
                        Address = "nn",
                        Role = Core.Enums.Role.SuperAdministrator,
                        IsActive = true,
                        CityId = city.Id
                    });
                    context.SaveChanges();
                }
            }

        }
    }
}
