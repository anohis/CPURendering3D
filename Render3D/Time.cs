using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rendering3D
{
	class Time
	{
		public event Action OnUpdateCallBack;

		private Thread _thread;
		private bool _isStop;
		private int _updateInterval;

		public Time(int updateInterval)
		{
			_isStop = true;
			_updateInterval = updateInterval;
			_thread = new Thread(Execute);
		}
		public void Start()
		{
			_isStop = false;
			_thread.Start();
		}
		public void Stop()
		{
			_isStop = true;
		}

		private void Execute()
		{
			while (_isStop == false)
			{
				Thread.Sleep(_updateInterval);
				OnUpdateCallBack?.Invoke();
			}
		}
	}
}
