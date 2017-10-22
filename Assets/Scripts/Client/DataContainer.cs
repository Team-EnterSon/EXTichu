using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSTichu.Client
{
	public static class DataContainer
	{
		public static string MyNickname { get; set; } = null;
		public static string HostIP { get; set; } = null;
		public static int HostPort { get; set; } = 8888;
	}
}
