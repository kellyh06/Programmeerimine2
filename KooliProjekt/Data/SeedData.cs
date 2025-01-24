namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.Artists.Any())
            {
                return; // Don't seed if there are already artists in the database
            }

            var list = new Artist
            {
                Name = "Artist 1"
            };

            // Add the list (which contains the artist) to the context
            context.Artists.Add(list);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}