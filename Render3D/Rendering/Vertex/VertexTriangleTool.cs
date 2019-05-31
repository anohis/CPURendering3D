using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	static class VertexTriangleTool
	{
		public static void GetMinRect(this VertexTriangle triangle, out int minX, out int maxX, out int minY, out int maxY)
		{
			minX = int.MaxValue;
			maxX = int.MinValue;
			minY = int.MaxValue;
			maxY = int.MinValue;

			foreach (var v in triangle.Vertexs)
			{
				MathTool.CompareMinMax(v.WorldPosition.X, ref minX, ref maxX);
				MathTool.CompareMinMax(v.WorldPosition.Y, ref minY, ref maxY);
			}
		}
		public static void InterpolateWeight(this VertexTriangle triangle, Vector3 screenPosition, out float w1, out float w2, out float w3)
		{
			Vector3 v3Tov2 = triangle.Vertexs[1].WorldPosition - triangle.Vertexs[2].WorldPosition;
			Vector3 v2Tov1 = triangle.Vertexs[0].WorldPosition - triangle.Vertexs[1].WorldPosition;
			Vector3 v1Tov3 = triangle.Vertexs[2].WorldPosition - triangle.Vertexs[0].WorldPosition;
			Vector3 v3ToP = screenPosition - triangle.Vertexs[2].WorldPosition;

			w1 = (v3Tov2.Y * v3ToP.X + -v3Tov2.X * v3ToP.Y) / (v3Tov2.Y * -v1Tov3.X + -v3Tov2.X * -v1Tov3.Y);
			w2 = (v1Tov3.Y * v3ToP.X + -v1Tov3.X * v3ToP.Y) / (v3Tov2.Y * -v1Tov3.X + -v3Tov2.X * -v1Tov3.Y);
			w3 = 1 - w1 - w2;
		}
		public static Vector2 InterpolateUV(this VertexTriangle triangle, Vector3 screenPosition)
		{
			float w1, w2, w3;
			triangle.InterpolateWeight(screenPosition, out w1, out w2, out w3);

			return w1 * triangle.Vertexs[0].UV + w2 * triangle.Vertexs[1].UV + w3 * triangle.Vertexs[2].UV;
		}
		public static Color InterpolateColor(this VertexTriangle triangle, Vector3 screenPosition)
		{
			float w1, w2, w3;
			triangle.InterpolateWeight(screenPosition, out w1, out w2, out w3);

			float colorR = w1 * triangle.Vertexs[0].Color.R + w2 * triangle.Vertexs[1].Color.R + w3 * triangle.Vertexs[2].Color.R;
			float colorG = w1 * triangle.Vertexs[0].Color.G + w2 * triangle.Vertexs[1].Color.G + w3 * triangle.Vertexs[2].Color.G;
			float colorB = w1 * triangle.Vertexs[0].Color.B + w2 * triangle.Vertexs[1].Color.B + w3 * triangle.Vertexs[2].Color.B;

			return Color.FromArgb((int)colorR, (int)colorG, (int)colorB);
		}
		public static bool IsContain(this VertexTriangle triangle, Vector3 screenPosition)
		{
			double d1 = MathTool.Sign(screenPosition - triangle.Vertexs[1].WorldPosition, triangle.Vertexs[0].WorldPosition - triangle.Vertexs[1].WorldPosition);
			double d2 = MathTool.Sign(screenPosition - triangle.Vertexs[2].WorldPosition, triangle.Vertexs[1].WorldPosition - triangle.Vertexs[2].WorldPosition);
			double d3 = MathTool.Sign(screenPosition - triangle.Vertexs[0].WorldPosition, triangle.Vertexs[2].WorldPosition - triangle.Vertexs[0].WorldPosition);

			bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
			bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

			return !(hasNeg && hasPos);
		}
		public static VertexTriangle Clone(this VertexTriangle triangle)
		{
			VertexTriangle clone = new VertexTriangle();
			for (int i = 0; i < triangle.Vertexs.Length; i++)
			{
				var v = triangle.Vertexs[i].Clone();
				v.Owner = clone;
				clone.Vertexs[i] = v;
			}
			return clone;
		}
		public static void SetVertexs(this VertexTriangle triangle, List<Vertex> list)
		{
			if (list.Count != 3)
			{
				return;
			}

			for (int i = 0; i < 3; i++)
			{
				list[i].Owner = triangle;
				triangle.Vertexs[i] = list[i];
			}
		}

		public static void GetInCubeVertexs(this VertexTriangle triangle, List<Vertex> outVertex)
		{
			foreach (var vertex in triangle.Vertexs)
			{
				if (MathTool.IsInCube(vertex.WorldPosition, 1, -1, 1, -1, 1, -1) == false)
				{
					outVertex.Add(vertex);


				}
			}
		}
	}
}