using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pylon.Domain.Entities.Base;
using Pylon.Domain.Repositories;
using Pylon.Infrastructure.Persistence;
using Pylon.Shared.Enums;
using Pylon.Shared.Helpers;
using System.Linq.Expressions;

namespace Pylon.Infrastructure.Repositories.Base
{
	public class CoreRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;
		private readonly IServiceProvider _serviceProvider;

		public CoreRepository(AppDbContext context, IServiceProvider serviceProvider)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_dbSet = _context.Set<T>();
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		/// <summary>
		/// Retrieves an instance of a specified repository type from the service provider.
		/// </summary>
		/// <typeparam name="TNewRepository">The type of the repository to retrieve.</typeparam>
		/// <returns>An instance of the requested repository.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the repository type is not registered in the service provider.</exception>
		protected TNewRepository GetRepository<TNewRepository>() where TNewRepository : class
		{
			return _serviceProvider.GetRequiredService<TNewRepository>();
		}

		/// <summary>
		/// Returns an IQueryable for the entity type T without tracking changes.
		/// </summary>
		/// <returns>An IQueryable of type T.</returns>
		public IQueryable<T> GetQueryable()
		{
			return _dbSet.AsNoTracking();
		}

		/// <summary>
		/// Returns an IQueryable for a specified entity type without tracking changes.
		/// </summary>
		/// <typeparam name="TNewEntity">The type of entity to query.</typeparam>
		/// <returns>An IQueryable of the specified entity type.</returns>
		public IQueryable<TNewEntity> GetQueryable<TNewEntity>() where TNewEntity : class
		{
			return _context.Set<TNewEntity>().AsNoTracking();
		}

		/// <summary>
		/// Returns an IQueryable for an entity by its ID without tracking changes.
		/// </summary>
		/// <param name="id">The ID of the entity to query.</param>
		/// <returns>An IQueryable of the entity with the specified ID.</returns>
		public IQueryable<T> GetQueryableById(long id)
		{
			return _dbSet.Where(c => EF.Property<long>(c, "Id") == id).AsNoTracking();
		}

		/// <summary>
		/// Retrieves an entity by its ID asynchronously.
		/// </summary>
		/// <param name="id">The ID of the entity to retrieve.</param>
		/// <returns>The entity with the specified ID, or null if not found.</returns>
		public async Task<T?> GetByIdAsync(long id)
		{
			return await _dbSet.FindAsync(id);
		}

