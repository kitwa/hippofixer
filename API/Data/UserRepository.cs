using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using API.Helpers;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(int id)
        {
            return await _context.Users
            .Where(x => x.Id == id)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }


        public async Task<MemberDto> GetMemberAgentAsync(string email)
        {
            return await _context.Users
            .Where(x => x.Email == email)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users
            .OrderBy(p => p.Created)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .Where(p => p.Email != userParams.CurrentEmail);

            return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);

        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users
            .SingleOrDefaultAsync(x => x.Email == email);
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}