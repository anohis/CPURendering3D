using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class RenderingPipelineRegister
	{
		public static RenderingPipelineRegister Instance { get; private set; }

		public IEnumerable<Vertex> VertexList { get { return _vertexList; } }
		public IEnumerable<VertexTriangle> TriangleList { get { return _triangleList; } }

		public Camera Camera { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public byte[] ColorBuffer { get; private set; }
		public byte[] DepthBuffer { get; private set; }

		private int _halfWidth { get { return Width / 2; } }
		private int _halfHeight { get { return Height / 2; } }
		private List<VertexTriangle> _triangleList;
		private List<Vertex> _vertexList;

		public static void Initialize(int width, int heigh, Camera camera)
		{
			Instance = new RenderingPipelineRegister();

			Instance.Width = width;
			Instance.Height = heigh;

			Instance.Camera = camera;

			Instance.Reset();
		}

		public void Reset()
		{
			_triangleList?.Clear();
			_vertexList?.Clear();
			ColorBuffer = new byte[ColorConst.ChannelCount * Width * Height];
			DepthBuffer = new byte[ColorConst.ChannelCount * Width * Height];
		}
		public void SetVertexTriangle(IEnumerable<VertexTriangle> list)
		{
			_triangleList = new List<VertexTriangle>();
			_vertexList = new List<Vertex>();

			foreach (var triangle in list)
			{
				var clone = triangle.Clone();
				_triangleList.Add(clone);
				_vertexList.AddRange(clone.Vertexs);
			}
		}
		public void WriteColor(System.Drawing.Color color, int x, int y)
		{
			if (CheckRange(x, y) == false)
			{
				return;
			}

			int index = XYtoIndex(x, y);

			ColorBuffer[index + (int)ColorConst.ColorChannel.R] = color.R;
			ColorBuffer[index + (int)ColorConst.ColorChannel.B] = color.B;
			ColorBuffer[index + (int)ColorConst.ColorChannel.G] = color.G;
			ColorBuffer[index + (int)ColorConst.ColorChannel.A] = color.A;
		}

		private RenderingPipelineRegister()
		{

		}
		private int XYtoIndex(int x, int y)
		{
			x += _halfWidth;
			y += _halfHeight;

			return ColorConst.ChannelCount * (y * Width + x);
		}
		private bool CheckRange(int x, int y)
		{
			return MathTool.IsInRange(-_halfWidth, _halfWidth, x, true, false) 
				&& MathTool.IsInRange(-_halfHeight, _halfHeight, y, true, false);
		}
	}
}
