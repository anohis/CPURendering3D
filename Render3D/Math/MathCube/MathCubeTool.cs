using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	static class MathCubeTool
	{
		public static List<MathPlane> GetPlanes(this MathCube cube)
		{
			var rtn = new Vector3(cube.Right, cube.Top, cube.Near);
			var lbf = new Vector3(cube.Left, cube.Bottom, cube.Far);

			var xN = new Vector3(1, 0, 0);
			var yN = new Vector3(0, 1, 0);
			var zN = new Vector3(0, 0, 1);

			var list = new List<MathPlane>();
			list.Add(new MathPlane() { Normal = xN, Point = rtn });
			list.Add(new MathPlane() { Normal = xN, Point = lbf });
			list.Add(new MathPlane() { Normal = yN, Point = rtn });
			list.Add(new MathPlane() { Normal = yN, Point = lbf });
			list.Add(new MathPlane() { Normal = zN, Point = rtn });
			list.Add(new MathPlane() { Normal = zN, Point = lbf });

			return list;
		}

		public static bool IsCross(this MathCube cube, MathLine line, List<Vector3> postionList)
		{
			var planeList = cube.GetPlanes();
			var position = new Vector3();
			bool result = false;
			foreach (var plane in planeList)
			{
				if (plane.IsCross(line, ref position))
				{
					postionList.Add(position);
					result = true;
				}
			}
			return result;
		}
	}
}
