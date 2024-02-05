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
    public class EventRepo : GenericRepository<Evenement>, IEventRepo
    {
        private readonly AppDBContext _dbContext;
        public EventRepo(AppDBContext appdbContext) : base(appdbContext)
        {
            _dbContext = appdbContext;
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}