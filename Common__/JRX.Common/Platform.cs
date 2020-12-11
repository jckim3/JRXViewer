using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRX.Common
{
	/// <summary>
	/// Defines the logging level for calls to one of the <b>Platform.Log</b> methods.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Debug log level.
		/// </summary>
		Debug,

		/// <summary>
		/// Info log level.
		/// </summary>
		Info,

		/// <summary>
		/// Warning log level.
		/// </summary>
		Warn,

		/// <summary>
		/// Error log level.
		/// </summary>
		Error,

		/// <summary>
		/// Fatal log level.
		/// </summary>
		Fatal
	}
	/// <summary>
	/// A collection of useful utility functions.
	/// </summary>
	public static class Platform
    {
		public static bool IsWin32Platform
		{
			get
			{
				PlatformID id = Environment.OSVersion.Platform;
				return (id == PlatformID.Win32NT || id == PlatformID.Win32Windows || id == PlatformID.Win32S || id == PlatformID.WinCE);
			}
		}
	}
}
