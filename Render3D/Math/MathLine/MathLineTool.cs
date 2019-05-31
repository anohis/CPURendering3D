using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
    static class MathLineTool
    {
		public static MathLine CreateLine(Vector3 v1, Vector3 v2)
		{
			MathLine line = new MathLine();

			return line;
		}
		public static Vector3 GetPointOnLine(this MathLine line)
		{
			return line.Point + (float)MathTool.Random() * line.Direction;
		}
    }
}
