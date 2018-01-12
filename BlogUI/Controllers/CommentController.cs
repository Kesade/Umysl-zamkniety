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
    public static class CustomExtensions
    {
        public static MvcHtmlString HiddenFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression);
        }

        public static MvcHtmlString HiddenFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression, htmlAttributes);
        }

        public static MvcHtmlString HiddenFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression, htmlAttributes);
        }

        private static void ReplacePropertyState<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string text = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(text);
            ModelStateDictionary modelState = htmlHelper.ViewContext.ViewData.ModelState;
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (modelState.ContainsKey(fullName))
            {
                ValueProviderResult currentValue = modelState[fullName].Value;
                modelState[fullName].Value = new ValueProviderResult(metadata.Model, Convert.ToString(metadata.Model), currentValue.Culture);
            }
            else
            {
                modelState[fullName] = new ModelState
                {
                    Value = new ValueProviderResult(metadata.Model, Convert.ToString(metadata.Model), CultureInfo.CurrentUICulture)
                };
            }
        }
    }
}