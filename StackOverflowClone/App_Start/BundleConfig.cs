using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StackOverflowClone
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include( "~/Scripts/bootstrap.js", "~/Scripts/jquery-3.6.0.js", "~/Scripts/umd/popper.js"));

            ////bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Content/Styles.css"));
            BundleTable.EnableOptimizations = true;
        }

    }
}