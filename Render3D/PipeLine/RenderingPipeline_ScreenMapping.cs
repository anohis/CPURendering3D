using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingPipeline_ScreenMapping : IRenderingPipeline
	{
		public void Execute()
		{
			var screenMappingMatrix = MatrixTool.CreateScreenMappingMatrix(RenderingPipelineRegister.Instance.Width, RenderingPipelineRegister.Instance.Height);

			foreach (var vertex in RenderingPipelineRegister.Instance.VertexList)
			{
				var position = MatrixTool.VectorToMatrix(vertex.WorldPosition);
				if (MatrixTool.TryMul(screenMappingMatrix, position, out position))
				{
					position.TryGetValue(0, 0, out vertex.WorldPosition.X);
					position.TryGetValue(0, 1, out vertex.WorldPosition.Y);
					position.TryGetValue(0, 2, out vertex.WorldPosition.Z);
				}
			}
		}
	}
}
