using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Rendering3D
{
	class Screen
	{
		public const int Width = 600;
		public const int Height = 400;

		private System.Windows.Controls.Image _screen;
		private WriteableBitmap _bitmap;
		private Int32Rect _bitmapRect;

		public Screen(System.Windows.Controls.Image screen, System.Windows.Shapes.Rectangle screenEdge)
		{
			_bitmap = new WriteableBitmap(CreateBitmapImage());

			_bitmapRect = new Int32Rect(0, 0, Width, Height);

			_screen = screen;
			_screen.Width = Width;
			_screen.Height = Height;
			_screen.Stretch = Stretch.Fill;
			_screen.Source = _bitmap;

			screenEdge.Width = Width;
			screenEdge.Height = Height;
		}

		public void DrawScreen()
		{
			_bitmap.Dispatcher.Invoke(new Action(() =>
			{
				_bitmap.WritePixels(_bitmapRect, RenderingPipelineRegister.Instance.ColorBuffer, Width * _bitmap.Format.BitsPerPixel / 8, 0);
				System.Threading.Thread.Sleep(1);
			}));
		}

		private BitmapImage CreateBitmapImage()
		{
			Bitmap bitmap = new Bitmap(Width, Height);
			using (var memory = new MemoryStream())
			{
				bitmap.Save(memory, ImageFormat.Png);
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();

				return bitmapImage;
			}
		}
	}
}
