using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
    static class MathPlaneTool
    {
		public static Vector3 GetPointOnPlane(this MathPlane plane)
		{
			var orthogonalVector = plane.Normal.GetOrthogonalVector();
			return plane.Point + orthogonalVector;
		}

		public static bool IsCross(this MathPlane plane, MathLine line ,ref Vector3 position)
		{
			var dot = Vector3.Dot(plane.Normal, line.Direction);
			bool result = dot != 0;
			if (result)
			{
				var d = Vector3.Dot(plane.GetPointOnPlane() - line.Point, plane.Normal) / dot;
				position = d * line.Direction + line.Point;
			}
			return result;
		}

	}
}
