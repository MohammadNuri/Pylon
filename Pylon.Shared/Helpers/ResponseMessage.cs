using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Shared.Helpers
{
	public static class ResponseMessage
	{
		public const string SUCCESSFUL_SAVE_CHANGES = "با موفقیت انجام شد. ";
		public const string SUCCESSFUL_DELETE = "با موفقیت حذف شد.";
		public const string NO_CLIENT_DATA = "داده ای در ورودی دریافت نشد.";
		public const string WRONG_CLIENT_DATA = "داده های ورودی نامعتبر است.";
		public const string NO_CHANGES_DETECTED = "تغییری در داده ها برای ذخیره یافت نشد.";
	}
}
