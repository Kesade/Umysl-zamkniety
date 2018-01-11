using System.Web.Mvc;
using Common.DomainEntities;
using Common.Services;

namespace BlogUI.Controllers
{
    public class AdministrateController : GenericController<IDiaryDomainEntity>
    {
        private readonly IService<ICommentDomainEntity> _comments;
        private readonly IService<IEntryDomainEntity> _entries;
        private readonly IService<IUserDomainEntity> _users;
        public AdministrateController(IService<ICommentDomainEntity> comments, IService<IEntryDomainEntity> entries, IService<IUserDomainEntity> users,
            IService<IDiaryDomainEntity> service) : base(service)
        {
            _comments = comments;
            _entries = entries;
            _users = users;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }


     
    }
}