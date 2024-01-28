using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LfragmentApi.Data;
using LfragmentApi.Entities;
using LfragmentApi.Helpers;

namespace LfragmentApi.Controllers
{
    public class FragmentController : ControllerBase
    {
        private readonly FragmentDbContext _context;

        public FragmentController(FragmentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fragment>>> GetAll()
        {
            return Ok(await _context.Fragments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fragment>> GetAll(Guid id)
        {
            var fragment = await _context.Fragments
                .FirstOrDefaultAsync(f => f.Id == id);

            return fragment is null ? NotFound() : Ok(fragment);
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<List<Fragment>>> GetFiltered([FromQuery] FilterSettings filter)
        {
            var query = _context.Fragments.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(x =>
                    EF.Functions.Like(x.Content.ToLower(), $"%{filter.SearchTerm}%".ToLower()) ||
                    EF.Functions.Like(x.Title.ToLower(), $"%{filter.SearchTerm}%".ToLower()));
            }

            query = filter.OrderBy switch
            {
                "new" => query.OrderByDescending(x => x.Created),
                "updated" => query.OrderByDescending(x=> x.Updated), 
                _ => query.OrderBy(x => x.Created)
            };

            query = filter.FilterBy switch
            {
                "C#" => query.Where(x => x.Tags.Any(t=>t.Name == "C#")),
            };

            return Ok();
        }
    }
}