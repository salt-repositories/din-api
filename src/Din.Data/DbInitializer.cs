
namespace Din.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DinContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
