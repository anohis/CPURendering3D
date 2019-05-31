using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class Vertex
	{
		public VertexTriangle Owner;
		public Vector3 WorldPosition;
		public Color Color;
		public Vector2 UV;

		public Vertex Clone()
		{
			Vertex clone = new Vertex();
			clone.WorldPosition = WorldPosition;
			clone.Color = Color;
			clone.UV = UV;

			return clone;
		}
	}
}
