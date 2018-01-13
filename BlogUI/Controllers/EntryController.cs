using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogUI.Models;
using Common.DomainEntities;
using Common.Services;
using Common.UI;
using DomainModel;
using Services;

namespace BlogUI.Controllers
{
    public class EntryController : GenericController<IEntryDomainEntity>, IPageable<IEntryDomainEntity>
    {
        private readonly IService<ICommentDomainEntity> _comments;
        private readonly IService<IDiaryDomainEntity> _diary;
        private readonly IService<IUserDomainEntity> _users;

        public EntryController(IService<ICommentDomainEntity> comments, IService<IEntryDomainEntity> service, IService<IUserDomainEntity> users,
            IService<IDiaryDomainEntity> diary) : base(service)
        {
            _comments = comments;
            _users = users;
            _diary = diary;
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(int diaryId = 1)
        {
            return View(new CreateEntry {ParrentId = diaryId});
        }

        [HttpPost]
        //[ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(CreateEntry model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Body = model.Body.Replace("\n", "");
            model.Diary = await _diary.GetById(model.ParrentId);
            model.Author = await ((UserService) _users).GetCurrentUser();
            await ((EntryService) Service).Put(model);

            
            return RedirectToAction("OkResult", new { message = $"Post {model.Title} was successfully posted."});
        }

        [HttpGet]
        public virtual async Task<ActionResult> Details(int id)
        {
            var entry = (Entry) await Service.GetById(id);
            return View(new DisplayEntry
            {
                Entry = entry,
                NewComment = new CreateComment {ParrentId = entry.Id}
            });
        }

        [HttpGet]
        public ActionResult GetEntries(int diaryId, int page = 0)
        {
            return View(Task.Run(async () => await GetPagedSet(diaryId, page)).Result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateComment(CreateComment model)
        {
            //if (!ValidateReCaptcha())
            //{
            //    var entry = await Service.GetById(model.ParrentId);
            //    model.Entry = entry;

            //    return View(@"~\Views\Entry\Details.cshtml", new DisplayEntry
            //    {
            //        Entry = entry,
            //        NewComment = model

            //    });
            //}
            model.Author = await ((UserService)_users).GetCurrentUser();
            model.Entry = await Service.GetById(model.ParrentId);
            await ((CommentService)_comments).Put(model);

            return RedirectToAction("Details", "Entry", new {id = model.ParrentId});
        }

        #region Paging

        public int PageSize { get; } = 3;

        public async Task<IPaged<IEntryDomainEntity>> GetPagedSet(int diaryId, int page)
        {
            var set = await Service.GetByParrent(diaryId);

            return new PagedEntries
            {
                NextUrl = GetNextPageUrl(diaryId, page, GetPageNumbers(set)),
                PrevUrl = GetPrevPageUrl(diaryId, page),
                Content = GetSetPage(page, set).ToList()
            };
        }

        private double GetPageNumbers(IQueryable<IEntryDomainEntity> set)
        {
            var totalCount = set.Count();
            var totalPages = Math.Ceiling((double) totalCount / PageSize);
            return totalPages;
        }

        private string GetPrevPageUrl(int diaryId, int page)
        {
            return page > 0 ? Url.Action("Index", "Home", new {diaryId, page = page - 1}, Request.Url.Scheme) : "";
        }

        private string GetNextPageUrl(int diaryId, int page, double totalPages)
        {
            return page < totalPages - 1
                ? Url.Action("Index", "Home", new {diaryId, page = page + 1}, Request.Url.Scheme)
                : "";
        }

        private IEnumerable<IEntryDomainEntity> GetSetPage(int page, IQueryable<IEntryDomainEntity> set)
        {
            return set.OrderByDescending(x => x.Timestamp)
                .Skip(PageSize * page)
                .Take(PageSize).ToList();
        }

        #endregion
    }
}