using System.Data.Entity;
using System.Threading.Tasks;
using Entities;

namespace Repositories.EntitiFramework.Concrete
{
    public sealed class EfUsersRepository : EntityFrameworkRepository<Users>
    {
        public EfUsersRepository(DbContext context) : base(context)
        {
        }

        protected override void ExcludePropForUpdate(Users db)
        {
            Context.Entry(db).Property(x => x.Id).IsModified = false;
        }

        protected override Task SetRefPropestiesToUpdate(Users obj, Users db)
        {
            return Task.FromResult(false);
        }
    }
}