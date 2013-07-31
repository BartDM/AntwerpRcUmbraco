using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AntwerpRC.UI.FrontEnd.Helpers
{
    public static class JsonHelper
    {
        public static string JsonHtmlHelper(this HtmlHelper html, object objectToSerialize)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(objectToSerialize);
        }
    }
}