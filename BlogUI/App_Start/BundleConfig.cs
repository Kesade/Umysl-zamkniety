using System.Web.Optimization;

namespace BlogUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
            //-{version}.js

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/facebook").Include(
                "~/Scripts/facebook.js"));

            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
                "~/Scripts/autosize.min.js",
                "~/Scripts/clean-blog.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/clean-blog.css",
                "~/Content/Site.css",
                "~/Content/CodeHighlight/obsidian.css"
                //HighlitghC:\Users\Konrad\Source\Repos\Umysl-zamkniety\BlogUI\Content\

                //
                ));
        }
    }
}