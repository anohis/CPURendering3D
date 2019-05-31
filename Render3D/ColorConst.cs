using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class ColorConst
	{
		public enum ColorChannel : int
		{
			B = 0,
			G = 1,
			R = 2,
			A = 3
		}

		public const int ChannelCount = 4;
	}
}
