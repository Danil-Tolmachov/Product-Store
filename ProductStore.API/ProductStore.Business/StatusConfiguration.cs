using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL
{
	public static class StatusConfiguration
	{
		public static long SubmitedStatusId { get; } = 1;
		public static long CompletedStatusId { get; } = 3;
		public static long CanceledStatusId { get; } = 4;
	}
}
