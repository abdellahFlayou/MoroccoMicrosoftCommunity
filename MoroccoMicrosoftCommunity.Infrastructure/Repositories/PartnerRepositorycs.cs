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
    public class PartnerRepositorycs : GenericRepository<Partenaire> ,IPartnerRepository
    {
        private AppDBContext _dbContext;
        private readonly DbSet<Partenaire> _dbSet;
        public PartnerRepositorycs(AppDBContext appdbContext) : base(appdbContext)
        {
            _dbContext = appdbContext;
            _dbSet = appdbContext.Set<Partenaire>();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task Update(Partenaire entity)
        {
            // Supposons que Partenaire a une propriété Id qui représente la clé primaire.
            var existingEntity = await _dbSet.FindAsync(entity.PartenaireId);

            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Entity with id {entity.PartenaireId} not found for update.");
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with id {id} not found for deletion.");
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
