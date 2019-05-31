using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rendering3D
{
	class Camera : WorldObject
	{
		public enum CameraType
		{
			Orthogonal,
			Perspective
		}

		public class OrthogonalData
		{
			public float Size;
			public float Near;
			public float Far;
		}

		public class PerspectiveData
		{
			public float FOV;
			public float Near;
			public float Far;
		}

		public CameraType Type { get; private set; }
		public OrthogonalData OrthogonalSetting { get; private set; }
		public PerspectiveData PerspectiveSetting { get; private set; }

		public void SetType(CameraType type, OrthogonalData setting)
		{
			type = Type;
			OrthogonalSetting = setting;
		}
		public void SetType(CameraType type, PerspectiveData setting)
		{
			type = Type;
			PerspectiveSetting = setting;
		}

		public Matrix GetProjectionMatrix()
		{
			switch (Type)
			{
				case CameraType.Orthogonal:
					float r = OrthogonalSetting.Size * Screen.Width * 0.5f;
					float l = -OrthogonalSetting.Size * Screen.Width * 0.5f;
					float t = OrthogonalSetting.Size * Screen.Height * 0.5f;
					float b = -OrthogonalSetting.Size * Screen.Height * 0.5f;
					float n = OrthogonalSetting.Near;
					float f = OrthogonalSetting.Far;
					return MatrixTool.CreateOrthogonalProjectionMatrix(r, l, t, b, n, f);
				case CameraType.Perspective:
					return new Matrix();
			}

			return new Matrix();
		}
	}
}
