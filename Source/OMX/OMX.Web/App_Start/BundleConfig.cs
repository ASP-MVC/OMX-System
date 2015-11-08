namespace OMX.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterBundleScripts(bundles);
            RegisterBudleCss(bundles);
        }

        private static void RegisterBudleCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.flatly.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                        "~/Content/kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/kendo.black.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                        "~/Content/site.css"));
        }

        private static void RegisterBundleScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.web.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/noty")
                .IncludeDirectory("~/Scripts/noty", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/custom/scripts.js").Include(
                        "~/Scripts/custom/ajaxHelper.js").Include(
                        "~/Scripts/custom/notificationHelper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}