using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AntwerpRC.UI.Umbraco.Helpers
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            if (response.Files.Any())
            {
                string currentDir = Directory.GetCurrentDirectory();
                var lessDir =
                    Path.GetDirectoryName(string.Concat(HttpContext.Current.Server.MapPath("~"),
                        response.Files.First().VirtualFile.VirtualPath));
                if (!string.IsNullOrEmpty(lessDir))
                {
                    //Setting Directory so the dotless library is able to find all @import statements in the base less file
                    Directory.SetCurrentDirectory(lessDir);

                    response.Content = dotless.Core.Less.Parse(response.Content);
                    response.ContentType = "text/css";

                    //Set the directory back to the previous value
                    Directory.SetCurrentDirectory(currentDir);
                }
            }
        }
    }
}