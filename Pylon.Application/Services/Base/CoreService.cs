using Microsoft.Extensions.DependencyInjection;

namespace Pylon.Application.Services.Base
{
	public class CoreService<T> where T : class
	{
		private readonly T _repository;
		private readonly IServiceProvider _serviceProvider;

		public CoreService(T repository, IServiceProvider serviceProvider)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		/// <summary>
		/// Retrieves the primary repository instance associated with this service.
		/// </summary>
		/// <returns>The repository of type T.</returns>
		public T GetRepository()
		{
			return _repository;
		}

		/// <summary>
		/// Retrieves an instance of a specified repository type from the service provider.
		/// </summary>
		/// <typeparam name="TNewRepository">The type of the repository to retrieve.</typeparam>
		/// <returns>An instance of the requested repository.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the repository type is not registered in the service provider.</exception>
		public TNewRepository GetRepository<TNewRepository>() where TNewRepository : class
		{
			return _serviceProvider.GetRequiredService<TNewRepository>();
		}

		/// <summary>
		/// Retrieves an instance of a specified service type from the service provider.
		/// </summary>
		/// <typeparam name="TService">The type of the service to retrieve.</typeparam>
		/// <returns>An instance of the requested service.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the service type is not registered in the service provider.</exception>
		protected TService GetService<TService>() where TService : class
		{
			return _serviceProvider.GetRequiredService<TService>();
		}
	}
}
