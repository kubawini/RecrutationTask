using AutoMapper;
using ModelLayer.DbContexts;
using ModelLayer.DTOs;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IUsersDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public UsersRepository(IUsersDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task SaveAllUsers(IEnumerable<UserModel> users)
        {
            using (UsersDbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach(var user in users)
                {
                    await context.Users.AddAsync(_mapper.Map<UserDTO>(user));
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(UserModel user)
        {
            using (UsersDbContext context = _dbContextFactory.CreateDbContext())
            {
                int userId = user.id;
                var userToBeUpdated = context.Users.Where(u => u.Id == userId).Single();
                UpdateUserData(userToBeUpdated, user);
                await context.SaveChangesAsync();
            }
        }

        public void UpdateUserData(UserDTO userToBeUpdated, UserModel newUser)
        {
            userToBeUpdated.Name = newUser.name;
            userToBeUpdated.Surname = newUser.surename;
            userToBeUpdated.Email = newUser.email;
            userToBeUpdated.Phone = newUser.phone;
        }
    }
}
