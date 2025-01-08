using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Seed
{
    public static class CountriesSeed
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
            }
            context.SaveChanges();
        }
    }
}
