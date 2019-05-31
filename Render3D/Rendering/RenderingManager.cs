using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingManager
	{
		public static RenderingManager Instance { get; private set; }

		private List<IRenderingPipeline> _pipeline;

		public static void Initialize(int width, int heigh,Camera camera)
		{
			RenderingPipelineRegister.Initialize(width, heigh, camera);

			Instance = new RenderingManager();
		}

		public void DrawCall(IEnumerable<VertexTriangle> vertexTriangleList)
		{
			RenderingPipelineRegister.Instance.Reset();
			RenderingPipelineRegister.Instance.SetVertexTriangle(vertexTriangleList);

			foreach (var stage in _pipeline)
			{
				stage.Execute();
			}
		}

		private RenderingManager()
		{
			_pipeline = new List<IRenderingPipeline>();
			_pipeline.Add(new RenderingPipeline_VertexShader());
			_pipeline.Add(new RenderingPipeline_ScreenMapping());
			_pipeline.Add(new RenderingPipeline_PixelShader());
		}
	}
}
