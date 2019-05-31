using System;
using System.Windows;
using System.Windows.Controls;

namespace Rendering3D
{
	public partial class MainWindow : Window
	{
		public const int UpdateInterval = 33;
		public const float PositionRangeMax = 1000;
		public const float RotationRangeMax = 180;

		private Time _time;

		private Screen _screen;
		private World _world;

		public MainWindow()
		{
			InitializeComponent();

			_world = new World();
			_screen = new Screen(_screenImage, _screenEdge);
			_time = new Time(UpdateInterval);
			_time.OnUpdateCallBack += Update;
			RenderingManager.Initialize(Screen.Width, Screen.Height, _world.Camera);

			_cameraType.ItemsSource = Enum.GetValues(typeof(Camera.CameraType));
			_cameraType.SelectedIndex = 0;

			_time.Start();
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			_time.Stop();
			base.OnClosing(e);
		}

		private void Update()
		{
			RenderingManager.Instance.DrawCall(_world.GetTotalVertexTriangle());

			_screen.DrawScreen();
		}

		private void OnPositionXChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Position.X = (float)e.NewValue * PositionRangeMax;
			_positionXLabel.Content = string.Format("X={0:0.00}", _world.Camera.Position.X);
		}
		private void OnPositionYChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Position.Y = (float)e.NewValue * PositionRangeMax;
			_positionYLabel.Content = string.Format("Y={0:0.00}", _world.Camera.Position.Y);
		}
		private void OnPositionZChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Position.Z = (float)e.NewValue * PositionRangeMax;
			_positionZLabel.Content = string.Format("Z={0:0.00}", _world.Camera.Position.Z);
		}

		private void OnRotationXChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Rotation.X = (float)e.NewValue * RotationRangeMax;
			_rotationXLabel.Content = string.Format("X={0:0.00}", _world.Camera.Rotation.X);
		}
		private void OnRotationYChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Rotation.Y = (float)e.NewValue * RotationRangeMax;
			_rotationYLabel.Content = string.Format("Y={0:0.00}", _world.Camera.Rotation.Y);
		}
		private void OnRotationZChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_world.Camera.Rotation.Z = (float)e.NewValue * RotationRangeMax;
			_rotationZLabel.Content = string.Format("Z={0:0.00}", _world.Camera.Rotation.Z);
		}

		private void OnCameraTypeChange(object sender, SelectionChangedEventArgs e)
		{
			var type = (Camera.CameraType)e.AddedItems[0];
			_cameraSetting.SelectedIndex = (int)type;
			CameraSave(type);
		}
		private void OnCameraSaveClick(object sender, RoutedEventArgs e)
		{
			CameraSave(_world.Camera.Type);
		}
		private void CameraSave(Camera.CameraType type)
		{
			switch (type)
			{
				case Camera.CameraType.Orthogonal:
					{
						Camera.OrthogonalData data = new Camera.OrthogonalData();

						data.Size = float.Parse(_orthogonalSize.Text);
						data.Near = float.Parse(_orthogonalNear.Text);
						data.Far = float.Parse(_orthogonalFar.Text);

						_world.Camera.SetType(type, data);
					}
					break;
				case Camera.CameraType.Perspective:
					{
						Camera.PerspectiveData data = new Camera.PerspectiveData();

						data.FOV = float.Parse(_perspectiveFOV.Text);
						data.Near = float.Parse(_perspectiveNear.Text);
						data.Far = float.Parse(_perspectiveFar.Text);

						_world.Camera.SetType(type, data);
					}
					break;
			}
		}
	}
}
