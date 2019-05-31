using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	struct Matrix
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		private double[,] _value;

		public Matrix(int width, int height)
		{
			Width = width;
			Height = height;
			_value = new double[Width, Height];
		}

		public bool TryGetValue(int x, int y, out float value)
		{
			double n;
			bool result = TryGetValue(x, y, out n);
			value = (float)n;
			return result;
		}
		public bool TryGetValue(int x, int y, out double value)
		{
			if (CheckRange(x, y) == false)
			{ 
				value = 0;
				Console.WriteLine("TryGetValue Error");
				return false;
			}

			value = _value[x, y];
			return true;
		}
		public bool TrySetValue(int x, int y, double value)
		{
			if (CheckRange(x,y) == false)
			{
				Console.WriteLine("TrySetValue Error");
				return false;
			}

			_value[x, y] = value;
			return true;
		}

		public override string ToString()
		{
			string str = "";

			for (int y = 0; y < Height; y++)
			{
				str += "[";
				for (int x = 0; x < Width; x++)
				{
					str += string.Format("{0:0.00}, ", _value[x, y]);
				}
				str += "]\n";
			}
			return str;
		}

		private bool CheckRange(int x, int y)
		{
			return MathTool.IsInRange(0, Width, x, true, false) && MathTool.IsInRange(0, Height, y, true, false);
		}
	}
}
