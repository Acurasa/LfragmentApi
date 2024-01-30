using AutoMapper;
using Humanizer;
using LfragmentApi.Data;
using LfragmentApi.DTOs;
using LfragmentApi.Entities;
using LfragmentApi.Helpers;
using LfragmentApi.RequestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LfragmentApi.Controllers
{
    public class FragmentController : ControllerBase
    {
        private readonly FragmentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public FragmentController(FragmentDbContext fragmentDbContext, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = fragmentDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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

        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<List<Fragment>>> GetByUserId(Guid id)
        {
            var fragments = await _context.Fragments.Where(f => f.UserId == id)
                .ToListAsync();

            return fragments is null ? NotFound() : Ok(fragments);
        }

        [HttpPost("CreateFragment")]
        public async Task<ActionResult<Fragment>> Post([FromBody] CreateFragmentDto createFragmentDto)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var fragment = _mapper.Map<Fragment>(createFragmentDto);
            fragment.UserId = currentUser.Id;

            _context.Fragments.Add(fragment);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok(fragment);
            }

            return BadRequest();
        }

        [HttpPut("EditFragment")]
        public async Task<ActionResult<Fragment>> Edit([FromBody] UpdateFragmentDto updateFragmentDto)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);
            var fragment = await _context.Fragments
                .FirstOrDefaultAsync(x => x.Id == updateFragmentDto.Id);
            if (fragment == null) { return NotFound(); }
            if (currentUser == null || currentUser.Id != fragment.UserId)
            {
                return Unauthorized();
            }
            _mapper.Map(updateFragmentDto, fragment);

            if (!(await _context.SaveChangesAsync() > 0))
            {
                return BadRequest($"Could not save Changes to the DB, Object : {nameof(fragment)}");
            }

            return Ok(fragment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);
            var fragment = await _context.Fragments.FirstOrDefaultAsync(x => x.Id == id);
            if (fragment == null)
            { return NotFound(); }
            if (currentUser == null || currentUser.Id != fragment.UserId)
            {
                return Unauthorized();
            }
            _context.Fragments.Remove(fragment);
            return Ok($"Fragment with GUID of : {id} has been deleted");
        }
    }
}