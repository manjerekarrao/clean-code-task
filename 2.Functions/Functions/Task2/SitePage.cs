using System.Collections.Generic;
using System.Text;

namespace Functions.Task2
{
    public class SitePage
    {
        private const string Http = "http://";
        private const string Editable = "/?edit=true";
        private const string Domain = "mysite.com";

        public string SiteGroup { get; }
        public string UserGroup { get; }

        public SitePage(string siteGroup, string userGroup)
        {
            SiteGroup = siteGroup;
            UserGroup = userGroup;
        }

        public string GetEditablePageUrl(IDictionary<string, string> pageUrls)
        {
            var pageUrlName = new StringBuilder();
            foreach (var pageUrl in pageUrls)
            {
                pageUrlName.Append($"&{pageUrl.Key}={pageUrl.Value}");
            }

            return Http + Domain + Editable + pageUrlName + GetAttributes();
        }

        private string GetAttributes()
        {
            return $"&siteGrp={SiteGroup}&userGrp={UserGroup}";
        }
    }
}