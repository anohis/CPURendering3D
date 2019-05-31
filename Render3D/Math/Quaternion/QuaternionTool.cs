using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	static class QuaternionTool
	{
		public static Quaternion VectorToQuaternion(Vector3 v)
		{
			Quaternion quaternion = new Quaternion();
			double num9 = MathTool.DegToRad(v.Z) * 0.5f;
			double num6 = Math.Sin(num9);
			double num5 = Math.Cos(num9);
			double num8 = MathTool.DegToRad(v.X) * 0.5f;
			double num4 = Math.Sin(num8);
			double num3 = Math.Cos(num8);
			double num7 = MathTool.DegToRad(v.Y) * 0.5f;
			double num2 = Math.Sin(num7);
			double num = Math.Cos(num7);
			quaternion.X = (float)(((num * num4) * num5) + ((num2 * num3) * num6));
			quaternion.Y = (float)(((num2 * num3) * num5) - ((num * num4) * num6));
			quaternion.Z = (float)(((num * num3) * num6) - ((num2 * num4) * num5));
			quaternion.W = (float)(((num * num3) * num5) + ((num2 * num4) * num6));
			return quaternion;
		}
	}
}
