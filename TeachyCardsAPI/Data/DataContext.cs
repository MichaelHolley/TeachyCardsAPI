using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TeachyCardsAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Card> Cards { get; set; }

		public override int SaveChanges()
		{
			DefaultEntityBaseOperation();
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			DefaultEntityBaseOperation();
			return base.SaveChangesAsync();
		}

		private void DefaultEntityBaseOperation()
		{
			var entries = ChangeTracker.Entries().Where(e =>
				e.Entity is BaseEntity && (
				e.State == EntityState.Added
				|| e.State == EntityState.Modified));

			foreach (var entityEntry in entries)
			{
				((BaseEntity)entityEntry.Entity).Modified = DateTime.Now;

				if (entityEntry.State == EntityState.Added)
				{
					((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
				}
			}
		}
	}
}
