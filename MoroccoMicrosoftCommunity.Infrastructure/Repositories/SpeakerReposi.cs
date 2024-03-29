﻿using Microsoft.EntityFrameworkCore;
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
    public class SpeakerReposi : GenericRepository<Speaker>, ISpeakerRepo
    {
        private AppDBContext _dbContext;
        public SpeakerReposi(AppDBContext appdbContext) : base(appdbContext)
        {
            _dbContext = appdbContext;
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<Speaker> AddOrUpdateSpeaker(Speaker speaker)
        {
            var existingSpeaker = await _dbContext.Speakers.FindAsync(speaker.SpeakerId);

            if (existingSpeaker != null)
            {
                _dbContext.Entry(existingSpeaker).State = EntityState.Detached;
            }

            var addedOrUpdatedSpeaker = await _dbContext.Speakers.AddAsync(speaker);

            await _dbContext.SaveChangesAsync();

            return addedOrUpdatedSpeaker.Entity;
        }
    }
}
