namespace LfragmentApi.Helpers
{
    public class FilterSettings
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string FilterBy { get; set; }
        public string OrderBy {  get; set; }
        public string SearchTerm { get; set; }
        public string ProgLang { get; set; }



    }
}
