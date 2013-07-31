using System.Web.Optimization;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;

namespace AntwerpRC.UI.Umbraco.Helpers
{
    public class RegisterEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarting(umbracoApplication, applicationContext);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
//            Document.BeforePublish += Document_BeforePublish;
        }

        private void Document_BeforePublish(Document sender, PublishEventArgs e)
        {
            //Do what you need to do. In this case logging to the Umbraco log
            Log.Add(LogTypes.Debug, sender.Id, "the document " + sender.Text + " is about to be published");

            //cancel the publishing if you want.
            e.Cancel = true;
        }
    }
}