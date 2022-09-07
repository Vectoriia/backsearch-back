using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackSearch.DAL
{
    public class UserRepository
    {
        private readonly DataContext context;
        private readonly DbSet<User> dBUsers;

        public UserRepository(DataContext dataContext)
        {
            this.context = dataContext;
            dBUsers = dataContext.Set<User>();
        }
        public async Task<User> InsertAsync(User user)
        {
            await dBUsers.AddAsync(user);
            context.SaveChanges();
            return user;
        }
        public IEnumerable<User> GetAll()
        {
            return dBUsers;
        }
    }
}
