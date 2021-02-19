using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public abstract class DataContextBase<TEntity, TContext> : DbContext
        where TEntity : class, IModel
        where TContext : DataContextBase<TEntity, TContext>
    {
        protected DataContextBase( DbContextOptions<TContext> options ) : base( options )
        {
        }

        public DbSet<TEntity> DataSet { get; set; }
    }
}