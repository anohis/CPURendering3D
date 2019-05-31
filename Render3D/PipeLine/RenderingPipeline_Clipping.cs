using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingPipeline_Clipping : IRenderingPipeline
	{
		private List<Vertex> _outVertexs = new List<Vertex>();

		public void Execute()
		{
			_outVertexs.Clear();

			foreach (var vertex in RenderingPipelineRegister.Instance.VertexList)
			{
				if (MathTool.IsInCube(vertex.WorldPosition, 1, -1, 1, -1, 1, -1) == false)
				{
					_outVertexs.Add(vertex);
				}
			}


		}
	}
}
