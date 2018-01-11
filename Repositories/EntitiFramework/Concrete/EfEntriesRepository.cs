using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Repositories.EntitiFramework.Concrete
{
    public sealed class EfEntriesRepository : EntityFrameworkRepository<Entries>
    {
        public EfEntriesRepository(DbContext context) : base(context)
        {
        }

        public override async Task RemoveAsync(Entries obj)
        {
            Context.Set<Comments>()
                .RemoveRange(Context.Set<Comments>().Where(x => x.EntryId == obj.Id));
            GetSet().Remove(obj);

            await Context.SaveChangesAsync();
        }

        protected override async Task SetRefPropestiesToUpdate(Entries obj, Entries db)
        {
            obj.Author = await Context.Set<Users>().FindAsync(db.AuthorId);
            obj.Diary = await Context.Set<Diaries>().FindAsync(db.DiaryId);
        }

        protected override void ExcludePropForUpdate(Entries db)
        {
            Context.Entry(db).Property(x => x.Id).IsModified = false;
            Context.Entry(db).Property(x => x.AuthorId).IsModified = false;
            Context.Entry(db).Property(x => x.Timestamp).IsModified = false;
            Context.Entry(db).Property(x => x.DiaryId).IsModified = false;
        }
    }
}