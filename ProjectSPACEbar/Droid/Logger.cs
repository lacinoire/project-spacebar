using Android.Util;
using ProjectSPACEbar;
using Xamarin.Forms;

namespace ProjectSPACEbar.Droid
{
	public class Logger : ILogger
	{
		const string TAG = "Project: SPACEbar";

		public void Debug(string text, params object[] args)
		{
			Log.Debug(TAG, string.Format(text, args));
		}

		public void Error(string text, params object[] args)
		{
			Log.Error(TAG, string.Format(text, args));
		}

		public void Fatal(string text, params object[] args)
		{
			Log.Wtf(TAG, string.Format(text, args));
		}

		public void Info(string text, params object[] args)
		{
			Log.Info(TAG, string.Format(text, args));
		}

		public void Trace(string text, params object[] args)
		{
			Log.Verbose(TAG, string.Format(text, args));
		}

		public void Warn(string text, params object[] args)
		{
			Log.Warn(TAG, string.Format(text, args));
		}
	}
}