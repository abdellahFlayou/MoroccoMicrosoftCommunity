using Microsoft.EntityFrameworkCore;
using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Domain.Models;
using MoroccoMicrosoftCommunity.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Infrastructure.Repositories
{
    public class SessionRepo : GenericRepository<Session>,ISessionRepo
    {
        private AppDBContext _dbContext;
        //private readonly DbSet<Session> _dbSet;
        public SessionRepo(AppDBContext appdbContext) : base(appdbContext)
        {
            _dbContext = appdbContext;
          //  _dbSet = appdbContext.Set<Session>();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
        //public async Task<Session> Add(Session entity)
        //{
        //    var addentity = _dbSet.Add(entity);
        //    await _dbContext.SaveChangesAsync();

        //    return entity;
        //}
    }
}
