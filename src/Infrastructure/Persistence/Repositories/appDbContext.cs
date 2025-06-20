
namespace Infrastructure.Persistence.Repositories
{
    [Serializable]
    internal class appDbContext : Exception
    {
        public appDbContext()
        {
        }

        public appDbContext(string? message) : base(message)
        {
        }

        public appDbContext(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}