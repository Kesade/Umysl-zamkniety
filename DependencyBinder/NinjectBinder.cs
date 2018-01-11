using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Repositories;
using Common.RepositoryHandlers;
using Common.Services;
using Contexts.Contexts;
using DomainModel;
using Entities;
using Ninject;
using Ninject.Web.Common;
using Repositories.EntitiFramework.Concrete;
using RepositoryHandlers.EntityFramework.Concrete;
using Services;

namespace DependencyBinder
{
    public static class NinjectBinder
    {
        public static IKernel GetKernel(IKernel kernel)
        {
            kernel.Settings.AllowNullInjection = false;

            kernel.Bind<DbContext>().To<DiaryDbContext>().InRequestScope();
            //kernel.Bind<IHttpActionSelector>().To<HybridActionSelector>();

            kernel.Bind<ICommentDomainEntity>().To<Comment>();
            kernel.Bind<IEntryDomainEntity>().To<Entry>();
            kernel.Bind<IDiaryDomainEntity>().To<Diary>();
            kernel.Bind<IUserDomainEntity>().To<User>();

            kernel.Bind<IRepository<Diaries>>().To<EfDiariesRepository>();
            kernel.Bind<IRepository<Comments>>().To<EfCommentsRepository>();
            kernel.Bind<IRepository<Users>>().To<EfUsersRepository>();
            kernel.Bind<IRepository<Entries>>().To<EfEntriesRepository>();

            kernel.Bind<IRepositoryHandler<IDiaryDomainEntity>>().To<DiaryRepositoryHandler>();
            kernel.Bind<IRepositoryHandler<IUserDomainEntity>>().To<UserRepositoryHandler>();
            kernel.Bind<IRepositoryHandler<IEntryDomainEntity>>().To<EntryRepositoryHandler>();
            kernel.Bind<IRepositoryHandler<ICommentDomainEntity>>().To<CommentRepositoryHandler>();


            kernel.Bind<IService<IEntryDomainEntity>>().To<EntryService>();
            kernel.Bind<IService<IUserDomainEntity>>().To<UserService>();
            kernel.Bind<IService<ICommentDomainEntity>>().To<CommentService>();
            kernel.Bind<IService<IDiaryDomainEntity>>().To<DiaryService>();

            return kernel;
        }
    }
}

