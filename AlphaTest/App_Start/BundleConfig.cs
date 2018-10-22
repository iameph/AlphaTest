using System.Web.Optimization;

namespace AlphaTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/css/bootstrap.min.css"));
        }
    }
}