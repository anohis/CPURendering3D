using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rendering3D
{
	class World
	{
		public Camera Camera { get; private set; }

		private List<RenderingObject> _objs = new List<RenderingObject>();

		public World()
		{
			Camera = new Camera();
			Camera.Position = new Vector3(0,0,0);
			Camera.Rotation = new Vector3(0,0,0);
			_objs.Add(new Triangle());
		}

		public List<VertexTriangle> GetTotalVertexTriangle()
		{
			List<VertexTriangle> output = new List<VertexTriangle>();

			foreach (var v in _objs)
			{
				output.AddRange(v.Triangles);
			}

			return output;
		}
	}
}
