using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Shared.Helpers
{
	public class PaginationClass
	{
		public int Skip { get; set; } = 0; // skip
		public int PageSize { get; set; } = 10; // top
		public string Orderby { get; set; } = string.Empty; // orderby
	}
}
