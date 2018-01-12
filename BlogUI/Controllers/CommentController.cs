using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual async Task<ActionResult> Delete(DeleteComment model)
        {
            await Service.Delete(model.Id);
            return RedirectToAction("Details","Entry",new {id =model.ParrentId});
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

    public class DeleteComment
    {
        public int ParrentId { get; set; }
        public int Id { get; set; }
        public IUserDomainEntity Author { get; set; }
    }
  
}