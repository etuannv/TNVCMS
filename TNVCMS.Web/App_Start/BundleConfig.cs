using System.Web;
using System.Web.Optimization;

namespace TNVCMS.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-2.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Content/themes/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                        "~/Content/themes/plugins/datatables/jquery.dataTables.js",
                        "~/Content/themes/plugins/datatables/dataTables.bootstrap.js",
                        "~/Content/themes/plugins/slimScroll/jquery.slimscroll.min.js",
                        "~/Content/themes/plugins/fastclick/fastclick.js",
                        "~/Content/themes/dist/js/app.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                        "~/ckeditor/ckeditor.js",
                        "~/ckfinder/ckfinder.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/themes/jquery-ui-1.11.4/jquery-ui.css "));

            bundles.Add(new StyleBundle("~/Css/bootstrap").Include("~/Content/themes/bootstrap/css/bootstrap.css"));


            bundles.Add(new StyleBundle("~/Css/AdminStyle").Include(
                        "~/Content/themes/dist/css/AdminLTE.css",
                        "~/Content/themes/dist/css/skins/skin-blue.css",
                        "~/Content/themes/plugins/datatables/dataTables.bootstrap.css"
                        ));

            bundles.Add(new StyleBundle("~/Contents/themes/base").Include(
                       "~/Content/themes/base/jquery-ui*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/tagsinput").Include(
                "~/Content/themes/plugin/bootstrap-tagsinput/bootstrap-tagsinput.css"
                ));
            
            
        }
    }
}