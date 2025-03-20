using Microsoft.AspNetCore.Mvc;

namespace Pylon.API.Controllers.Base
{
	public abstract class CoreController<TService> : ControllerBase where TService : class
	{
		private readonly IServiceProvider _serviceProvider;

		protected CoreController(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		/// <summary>
		/// Retrieves the primary service instance of type TService from the service provider.
		/// </summary>
		/// <returns>An instance of the primary service of type TService.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the service type is not registered in the service provider.</exception>
		protected TService GetService()
		{
			return _serviceProvider.GetRequiredService<TService>();
		}

		/// <summary>
		/// Retrieves an instance of a specified service type from the service provider.
		/// </summary>
		/// <typeparam name="TNewService">The type of the service to retrieve.</typeparam>
		/// <returns>An instance of the requested service.</returns>
		/// <exception cref="InvalidOperationException">Thrown if the service type is not registered in the service provider.</exception>
		protected TNewService GetService<TNewService>() where TNewService : class
		{
			return _serviceProvider.GetRequiredService<TNewService>();
		}
	}
}
