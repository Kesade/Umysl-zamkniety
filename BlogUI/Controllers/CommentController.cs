using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogUI.Models;
using Common.DomainEntities;
using Common.Services;
using Services;

namespace BlogUI.Controllers
{
    public class CommentController : GenericController<ICommentDomainEntity>
    {
        public CommentController(IService<ICommentDomainEntity> service) : base(service)
        {
        }


        [ChildActionOnly]
        public virtual ActionResult Create(CreateComment model)
        {
            return PartialView(model);
        }


        [ChildActionOnly]
        public virtual async Task<ActionResult> Get(int id)
        {
            return PartialView("Comments", (await ((CommentService) Service).GetByParrent(id)).ToList());
        }
    }
}