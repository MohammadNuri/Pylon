using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Shared.Helpers
{
	public class ServiceResult
	{
		public bool IsSuccessful { get; }
		public string Message { get; }

		protected ServiceResult(bool isSuccessful, string message)
		{
			IsSuccessful = isSuccessful;
			Message = message;
		}

		public static ServiceResult Success(string message = "")
		{
			return new ServiceResult(true, message);
		}

		public static ServiceResult Failure(string message)
		{
			return new ServiceResult(false, message);
		}
	}

	public class ServiceResult<T> : ServiceResult
	{
		public T ReturnValue { get; }
		private ServiceResult(bool isSuccessful, string message, T returnValue)
		: base(isSuccessful, message)
		{
			ReturnValue = returnValue;
		}
		public static ServiceResult<T> Success(string message = null, T returnValue = default)
		{
			return new ServiceResult<T>(true, message, returnValue);
		}

		public static ServiceResult<T> Failure(string message = null, T returnValue = default)
		{
			return new ServiceResult<T>(false, message, returnValue);
		}
	}
}
