namespace Sync.Common
{
    public static class ProjectConst
    {
        public static class ActionConst
        {
            private const string BaseConst = "/html/body/div[3]/div[3]/div[4]/div";

            public const string Event = BaseConst + "/ul[1]/li";
            public const string Birth = BaseConst + "/ul[2]/li";
            public const string Death = BaseConst + "/ul[3]/li";
            public const string Other = BaseConst + "/ul[4]/li";
        }
    }
}