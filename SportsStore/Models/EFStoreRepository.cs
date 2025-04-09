namespace SportsStore.Models
{
    public class EFStoreRepository(StoreDbContext dbContext) : IStoreRepository
    {
        private readonly StoreDbContext _dbContext = dbContext;
        public IQueryable<Product> Products => _dbContext.Products;
    }
}
