using HtmlAgilityPack;

namespace Sync.Common
{
    public class WebDocument
    {
        private HtmlDocument Load(string url)
        {
            var web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            return htmlDoc;
        }

        public HtmlNodeCollection GetSelectNodes(string url, string regex)
        {
            return Load(url).DocumentNode.SelectNodes(regex);
        }
    }
}