		/// <summary>
		/// Retrieves an entity by its ID with related data asynchronously.
		/// </summary>
		/// <param name="id">The ID of the entity to retrieve.</param>
		/// <param name="includes">Expressions specifying related data to include.</param>
		/// <returns>The entity with the specified ID and related data, or null if not found.</returns>
		public async Task<T?> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id);
		}

		/// <summary>
		/// Retrieves all entities of type T asynchronously.
		/// </summary>
		/// <returns>A list of all entities of type T.</returns>
		public async Task<List<T>> GetAllAsync(int top = 10)
		{
			return await _dbSet.Take(10).ToListAsync();
		}

		/// <summary>
		/// Finds entities matching a specified condition asynchronously.
		/// </summary>
		/// <param name="predicate">The condition to filter entities.</param>
		/// <returns>A list of entities that match the predicate.</returns>
		public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Finds entities matching a condition with related data asynchronously.
		/// </summary>
		/// <param name="predicate">The condition to filter entities.</param>
		/// <param name="includes">Expressions specifying related data to include.</param>
		/// <returns>A list of entities that match the predicate with related data.</returns>
		public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			foreach (var include in includes)
			{
				query = query.Include(include);
			}
			return await query.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Adds a new entity to the repository.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		public void Add(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_dbSet.Add(entity);
		}

		/// <summary>
		/// Adds multiple entities to the repository asynchronously without saving changes.
		/// </summary>
		/// <param name="entities">The collection of entities to add.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			if (entities == null) throw new ArgumentNullException(nameof(entities));
			await _dbSet.AddRangeAsync(entities);
		}

		/// <summary>
		/// Adds multiple entities to the repository and saves changes asynchronously (bulk insert).
		/// </summary>
		/// <param name="entities">The collection of entities to add.</param>
		/// <returns>A ServiceResult indicating success or failure of the bulk insert operation.</returns>
		public async Task<ServiceResult> BulkInsertAsync(IEnumerable<T> entities)
		{
			try
			{
				await _dbSet.AddRangeAsync(entities);
				await _context.SaveChangesAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (Exception ex)
			{
				return ServiceResult.Failure($"Bulk insert error: {ex.Message}");
			}
		}

		/// <summary>
		/// Updates an existing entity in the repository.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		public void Update(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_dbSet.Update(entity);
		}

		/// <summary>
		/// Updates multiple entities in the repository.
		/// </summary>
		/// <param name="entities">The collection of entities to update.</param>
		public void UpdateRange(IEnumerable<T> entities)
		{
			if (entities == null) throw new ArgumentNullException(nameof(entities));
			_dbSet.UpdateRange(entities);
		}

		/// <summary>
		/// Updates multiple entities in the repository.
		/// </summary>
		/// <param name="entities">The collection of entities to update.</param>
		public async Task<ServiceResult> BulkUpdateAsync(IEnumerable<T> entities)
		{
			if (entities == null || !entities.Any())
				return ServiceResult.Failure("No entities provided for update.");

			try
			{
				_dbSet.UpdateRange(entities);
				await _context.SaveChangesAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (Exception ex)
			{
				return ServiceResult.Failure($"Bulk update error: {ex.Message}");
			}
		}

		/// <summary>
		/// Deletes an entity from the repository.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		public void Delete(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			_dbSet.Remove(entity);
		}

		/// <summary>
		/// Deletes multiple entities from the repository.
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		public void DeleteRange(IEnumerable<T> entities)
		{
			if (entities == null) throw new ArgumentNullException(nameof(entities));
			_dbSet.RemoveRange(entities);
		}

		/// <summary>
		/// Deletes entities
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		/// <returns>A ServiceResult indicating success or failure of the delete operation.</returns>
		public async Task<ServiceResult> BulkDeleteAsync(IEnumerable<T> entities)
		{
			if (entities == null || !entities.Any())
				return ServiceResult.Failure("No entities provided for deletion.");

			try
			{
				_dbSet.RemoveRange(entities);
				await _context.SaveChangesAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_DELETE);
			}
			catch (Exception ex)
			{
				return ServiceResult.Failure($"Bulk delete error: {ex.Message}");
			}
		}

		/// <summary>
		/// Deletes entities matching a condition asynchronously.
		/// </summary>
		/// <param name="predicate">The condition to identify entities to delete.</param>
		/// <returns>A ServiceResult indicating success or failure of the delete operation.</returns>
		public async Task<ServiceResult> BulkDeleteByConditionAsync(Expression<Func<T, bool>> predicate)
		{
			if (predicate == null)
				return ServiceResult.Failure("Predicate must not be null.");

			try
			{
				int affectedRows = await _dbSet.Where(predicate).ExecuteDeleteAsync();
				return affectedRows > 0
					? ServiceResult.Success(ResponseMessage.SUCCESSFUL_DELETE)
					: ServiceResult.Failure("No records found to delete.");
			}
			catch (Exception ex)
			{
				return ServiceResult.Failure($"Bulk delete error: {ex.Message}");
			}
		}

		/// <summary>
		/// Saves all changes made to the context asynchronously. (using Transaction) (Considering RowStateType in each Entity)
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <returns>A ServiceResult indicating success or failure of the save operation.</returns>
		public async Task<ServiceResult> SaveChangesAsync(T entity)
		{
			// Apply CRUD Opration By StateType
			ApplyByStateType(entity);

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				if (!_context.ChangeTracker.HasChanges())
					return ServiceResult.Success(ResponseMessage.NO_CHANGES_DETECTED);

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (DbUpdateException dbEx)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure($"Database update error: {dbEx.Message}");
			}
			catch (Exception error)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure(error.Message);
			}
		}

		/// <summary>
		/// Saves all changes made to the context asynchronously. (Considering RowStateType in each Entity) (using Transaction) 
		/// </summary>
		/// <param name="entities">The collection of entities to save in database.</param>
		/// <returns>A ServiceResult indicating success or failure of the save operation.</returns>
		public async Task<ServiceResult> SaveChangesAsync(IEnumerable<T> entities)
		{
			// Apply CRUD Opration By StateType
			ApplyByStateType(entities);

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				if (!_context.ChangeTracker.HasChanges())
					return ServiceResult.Success(ResponseMessage.NO_CHANGES_DETECTED);

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (DbUpdateException dbEx)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure($"Database update error: {dbEx.Message}");
			}
			catch (Exception error)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure(error.Message);
			}
		}

		/// <summary>
		/// Apply CRUD Opration without Saving Changes By State Types of Entities
		/// </summary>
		/// <returns></returns>
		private void ApplyByStateType(T entity)
		{
			if (entity is not BaseEntity baseEntity)
				throw new InvalidOperationException($"Entity of type {typeof(T).Name} does not inherit from BaseEntity, so its state cannot be determined.");

			switch (baseEntity.StateType)
			{
				case StateType.Insert:
					if (_context.Entry(entity).State == EntityState.Detached)
						_context.Attach(entity);
					Add(entity);
					break;
				case StateType.Update:
					Update(entity);
					break;
				case StateType.Delete:
					Delete(entity);
					break;
			}
		}

		/// <summary>
		/// Apply CRUD Opration without Saving Changes By State Types of Entities
		/// </summary>
		/// <returns></returns>
		private void ApplyByStateType(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				if (entity is not BaseEntity baseEntity)
					throw new InvalidOperationException($"Entity of type {typeof(T).Name} does not inherit from BaseEntity, so its state cannot be determined.");

				switch (baseEntity.StateType)
				{
					case StateType.Insert:
						if (_context.Entry(entity).State == EntityState.Detached)
							_context.Attach(entity);
						Add(entity);
						break;
					case StateType.Update:
						Update(entity);
						break;
					case StateType.Delete:
						Delete(entity);
						break;
				}
			}
		}

		/// <summary>
		/// Checks if an entity matching the specified condition exists asynchronously.
		/// </summary>
		/// <param name="predicate">The condition to check for existence.</param>
		/// <returns>True if an entity exists, false otherwise.</returns>
		public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		#region Super Bulk Insert - For Big Data
		/// <summary>
		/// Performs a bulk insert operation for a collection of entities.
		/// </summary>
		/// <param name="entities">The collection of entities to be inserted.</param>
		/// <returns>A ServiceResult indicating the success or failure of the operation.</returns>
		public async Task<ServiceResult> SuperBulkInsertAsync(IEnumerable<T> entities)
		{
			if (entities == null || !entities.Any())
				return ServiceResult.Failure("No entities provided for bulk insert.");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var options = new BulkConfig
				{
					PreserveInsertOrder = true,  // Keeps the original order of the entities
					SetOutputIdentity = true,    // Ensures newly generated identities (IDs) are retrieved after insert
					BatchSize = 5000,            // Processes the insert in batches of 5000 to optimize memory usage and performance
				};

				await _context.BulkInsertAsync(entities.ToList(), options);
				await transaction.CommitAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure($"SuperBulkInsert error: {ex.Message}");
			}
		}

		/// <summary>
		/// Performs a bulk update operation for a collection of entities.
		/// </summary>
		/// <param name="entities">The collection of entities to be updated.</param>
		/// <returns>A ServiceResult indicating the success or failure of the operation.</returns>
		public async Task<ServiceResult> SuperBulkUpdateAsync(IEnumerable<T> entities)
		{
			if (entities == null || !entities.Any())
				return ServiceResult.Failure("No entities provided for bulk update.");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var options = new BulkConfig
				{
					BatchSize = 5000,
					PreserveInsertOrder = false,
				};

				await _context.BulkUpdateAsync(entities.ToList(), options);
				await transaction.CommitAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_SAVE_CHANGES);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure($"SuperBulkUpdate error: {ex.Message}");
			}
		}

		/// <summary>
		/// Performs a bulk delete operation for a collection of entities.
		/// </summary>
		/// <param name="entities">The collection of entities to be deleted.</param>
		/// <returns>A ServiceResult indicating the success or failure of the operation.</returns>
		public async Task<ServiceResult> SuperBulkDeleteAsync(IEnumerable<T> entities)
		{
			if (entities == null || !entities.Any())
				return ServiceResult.Failure("No entities provided for bulk delete.");

			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var options = new BulkConfig
				{
					BatchSize = 5000,
					PreserveInsertOrder = false,
				};

				await _context.BulkDeleteAsync(entities.ToList(), options);
				await transaction.CommitAsync();
				return ServiceResult.Success(ResponseMessage.SUCCESSFUL_DELETE);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResult.Failure($"SuperBulkDelete error: {ex.Message}");
			}
		}

		#endregion
	}
}
