using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingPipeline_VertexShader : IRenderingPipeline
	{
		/// <summary>
		/// World Space to View Space
		/// </summary>
		public void Execute()
		{
			var cameraPosition = RenderingPipelineRegister.Instance.Camera.Position;
			var cameraRotation = RenderingPipelineRegister.Instance.Camera.Rotation;
			var translationMatrix = MatrixTool.CreateTranslationMatrix(-cameraPosition.X, -cameraPosition.Y, -cameraPosition.Z);
			var rotationMatrix = MatrixTool.CreateRotationMatrix(-cameraRotation.X, -cameraRotation.Y, -cameraRotation.Z);
			var projectionMatrix = RenderingPipelineRegister.Instance.Camera.GetProjectionMatrix();

			foreach (var vertex in RenderingPipelineRegister.Instance.VertexList)
			{
				var position = MatrixTool.VectorToMatrix(vertex.WorldPosition);

				if (MatrixTool.TryMul(translationMatrix, position, out position))
				{
					if (MatrixTool.TryMul(rotationMatrix, position, out position))
					{
						if (MatrixTool.TryMul(projectionMatrix, position, out position))
						{
							position.TryGetValue(0, 0, out vertex.WorldPosition.X);
							position.TryGetValue(0, 1, out vertex.WorldPosition.Y);
							position.TryGetValue(0, 2, out vertex.WorldPosition.Z);
						}
					}
				}
			}
		}
	}
}
