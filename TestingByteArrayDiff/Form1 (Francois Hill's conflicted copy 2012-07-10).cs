using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharedClasses;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;

namespace TestingByteArrayDiff
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		const string FolderToSyncClient = @"C:\Francois\other\TmpSync\Client";
		//const string FolderToSyncServer = @"C:\Francois\other\TmpSync\Server";

		const string diff = @"C:\Francois\other\Tmp\_1.bmp_2.bmp_.diff";
		const string file1 = @"C:\Francois\other\Tmp\1.docx";
		const string file2 = @"C:\Francois\other\Tmp\2.docx";
		//const string file1 = @"C:\Francois\other\Tmp\1.bmp";
		//const string file2 = @"C:\Francois\other\Tmp\2.bmp";
		//const string file1 = @"C:\Users\francois\AppData\Local\FJH\NSISinstaller\NSISexports\Setup_QuickAccess_1_0_0_134.exe";
		//const string file2 = @"C:\Users\francois\AppData\Local\FJH\NSISinstaller\NSISexports\Setup_QuickAccess_1_0_0_135.exe";
		private void button1_Click(object sender, EventArgs e)
		{
			BinaryDiff.MakePatch(file1, file2, diff);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string f1 = textBoxFile1.Text;
			string f2 = textBoxFile2.Text;
			string fdiff = f1 + ".diff";

			if (!File.Exists(f1))
				UserMessages.ShowWarningMessage("File1 does not exist: " + f1);
			else if (!File.Exists(f2))
				UserMessages.ShowWarningMessage("File2 does not exist: " + f2);
			else
			{
				if (File.Exists(fdiff))
					File.Delete(fdiff);
				BinaryDiff.MakePatch(f1, f2, fdiff);
				if (File.Exists(fdiff))
					Process.Start("explorer", string.Format("/select,\"{0}\"", fdiff));
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string f1 = textBoxFile1.Text;
			string fdiff = textBoxFile2.Text;
			string fpatch = Path.GetDirectoryName(f1).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(f1) + "_patched" + Path.GetExtension(f1);
			if (!File.Exists(f1))
				UserMessages.ShowWarningMessage("File1 does not exist: " + f1);
			else if (!File.Exists(fdiff))
				UserMessages.ShowWarningMessage("File2 does not exist: " + fdiff);
			else
			{
				if (File.Exists(fpatch))
					File.Delete(fpatch);
				BinaryDiff.ApplyPatch(f1, fdiff, fpatch);
				if (File.Exists(fpatch))
					Process.Start("explorer", string.Format("/select,\"{0}\"", fpatch));
			}
		}

		private void textBoxFile1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void textBoxFile1_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length == 1)
			{
				TextBox tb = sender as TextBox;
				tb.Text = files[0];
			}
			else if (files.Length > 1)
				UserMessages.ShowWarningMessage("Cannot drag-drop multiple files");
		}

		private void button4_Click(object sender, EventArgs e)
		{
			//var localfolderData = BinaryDiff.GetFolderMetaData(
			//    //@"C:\Users\francois\Dropbox",
			//    @"C:\Francois\_TestAutosyncClient",
			//    "ftp://fjh.dyndns.org",// /francois/AutoSyncServer"
			//    "User1"
			//    /*,
			//    true*/
			//    //FolderToSyncClient,
			//    //GlobalSettings.VisualStudioInteropSettings.Instance.UriProtocolForVsPublishing.ToString().ToLower() + "://" + GlobalSettings.VisualStudioInteropSettings.Instance.BaseUri + "/francois/other/TmpSync/Server"
			//);
			//List<string> patchesMade = localfolderData.GeneratePatches();
			//foreach (string p in patchesMade)
			//{ }// UserMessages.ShowInfoMessage("Patch made: " + p);

			//string UserFolderName = "User1";
			var localfolderData = new BinaryDiff.FolderData(
				@"C:\Francois\_TestAutosyncClient",
				"ftp://fjh.dyndns.org",
				"User1",
				null,
				"autosync",
				"autosyncpass123");
			var uploadPathces = localfolderData.UploadChangesToServer();
			textBoxMessages.Text += (textBoxMessages.Text.Length > 0 ? Environment.NewLine : "")
				+ DateTime.Now.ToString("HH:mm:ss") + ", " + uploadPathces.ToString();
			//var lastCached = localfolderData.GetLastCachedFolderData();

			//var remotefolderData = BinaryDiff.GetFolderMetaData(
			//    UserFolderName,
			//    false,
			//    1);

			//if (folderData == null)
			//return;
		}

		WebClient webClient;
		private void buttonUploadHttp_Click(object sender, EventArgs e)
		{
			ThreadingInterop.PerformVoidFunctionSeperateThread(() =>
			{
				try
				{
					ThreadingInterop.UpdateGuiFromThread(this, delegate { buttonCancelUpload.Enabled = true; buttonUploadHttp.Enabled = false; });

					webClient = new WebClient();
					webClient.Headers.Add("Content-Type", "binary/octet-stream");

					webClient.UploadProgressChanged += (sn, ev) =>
					{
						ThreadingInterop.UpdateGuiFromThread(this, delegate
						{
							progressBar1.Value = ev.ProgressPercentage;
							if (ev.ProgressPercentage == 100) progressBar1.Value = 0;
						});
					};
					webClient.UploadFileCompleted += (sn, ev) =>
					{
						if (!ev.Cancelled && ev.Error == null)
						{
							//MessageBox.Show("Successfully uploaded file to server (server might have sent back error).");
							string responseString = System.Text.Encoding.UTF8.GetString(ev.Result);
							if (responseString.StartsWith("1:", StringComparison.InvariantCultureIgnoreCase))
							{
								//Successfully uploaded and moved
								string successStr = responseString.Substring(2);
								MessageBox.Show("Success: " + successStr, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else if (responseString.StartsWith("0:", StringComparison.InvariantCultureIgnoreCase))
							{
								//Upload failed, error string after 0:
								string errorStr = responseString.Substring(2);
								MessageBox.Show("Error: " + errorStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else if (responseString.StartsWith("2:", StringComparison.InvariantCultureIgnoreCase))
							{
								//Unable to move file, uploaded successful
								string errorStr = responseString.Substring(2);
								MessageBox.Show("Error: " + errorStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							else
								MessageBox.Show("Unknown message from server. " + responseString);
						}
						else if (ev.Cancelled)
							MessageBox.Show("User cancelled upload");
						else if (ev.Error != null)
							MessageBox.Show("Error uploading: " + ev.Error.Message);
					};
					string filepath = @"C:\tmpfile.txt";
					//string filepath = @"C:\tmpfile2.txt";

					string rootUri = "http://localhost";
					//string rootUri = "http://fjh.dyndns.org";

					string userFolderName = "User1";
					string relativePath = "/Photos";
					webClient.UploadFileAsync(
						new Uri(string.Format("{0}/uploadautosyncserver/doupload/{1}{2}",
							rootUri,
							userFolderName,
							string.IsNullOrWhiteSpace(relativePath) ? "" : "/" + relativePath.TrimStart('/').Replace('\\', '/'))),
						filepath);

					while (webClient.IsBusy)
						Application.DoEvents();

					//string response = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
					//MessageBox.Show(response);
				}
				finally
				{
					ThreadingInterop.UpdateGuiFromThread(this, delegate { buttonCancelUpload.Enabled = false; buttonUploadHttp.Enabled = true; });
				}
			},
			true);
		}

		private void buttonCancelUpload_Click(object sender, EventArgs e)
		{
			webClient.CancelAsync();
		}
	}
}
