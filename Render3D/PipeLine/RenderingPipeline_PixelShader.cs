using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingPipeline_PixelShader : IRenderingPipeline
	{
		public void Execute()
		{
			var triangles = RenderingPipelineRegister.Instance.TriangleList;

			foreach (var triangle in triangles)
			{
				int minX, maxX, minY, maxY;
				triangle.GetMinRect(out minX, out maxX, out minY, out maxY);
				for (int x = minX; x <= maxX; x++)
				{
					for (int y = minY; y <= maxY; y++)
					{
						Vector3 screenPoint = new Vector3(x, y, 0);
						if (triangle.IsContain(screenPoint))
						{
							System.Drawing.Color color = triangle.InterpolateColor(screenPoint);

							RenderingPipelineRegister.Instance.WriteColor(color, x, y);
						}
					}
				}
			}
		}
	}
}
