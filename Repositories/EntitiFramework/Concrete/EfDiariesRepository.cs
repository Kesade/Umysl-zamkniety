using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Repositories.EntitiFramework.Concrete
{
    public sealed class EfDiariesRepository : EntityFrameworkRepository<Diaries>
    {
        public EfDiariesRepository(DbContext ctx) : base(ctx)
        {
        }


        protected override async Task SetRefPropestiesToUpdate(Diaries obj, Diaries db)
        {
            obj.Author = await Context.Set<Users>().FindAsync(db.AuthorId);
        }

        protected override void ExcludePropForUpdate(Diaries db)
        {
            Context.Entry(db).Property(x => x.Id).IsModified = false;
            Context.Entry(db).Property(x => x.AuthorId).IsModified = false;
            Context.Entry(db).Property(x => x.Timestamp).IsModified = false;
        }

        public override async Task RemoveAsync(Diaries obj)
        {
            Context.Set<Entries>()
                .RemoveRange(Context.Set<Entries>().Where(x => x.DiaryId == obj.Id));

            GetSet().Remove(obj);

            await Context.SaveChangesAsync();
        }
    }
}