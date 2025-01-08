using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Seed
{
    public static class CitiesSeed
    {
        public static void Seed(DatabaseContext context)
        {
            if (!context.Cities.Any(x => x.Name == "Mostar"))
            {
                context.Cities.Add(new City
                {
                    Name = "Mostar",
                    DisplayName = "Mostar",
                    PostalCode = 88000,
                    CountryId = 1
                });
            }
            context.SaveChanges();
        }
    }
}
