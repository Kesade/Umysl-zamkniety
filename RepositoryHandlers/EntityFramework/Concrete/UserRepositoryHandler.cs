using System;
using System.Threading.Tasks;
using Common.DomainEntities;
using Common.Repositories;
using Common.StorageEntities;
using DomainModel.Extensions;
using Entities;
using Entities.Extensions;

namespace RepositoryHandlers.EntityFramework.Concrete
{
    public class UserRepositoryHandler : RepositoryHandler<IUserDomainEntity, Users>
    {
        public UserRepositoryHandler(IRepository<Users> repository) : base(repository)
        {
            Repository = repository;
        }

        private IRepository<Users> Repository { get; }

        public override IUserDomainEntity ConvertToDomain(IStorageEntity obj)
        {
            return ((IUserEntity) obj).ConvertToDomain();
        }

        protected override async Task<IUserDomainEntity> UpdateAuthor(IUserDomainEntity toUpdate)
        {
            return toUpdate;
        }

        public override IStorageEntity ConvertToEntity(IUserDomainEntity entities)
        {
            return entities.ConvertToEntity();
        }
    }
}