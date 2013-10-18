namespace Storage.Code
{
    public class PagerSettings
    {
        public string Pages { get; set; }
        public string FirstPage { get; set; }
        public string PrevPage { get; set; }
        public string NextPage { get; set; }
        public string LastPage { get; set; }
        public int PagesRangeSize { get; set; }

        public static PagerSettings Default
        {
            get
            {
                return new PagerSettings()
                {
                    Pages = "Страницы",
                    FirstPage = "Первая",
                    PrevPage = "Предыдущая",
                    NextPage = "Следующая",
                    LastPage = "Последняя",
                    PagesRangeSize = 5,
                };
            }
        }
    }
}