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
    public class SupportRepo : GenericRepository<Support>, ISupportRepo
    {
        private readonly AppDBContext _dbContext;
        public SupportRepo(AppDBContext appdbContext) : base(appdbContext)
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
