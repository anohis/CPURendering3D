using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	static class MatrixTool
	{
		public static bool IsSizeEqual(Matrix a, Matrix b)
		{
			return IsWidthEqual(a, b) && IsHeightEqual(a, b);
		}
		public static bool IsWidthEqual(Matrix a, Matrix b)
		{
			return a.Width == b.Width;
		}
		public static bool IsHeightEqual(Matrix a, Matrix b)
		{
			return a.Height == b.Height;
		}

		public static bool TryAdd(Matrix a, Matrix b, out Matrix v)
		{
			if (IsSizeEqual(a, b) == false)
			{
				v = new Matrix(0, 0);
				return false;
			}

			v = new Matrix(a.Width, a.Height);

			double valueA;
			double valueB;

			for (int x = 0; x < a.Width; x++)
			{
				for (int y = 0; y < a.Height; y++)
				{
					if (a.TryGetValue(x, y, out valueA) && b.TryGetValue(x, y, out valueB))
					{
						v.TrySetValue(x, y, valueA + valueB);
					}
				}
			}

			return true;
		}
		public static bool TrySub(Matrix a, Matrix b, out Matrix v)
		{
			if (IsSizeEqual(a, b) == false)
			{
				v = new Matrix(0, 0);
				return false;
			}

			v = new Matrix(a.Width, a.Height);

			double valueA;
			double valueB;

			for (int x = 0; x < a.Width; x++)
			{
				for (int y = 0; y < a.Height; y++)
				{
					if (a.TryGetValue(x, y, out valueA) && b.TryGetValue(x, y, out valueB))
					{
						v.TrySetValue(x, y, valueA - valueB);
					}
				}
			}

			return true;
		}
		public static bool TryMul(Matrix a, Matrix b, out Matrix v)
		{
			if (a.Width != b.Height)
			{
				v = new Matrix(0, 0);
				return false;
			}

			v = new Matrix(b.Width, a.Height);

			for (int vX = 0; vX < v.Width; vX++)
			{
				for (int vY = 0; vY < v.Height; vY++)
				{
					double sum = 0;
					double valueA = 0;
					double valueB = 0;
					for (int i = 0; i < a.Width; i++)
					{
						if (a.TryGetValue(i, vY, out valueA) && b.TryGetValue(vX, i, out valueB))
						{
							sum += valueA * valueB;
						}
					}
					v.TrySetValue(vX, vY, sum);
				}
			}

			return true;
		}
		public static bool TryMul(Matrix a, float b, out Matrix v)
		{
			v = new Matrix(a.Width, a.Height);

			for (int x = 0; x < v.Width; x++)
			{
				for (int y = 0; y < v.Height; y++)
				{
					double valueA = 0;
					if (a.TryGetValue(x, y, out valueA))
					{
						v.TrySetValue(x, y, valueA * b);
					}
				}
			}

			return true;
		}

		public static Matrix VectorToMatrix(Vector3 v)
		{
			Matrix m = new Matrix(1, 4);
			m.TrySetValue(0, 0, v.X);
			m.TrySetValue(0, 1, v.Y);
			m.TrySetValue(0, 2, v.Z);
			m.TrySetValue(0, 3, 1);
			return m;
		}

		public static Matrix CreateTranslationMatrix(float x, float y, float z)
		{
			Matrix m = new Matrix(4, 4);
			m.TrySetValue(0, 0, 1);
			m.TrySetValue(1, 1, 1);
			m.TrySetValue(2, 2, 1);
			m.TrySetValue(3, 3, 1);

			m.TrySetValue(3, 0, x);
			m.TrySetValue(3, 1, y);
			m.TrySetValue(3, 2, z);

			return m;
		}

		public static Matrix CreateRotationMatrix(float x, float y, float z)
		{
			Matrix xR = CreateRotationXMatrix(x);
			Matrix yR = CreateRotationYMatrix(y);
			Matrix zR = CreateRotationZMatrix(z);

			Matrix m;
			TryMul(xR, yR, out m);
			TryMul(m, zR, out m);

			return m;
		}
		public static Matrix CreateRotationXMatrix(float angle)
		{
			var cos = MathTool.Cos(angle);
			var sin = MathTool.Sin(angle);

			Matrix m = new Matrix(4, 4);
			m.TrySetValue(0, 0, 1);
			m.TrySetValue(1, 1, cos);
			m.TrySetValue(2, 1, -sin);
			m.TrySetValue(2, 2, cos);
			m.TrySetValue(1, 2, sin);
			m.TrySetValue(3, 3, 1);

			return m;
		}
		public static Matrix CreateRotationYMatrix(float angle)
		{
			var cos = MathTool.Cos(angle);
			var sin = MathTool.Sin(angle);

			Matrix m = new Matrix(4, 4);
			m.TrySetValue(0, 0, cos);
			m.TrySetValue(2, 0, sin);
			m.TrySetValue(1, 1, 1);
			m.TrySetValue(0, 2, -sin);
			m.TrySetValue(2, 2, cos);
			m.TrySetValue(3, 3, 1);

			return m;
		}
		public static Matrix CreateRotationZMatrix(float angle)
		{
			var cos = MathTool.Cos(angle);
			var sin = MathTool.Sin(angle);

			Matrix m = new Matrix(4, 4);
			m.TrySetValue(0, 0, cos);
			m.TrySetValue(1, 0, -sin);
			m.TrySetValue(0, 1, sin);
			m.TrySetValue(1, 1, cos);
			m.TrySetValue(2, 2, 1);
			m.TrySetValue(3, 3, 1);

			return m;
		}

		public static Matrix CreateScaleMatrix(float x, float y, float z)
		{
			Matrix m = new Matrix(4, 4);

			m.TrySetValue(0, 0, x);
			m.TrySetValue(1, 1, y);
			m.TrySetValue(2, 2, z);
			m.TrySetValue(3, 3, 1);

			return m;
		}

		public static Matrix CreateOrthogonalProjectionMatrix(float r, float l, float t, float b, float n, float f)
		{
			Vector3 center = new Vector3();
			center.X = (r + l) * 0.5f;
			center.Y = (t + b) * 0.5f;
			center.Z = (n + f) * 0.5f;

			Matrix translationMatrix = CreateTranslationMatrix(-center.X, -center.Y, -center.Z);
			Matrix scaleMatrix = CreateScaleMatrix(2 / (r - l), 2 / (t - b), 2 / (f - n));

			Matrix m;
			TryMul(scaleMatrix, translationMatrix, out m);

			return m;
		}

		public static Matrix CreateScreenMappingMatrix(int width,int height)
		{
			return CreateScaleMatrix(width / 2, height / 2, 1);
		}
	}
}
