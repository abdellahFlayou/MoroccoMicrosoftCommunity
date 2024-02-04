﻿using MoroccoMicrosoftCommunity.Application.Interface;
using MoroccoMicrosoftCommunity.Domain.Models;
using MoroccoMicrosoftCommunity.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoroccoMicrosoftCommunity.Infrastructure.Repositories
{
    public class SponsorRepo : GenericRepository<Sponsor>, ISponsorRepos
    {
        public SponsorRepo(AppDBContext appdbContext) : base(appdbContext)
        {
        }
    }
}
