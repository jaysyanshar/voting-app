using System.Linq;
using Microsoft.EntityFrameworkCore;
using VotingApp.Core.Models;

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

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<TEntity>( entity =>
            {
                entity.ToTable( typeof( TEntity ).Name );
            } );
        }

        public void DetachLocal<TKey, TModel>( TKey key, TModel value )
            where TModel : class, IModel<TKey>
        {
            TModel local = Set<TModel>()
                .Local
                .FirstOrDefault( entry => entry.GetKey().Equals( key ) );

            if( local != null )
            {
                Entry( local ).State = EntityState.Detached;
            }

            Entry( value ).State = EntityState.Modified;
        }

    }
}