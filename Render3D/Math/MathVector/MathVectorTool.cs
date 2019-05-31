using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
    static class MathVectorTool
    {
		public static Vector3 GetOrthogonalVector(this Vector3 vector)
		{
			Vector3 v = new Vector3(0, 0, 0);

			v.X = (float)MathTool.Random();
			v.Y = (float)MathTool.Random(false);
			v.Z = -Vector3.Dot(vector, v) / vector.Z;

			return Vector3.Normalize(v);
		}
	}
}
