using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharedClasses;
using System.Reflection;
using System.Drawing;

namespace TestingByteArrayDiff
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Form mainForm = new Form1();
			AutoUpdatingForm.CheckForUpdates(
				delegate { mainForm.Close(); },
				true,
				(versionstring) => mainForm.Text += " (up to date version " + versionstring + ")");

			typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static)
				.SetValue(null, new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TestingByteArrayDiff.app.ico")));

			Application.Run(mainForm);
		}
	}
}
