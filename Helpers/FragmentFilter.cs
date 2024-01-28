using LfragmentApi.Data;
using LfragmentApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LfragmentApi.Helpers
{
    public class FragmentFilter
    {
        private readonly FilterSettings filter;
        private readonly FragmentDbContext _context;

        public FragmentFilter(FilterSettings filter,FragmentDbContext _context)
        {
            this.filter = filter;
            this._context = _context;
        }

        public IQueryable<Entities.Fragment> Filter()
        {
            var query = _context.Fragments.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(x =>
                    EF.Functions.Like(x.Content.ToLower(), $"%{filter.SearchTerm}%".ToLower()) ||
                    EF.Functions.Like(x.Title.ToLower(), $"%{filter.SearchTerm}%".ToLower()));
            }
            
            if (!string.IsNullOrEmpty(filter.ProgLang))
            {
                List<string> langs = PL.ToList();
                query = query.Where(x => x.Tags.Any(n=> langs.Contains(filter.ProgLang)));
            }

            query = filter.OrderBy switch
            {
                "new" => query.OrderByDescending(x => x.Created),
                "updated" => query.OrderByDescending(x => x.Updated),
                _ => query.OrderBy(x => x.Created)
            };

            //query = filter.FilterBy switch
            //{
                
            //};

            return query;
        }
    }
}
