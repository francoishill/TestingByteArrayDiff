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
			AutoUpdating.CheckForUpdates(
				//AutoUpdatingForm.CheckForUpdates(
				//exitApplicationAction: () => mainForm.Close(),
				ActionIfUptoDate_Versionstring: versionstring => ThreadingInterop.UpdateGuiFromThread(mainForm, () => mainForm.Text += " (up to date version " + versionstring + ")"));//,
				//ActionIfUnableToCheckForUpdates: errmsg =>ThreadingInterop.UpdateGuiFromThread(mainForm, () => mainForm.Text += " (" + errmsg + ")"),
				//ShowModally: true);

			typeof(Form).GetField("defaultIcon", BindingFlags.NonPublic | BindingFlags.Static)
				.SetValue(null, new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TestingByteArrayDiff.app.ico")));

			Application.Run(mainForm);
		}
	}
}
