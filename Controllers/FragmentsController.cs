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
using LfragmentApi.DTOs;

namespace LfragmentApi.Controllers
{
    public class FragmentController : ControllerBase
    {
        private readonly FragmentDbContext _context;
        private readonly UserDbContext _user;

        public FragmentController(FragmentDbContext fragmentDbContext, UserDbContext userDbContext)
        {
            _context = fragmentDbContext;
            _user = userDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fragment>>> GetAll()
        {
            return Ok(await _context.Fragments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fragment>> GetByID(Guid id)
        {
            var fragment = await _context.Fragments
                .FirstOrDefaultAsync(f => f.Id == id);

            return fragment is null ? NotFound() : Ok(fragment);
        }

        [HttpGet]
        public async Task<ActionResult<List<Fragment>>> GetFiltered([FromQuery] FilterSettings filter)
        {
            var query = FragmentFilter.Filter(filter, _context);
            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            var result = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
  
            return Ok(new
            {
                Results = result,
                PageCount = pageCount,
                TotalCount = totalCount
            });
        }


        [HttpGet("ByUser/{id}")]
        public async Task<ActionResult<List<Fragment>>> GetByUserId(Guid id)
        {
            var fragments = await _context.Fragments.Where(f => f.UserId == id)
                .ToListAsync();

            return fragments is null ? NotFound() : Ok(fragments);
        }

        [HttpPost]
        public async Task<ActionResult<Fragment>> Post(CreateFragmentDto createFragmentDto)
        {
            
            return Ok();
        }
    }
}