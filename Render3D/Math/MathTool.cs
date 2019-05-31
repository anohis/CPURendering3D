using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	static class MathTool
	{
		private static Random _random = new Random();

		public static bool IsInRange(double min, double max, double value, bool containMin, bool containMax)
		{
			if (value < min || (containMin == false && value == min))
			{
				return false;
			}

			if (value > max || (containMax == false && value == max))
			{
				return false;
			}

			return true;
		}
		public static bool IsInCube(Vector3 point, double r, double l, double t, double b, double n, double f)
		{
			return IsInRange(l, r, point.X, true, true)
				&& IsInRange(b, t, point.Y, true, true)
				&& IsInRange(n, f, point.Z, true, true);
		}

		public static void CompareMinMax(double value, ref int min, ref int max)
		{
			if (value <= min)
			{
				min = (int)Math.Floor(value);
			}
			if (value >= max)
			{
				max = (int)Math.Ceiling(value);
			}
		}

		public static double DegToRad(double deg)
		{
			return deg * Math.PI / 180.0f;
		}
		public static double RadToDeg(double rad)
		{
			return rad * 180.0f / Math.PI;
		}

		public static double Sign(Vector3 v1,Vector3 v2)
		{
			return v1.X * v2.Y - v2.X * v1.Y;
		}

		public static double Sin(double deg)
		{
			return Math.Sin(DegToRad(deg));
		}
		public static double Cos(double deg)
		{
			return Math.Cos(DegToRad(deg));
		}

		public static double Random(bool containZero = true)
		{
			var v = _random.NextDouble();

			if (containZero == false && v == 0)
			{
				v += 0.01f;
			}

			return v;
		}
	}
}
