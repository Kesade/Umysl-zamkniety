using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Helpers
{
    public static class UIExtensions
    {
        public static MvcHtmlString HiddenForExt<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression);
        }

        public static MvcHtmlString HiddenForExt<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression, htmlAttributes);
        }

        public static MvcHtmlString HiddenForExt<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ReplacePropertyState(htmlHelper, expression);
            return htmlHelper.HiddenFor(expression, htmlAttributes);
        }

        private static void ReplacePropertyState<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            string text = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(text);
            ModelStateDictionary modelState = htmlHelper.ViewContext.ViewData.ModelState;
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (modelState.ContainsKey(fullName))
            {
                ValueProviderResult currentValue = modelState[fullName].Value;
                modelState[fullName].Value = new ValueProviderResult(metadata.Model, Convert.ToString(metadata.Model),
                    currentValue.Culture);
            }
            else
            {
                modelState[fullName] = new ModelState
                {
                    Value = new ValueProviderResult(metadata.Model, Convert.ToString(metadata.Model),
                        CultureInfo.CurrentUICulture)
                };
            }
        }
    }
}