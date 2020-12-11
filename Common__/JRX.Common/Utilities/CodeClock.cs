using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace JRX.Common.Utilities
{
	#region IPerformanceCounter

	//this used to be public to support other performance counter types, but has been made internal for now.
	internal interface IPerformanceCounter
	{
		long Count
		{
			get;
		}

		long Frequency
		{
			get;
		}
	}
	#endregion

	#region Performance Counters
	internal class Win32PerformanceCounter : IPerformanceCounter
	{
		[System.Runtime.InteropServices.DllImport("KERNEL32")]
		private static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);

		[System.Runtime.InteropServices.DllImport("KERNEL32")]
		private static extern bool QueryPerformanceFrequency(ref long lpFrequency);

		public long Count
		{
			get
			{
				long count = 0;
				QueryPerformanceCounter(ref count);
				return count;
			}
		}

		public long Frequency
		{
			get
			{
				long freq = 0;
				QueryPerformanceFrequency(ref freq);
				return freq;
			}
		}
	}

	internal class DefaultPerformanceCounter : IPerformanceCounter
	{

		public long Count
		{
			get
			{
				return System.DateTime.UtcNow.Ticks;
			}
		}

		public long Frequency
		{
			get
			{
				return 10000000;    // 10 million
			}
		}
	}
	#endregion

	class CodeClock
    {
		long elapsedCount = 0;
		long startCount = 0;

		private IPerformanceCounter _clock;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeClock"/> class.
		/// </summary>
		public CodeClock()
		{
            if (Platform.IsWin32Platform)
            {
                _clock = new Win32PerformanceCounter();
            }
            else
            {
                _clock = new DefaultPerformanceCounter();
            }
           // _clock = new DefaultPerformanceCounter();

		}

		/// <summary>
		/// Starts the clock.
		/// </summary>
		public void Start()
		{
			startCount = _clock.Count;
		}


		/// <summary>
		/// Stops the clock.
		/// </summary>
		public void Stop()
		{
			long stopCount = _clock.Count;
			elapsedCount += (stopCount - startCount);
		}

		/// <summary>
		/// Clears (resets) the clock.
		/// </summary>
		public void Clear()
		{
			elapsedCount = 0;
		}

		/// <summary>
		/// Gets the number of seconds elapsed between start and stop.
		/// </summary>
		public float Seconds
		{
			get
			{
				return ((float)elapsedCount / (float)_clock.Frequency);
			}
		}

		/// <summary>
		/// Gets the number of seconds elapsed between start and stop as a formatted string.
		/// </summary>
		public override string ToString()
		{
			ResourceManager resmgr = new ResourceManager(typeof(SR));
			return String.Format(resmgr.GetString("FormatSeconds"), Seconds);
			//return String.Format(SR.FormatSeconds, Seconds); 
			//return String.Format("{0}seconds", Seconds);
		}
	}
}
