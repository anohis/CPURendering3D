using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class Triangle : RenderingObject
	{
		private Vector3 _scale = new Vector3(100, 100, 100);

		private readonly float _unitHeight = (float)Math.Pow(3.0f, 0.5f) / 2.0f;

		public Triangle()
		{
			List<Vertex> vertex = new List<Vertex>();
			vertex.Add(new Vertex() { WorldPosition = new Vector3(0, 0, 100), Color = Color.Green });
			vertex.Add(new Vertex() { WorldPosition = new Vector3(_scale.X, 0, 100), Color = Color.Red });
			vertex.Add(new Vertex() { WorldPosition = new Vector3(_scale.X / 2, _unitHeight * _scale.Y, 100), Color = Color.Blue });

			Triangles = new VertexTriangle[1];
			Triangles[0] = new VertexTriangle();
			Triangles[0].SetVertexs(vertex);
		}
	}
}
