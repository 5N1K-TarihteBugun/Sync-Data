using HtmlAgilityPack;

namespace SyncData.Common
{
    public class WebDocument
    {
        private readonly string _url;

        public WebDocument(string url)
        {
            _url = url;
        }

        public HtmlDocument Load()
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(_url);

            return htmlDoc;
        }

        public HtmlNodeCollection GetSelectNodes(string regex)
        {
            return Load().DocumentNode.SelectNodes(regex);
        }
    }
}