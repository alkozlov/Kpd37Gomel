namespace Kpd37Gomel.DataAccess.IServices.Implementation
{
    public abstract class BaseService
    {
        protected ApplicationDbContext Context { get; }

        protected BaseService(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}