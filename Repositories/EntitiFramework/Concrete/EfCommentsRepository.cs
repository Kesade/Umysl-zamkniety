using System.Data.Entity;
using System.Threading.Tasks;
using Entities;

namespace Repositories.EntitiFramework.Concrete
{
    public sealed class EfCommentsRepository : EntityFrameworkRepository<Comments>
    {
        public EfCommentsRepository(DbContext context) : base(context)
        {
        }

        protected override void ExcludePropForUpdate(Comments db)
        {
            Context.Entry(db).Property(x => x.Id).IsModified = false;
            Context.Entry(db).Property(x => x.AuthorId).IsModified = false;
            Context.Entry(db).Property(x => x.Timestamp).IsModified = false;
            Context.Entry(db).Property(x => x.EntryId).IsModified = false;
        }

        protected override async Task SetRefPropestiesToUpdate(Comments obj, Comments db)
        {
            obj.Author = await Context.Set<Users>().FindAsync(db.AuthorId);
            obj.Entry = await Context.Set<Entries>().FindAsync(db.EntryId);
        }
    }
}