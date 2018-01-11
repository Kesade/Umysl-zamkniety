using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Common.DomainEntities;
using Common.RepositoryHandlers;
using Common.UI;
using DomainModel;

namespace Services
{
    public class UserService : GenericService<IUserDomainEntity>
    {
        public UserService(IRepositoryHandler<IUserDomainEntity> handler) : base(handler)
        {
        }
        public async Task Put(ICreateUser model)
        {
            await Put(new User
            {
                Login = model.Login,
                Name = model.Name,
                Password = model.Password,
                Roles = model.Roles
            });
        }

        public async Task<IUserDomainEntity> GetCurrentUser()
        {
            return await GetUser(new User {Login = HttpContext.Current.User.Identity.Name});
        }

        public async Task<IUserDomainEntity> GetUser(IUserDomainEntity user)
        {
            if (await Exists(user))
                return await EnrichUser(user);
            
            return (await Handler.GetDomainRepository()).FirstOrDefault(x => x.Name == "Guest");
        }

        private async Task<IUserDomainEntity> EnrichUser(IUserDomainEntity user)
        {
            return (await Handler.GetDomainRepository()).FirstOrDefault(x => x.Login == user.Login);
        }

        public override async Task<bool> Exists(IUserDomainEntity user)
        {
            if (user?.Login == null)
                throw new ArgumentException("User has to have login");

            return await EnrichUser(user) != null;
        }

        #region Authorization

        public async Task<IUserDomainEntity> AuthorizeUser(IUserDomainEntity user)
        {
            if (!await Exists(user))
                throw new UnauthorizedAccessException("There is no user with given login.");         
            
            var dbUser = await EnrichUser(user);

            if (dbUser == null || !IsPasswordProvidedMatchingDb(user, dbUser))
                throw new UnauthorizedAccessException("Provided password is wrong.");

            return dbUser;
        }

        private static bool IsPasswordProvidedMatchingDb(IUserDomainEntity user, IUserDomainEntity dbUser)
        {
            var hashBytes = Convert.FromBase64String(dbUser.Password);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        #endregion
    }
}