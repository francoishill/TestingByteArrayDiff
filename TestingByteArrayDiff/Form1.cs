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
using System.Threading;

namespace TestingByteArrayDiff
{
	public partial class Form1 : Form
	{
		List<TrayActionClass> TrayActions = new List<TrayActionClass>();
		private bool ForceClosing = false;

		private Icon originalNoChangesTrayIcon = null;
		private Icon errorTrayIcon = null;
		private Icon busyCheckingChangesTrayIcon = null;

		TextFeedbackEventHandler textFeedbackHandler;
		//ProgressChangedEventHandler progressChangedHandler;

		List<BinaryDiff.FolderData> monitoredFolders = new List<BinaryDiff.FolderData>();

		public Form1()
		{
			InitializeComponent();
			SetupNotifyIcons();
			InitializeContextMenu();
			SetupTrayActions();

			numericUpDownFtpPort.Value = BinaryDiff.AutoSyncFtpPortToUse;

			textFeedbackHandler += (s, e) =>
			{
				AppendMessage(e.FeedbackText, e.FeedbackType);
			};

			RefreshListOfMonitoredFolders(textFeedbackHandler);
			SyncNow(null, null);

			this.checkBoxTopmost.Checked = this.TopMost;
			this.checkBoxTopmost.CheckedChanged += delegate { this.TopMost = checkBoxTopmost.Checked; };

			//progressChangedHandler += (s, e) =>
			//{
			//    UpdateProgess((int)Math.Truncate((double)100 * (double)e.CurrentValue / (double)e.MaximumValue));
			//};
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			this.HideSelf();
			if (!Win32Api.RegisterHotKey(this.Handle, Win32Api.Hotkey1, Win32Api.MOD_WIN, (uint)Keys.A))
				UserMessages.ShowWarningMessage("AutoSync could not register hotkey Win + A");
			//if (!Win32Api.RegisterHotKey(this.Handle, Win32Api.Hotkey2, Win32Api.MOD_WIN + Win32Api.MOD_CONTROL, (uint)Keys.A))
			//    UserMessages.ShowWarningMessage("AutoSync could not register hotkey Win + Ctrl + A");
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Win32Api.WM_HOTKEY)
			{
				if (m.WParam == new IntPtr(Win32Api.Hotkey1))
					ToggleShowHide();

				//Took out the Hotkey2 for showing context menu, cannot use "this" as control because it is hidden
				//else if (m.WParam == new IntPtr(Win32Api.Hotkey2))
				//    notifyIconTrayIcon.ContextMenu.Show(this, new Point(Form.MousePosition.X, Form.MousePosition.Y));
			}
			base.WndProc(ref m);
		}

		private void SetupNotifyIcons()
		{
			originalNoChangesTrayIcon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("TestingByteArrayDiff.app.ico"));
			Font fontX = new System.Drawing.Font(this.Font.FontFamily, 16, FontStyle.Bold);
			Font fontAt = new System.Drawing.Font(this.Font.FontFamily, 16, FontStyle.Bold);
			errorTrayIcon = IconsInterop.GetIcon("E", Color.Transparent, Brushes.Red, fontX, originalNoChangesTrayIcon, new PointF(0, 10));
			busyCheckingChangesTrayIcon = IconsInterop.GetIcon("#", Color.Transparent, Brushes.Gray, fontAt, originalNoChangesTrayIcon, new PointF(0, 10));

			notifyIconTrayIcon.Icon = originalNoChangesTrayIcon;
		}

		private void InitializeContextMenu()
		{
			this.notifyIconTrayIcon.ContextMenu = new ContextMenu(
				new MenuItem[]
				{
					new MenuItem("S&how [Win + A]", delegate { this.ShowSelf(); }),
					new MenuItem("-"),
					new MenuItem("S&ync now", delegate { this.SyncNow(null, null); }),
					new MenuItem("-"),
					new MenuItem("E&xit", delegate { this.ForceClosing = true; this.Close(); })
				});
		}

		private string filePathToDoNotShowHiddenToTrayOnClose = SettingsInterop.GetFullFilePathInLocalAppdata("DoNotShowHiddenToTrayOnClose.fjset", BinaryDiff.ThisAppName);
		private bool DoNotShowHiddenToTrayOnClose
		{
			get { return File.Exists(filePathToDoNotShowHiddenToTrayOnClose); }
			set
			{
				if (value && !File.Exists(filePathToDoNotShowHiddenToTrayOnClose)) File.Create(filePathToDoNotShowHiddenToTrayOnClose).Close();
				else if (!value && File.Exists(filePathToDoNotShowHiddenToTrayOnClose)) File.Delete(filePathToDoNotShowHiddenToTrayOnClose);
			}
		}
		private void SetupTrayActions()
		{
			TrayActions.Add(
				new TrayActionClass(
					TrayActionClass.TrayActions.NotClosedButHiddenToTray,
					"AutoSync hidden to tray, click here to never show this message again.",
					delegate { DoNotShowHiddenToTrayOnClose = true; },
					ToolTipIcon.Info));
			TrayActions.Add(
				new TrayActionClass(
					TrayActionClass.TrayActions.NoMonitoredFoldersYet,
					"No monitored folders yet, click here to add now.",
					delegate { AddLinkedFolders(); },
					ToolTipIcon.Warning));
			//TrayActions.Add(
			//    new TrayActionClass(
			//        TrayActionClass.TrayActions.ReminderToSync,
			//        "Remember to manually sync, syncing automatically is not incorporated yet. Click here to sync now.",
			//        delegate { SyncNow(); },
			//        ToolTipIcon.Warning));
		}

		private void RefreshListOfMonitoredFolders(TextFeedbackEventHandler textFeedbackHandler)
		{
			foreach (var mf in monitoredFolders)
				mf.RemoveFolderWatcher();
			monitoredFolders.Clear();
			var monitoredFiles = Directory.GetFiles(BinaryDiff.FolderData.GetFolderForStoringMonitoredFolders(), "*" + BinaryDiff.FolderData.cMonExtension, SearchOption.AllDirectories);
			foreach (string monFile in monitoredFiles)
			{
				var registeredDetails = BinaryDiff.FolderData.GetRegisteredLocalMonitoredFolder(monFile);
				//string localFolderPath = BinaryDiff.FolderData.GetLocalMonitoredPathFromEncodedFilename(monFile);

				string metadataPath = BinaryDiff.GetZippedFilename(registeredDetails.GetMetadataFullpathLocal());
				if (!File.Exists(metadataPath))
				{
					UserMessages.ShowWarningMessage("Local folder marked to be monitored, but metadata file is missing (cannot sync this folder): " + Environment.NewLine + metadataPath);
					continue;
				}

				var tmpFolderData = new BinaryDiff.FolderData();
				BinaryDiff.FolderData.PopulateFolderDataFromZippedJson(
					tmpFolderData,
					metadataPath,
					textFeedbackHandler);
				monitoredFolders.Add(tmpFolderData);
				tmpFolderData.StartFolderWatcher(
					(folderData, changed_created_deleted) =>
					{
						SyncNow(folderData, new List<string>() { folderData.GetRelativePath(changed_created_deleted.FullPath) });
					},
					(folderData, renamed) =>
					{
						SyncNow(folderData, new List<string>() { folderData.GetRelativePath(renamed.OldFullPath), renamed.FullPath });
					});
			}

			if (monitoredFolders.Count == 0)
				ShowTrayNotification(TrayActionClass.TrayActions.NoMonitoredFoldersYet);
		}

		//private void SyncAllMonitoredFoldersNow()
		//{
		//    foreach (var folder in monitoredFolders)
		//        SyncNow(folder);
		//}

		private void button4_Click(object sender, EventArgs e)
		{
			SyncNow(null, null);
			//SyncAllMonitoredFoldersNow();
			//MessageBox.Show("Function unavailable");
			//SyncNow(textBoxLocalDir.Text);
		}

		//string serverRootUri = "ftp://fjh.dyndns.org";
		//string userfolder = "User1";
		//string username = "autosync";
		//string password = "autosyncpass123";

		//public class FolderDataAndChangedFiles
		//{
		//    public BinaryDiff.FolderData FolderData;
		//    public List<string> ChangedFiles;
		//    public FolderDataAndChangedFiles(BinaryDiff.FolderData FolderData, List<string> ChangedFiles)
		//    {
		//        this.FolderData = FolderData;
		//        this.ChangedFiles = ChangedFiles;
		//    }
		//}

		bool syncBusy = false;
		//bool isSyncQueued = false;
		public static Dictionary<BinaryDiff.FolderData, List<string>> relativePathsQueued = new Dictionary<BinaryDiff.FolderData, List<string>>();
		private void SyncNow(BinaryDiff.FolderData folderDataIn, List<string> changedRelativeFilesIn)//BinaryDiff.FolderData localfolderData)//string localpath, string serverRootUri, string username, string password)
		{
			bool isSyncingSpecificFiles = folderDataIn != null && changedRelativeFilesIn != null;
			if (isSyncingSpecificFiles)
			{
				if (!relativePathsQueued.ContainsKey(folderDataIn))
					relativePathsQueued.Add(folderDataIn, new List<string>());
				relativePathsQueued[folderDataIn].AddRange(changedRelativeFilesIn);
			}

			if (syncBusy)
				return;

			syncBusy = true;
			ThreadingInterop.PerformVoidFunctionSeperateThread(() =>
			{
				ThreadingInterop.UpdateGuiFromThread(this, delegate
				{
					buttonManuallySyncWithServer.Enabled = false;
					progressBarSyncingProgress.Visible = true;
					notifyIconTrayIcon.Icon = busyCheckingChangesTrayIcon;
				});

				bool anyError = false;

				try
				{
					//var localfolderData = new BinaryDiff.FolderData(
					//    localpath,
					//    serverRootUri,
					//    //userfolder,
					//    null,
					//    username,
					//    password);
					//localfolderData.InitialSetupLocally();

					if (isSyncingSpecificFiles)
					{
						var keyval = relativePathsQueued.ElementAt(0);
						relativePathsQueued.Remove(keyval.Key);

						keyval.Key.PopulateFilesData(keyval.Value, textFeedbackHandler);//changedRelativeFiles, textFeedbackHandler);
						var uploadPathces = keyval.Key.UploadChangesToServer(textFeedbackHandler);

						bool iserr = uploadPathces != BinaryDiff.FolderData.UploadPatchesResult.Success
							&& uploadPathces != BinaryDiff.FolderData.UploadPatchesResult.NoLocalChanges;

						AppendMessage("{" + keyval.Key.LocalFolderPath + "} " + uploadPathces.ToString(), iserr ? TextFeedbackType.Error : TextFeedbackType.Subtle);
						if (iserr)
							anyError = true;
					}
					else
						foreach (var localfolderData in monitoredFolders)
						{
							bool validDir = false;
							if (!Directory.Exists(localfolderData.LocalFolderPath))
							{
								try
								{
									Directory.CreateDirectory(localfolderData.LocalFolderPath);
									if (Directory.Exists(localfolderData.LocalFolderPath))
										validDir = true;
								}
								catch (Exception exc) { UserMessages.ShowWarningMessage("Cannot create directory '" + localfolderData.LocalFolderPath + "': " + exc.Message); }
							}
							else
								validDir = true;

							if (!validDir)
							{
								UserMessages.ShowWarningMessage("{" + localfolderData.LocalFolderPath + "} An unknown error occurred, invalid directory '" + localfolderData.LocalFolderPath + "'");
								return;
							}

							localfolderData.PopulateFilesData(null, textFeedbackHandler);//changedRelativeFiles, textFeedbackHandler);
							var uploadPathces = localfolderData.UploadChangesToServer(textFeedbackHandler);

							bool iserr = uploadPathces != BinaryDiff.FolderData.UploadPatchesResult.Success
								&& uploadPathces != BinaryDiff.FolderData.UploadPatchesResult.NoLocalChanges;

							AppendMessage("{" + localfolderData.LocalFolderPath + "} " + uploadPathces.ToString(), iserr ? TextFeedbackType.Error : TextFeedbackType.Subtle);
							if (iserr)
								anyError = true;
						}
				}
				finally
				{
					ThreadingInterop.UpdateGuiFromThread(this, delegate
					{
						buttonManuallySyncWithServer.Enabled = true;
						progressBarSyncingProgress.Visible = false;
						if (anyError)
							notifyIconTrayIcon.Icon = errorTrayIcon;
						else
							notifyIconTrayIcon.Icon = originalNoChangesTrayIcon;
					});
				}

				if (relativePathsQueued.Count > 0)
				{
					var keyval = relativePathsQueued.ElementAt(0);
					relativePathsQueued.Remove(keyval.Key);
					//syncBusy = false;
					//SyncNow(keyval.Key, keyval.Value);
					syncBusy = false;
					SyncNow(keyval.Key, keyval.Value);
					//folderData = keyval.Key;
					//changedRelativeFiles = keyval.Value;
					//goto syncAgainIfQueued;
				}
				else
				{
					//bool hasUncopyableFiles = false;
					foreach (var localfolderData in monitoredFolders)
						if (localfolderData.HasUncopyableFilesInList())
						{
							Thread.Sleep(TimeSpan.FromSeconds(5));//Wait before syncing again

							int possibleNextLineMustBeNullAndNotEmptyList;
							SyncNow(localfolderData, new List<string>());
							
							//folderData = localfolderData;
							//changedRelativeFiles = new List<string>();
							//goto syncAgainIfQueued;
							//hasUncopyableFiles = true;
							//SyncNow(localfolderData, new List<string>());
						}
					//if (!hasUncopyableFiles)
					syncBusy = false;
					//else
					//    goto syncAgainIfQueued;
				}
			},
			false);
		}

		private void AppendMessage(string msg, TextFeedbackType feedbackType)
		{
			ThreadingInterop.UpdateGuiFromThread(this, delegate
			{
				textBoxMessages.Text +=
					(textBoxMessages.Text.Length > 0 ? Environment.NewLine : "")
					+ "[" + DateTime.Now.ToString("HH:mm:ss") + "] "
					+ (feedbackType == TextFeedbackType.Error ? "[ERROR] " : "")
					+ msg;
				textBoxMessages.SelectionStart = textBoxMessages.Text.Length;
				textBoxMessages.SelectionLength = 0;
				textBoxMessages.ScrollToCaret();
			});
		}

		/*private void UpdateProgess(int percentage)
		{
			Have to progressBarSyncingProgress style to continuous
			ThreadingInterop.UpdateGuiFromThread(this, delegate
			{
				if (progressBarSyncingProgress.Value != percentage)
				{
					progressBarSyncingProgress.Value = percentage;
					Application.DoEvents();
				}
				if (percentage == 100)
				{
					progressBarSyncingProgress.Value = 0;
					//labelStatusMessage.Text = "Completed";
				}
			});
		}*/

		private void buttonAddLinkedFolders_Click(object sender, EventArgs e)
		{
			AddLinkedFolders();
		}

		private void AddLinkedFolders()
		{
			NewLinkedFolder tmpform = new NewLinkedFolder();
			tmpform.RemoveAllUsercontrols();
			foreach (var mf in monitoredFolders)
			{
				var uc = tmpform.AddAnotherLinkedFolder(mf.LocalFolderPath, mf.ServerRootUri, mf.FtpUsername, mf.FtpPassword);
				uc.Tag = mf;
			}
			if (tmpform.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				foreach (LinkedFolderUserControl uc in tmpform.GetUsercontrols())
				{
					var linkedFolderTag = uc.Tag as BinaryDiff.FolderData;
					if (linkedFolderTag != null)
					{
						if (linkedFolderTag.LocalFolderPath == uc.textBoxLocalRootDirectory.Text
							&& linkedFolderTag.ServerRootUri == uc.textBoxFtpRootUrl.Text
							&& linkedFolderTag.FtpUsername == uc.textBoxFtpUsername.Text
							&& linkedFolderTag.FtpPassword == uc.textBoxFtpPassword.Text
							//&& LinkedFolderToFtp.AreStringListsEqual(linkedFolderTag.ExcludedRelativeFolders, uc.textBoxExcludedFolders.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim('\\')).ToList())
							)
							continue;
						else
						{
							UserMessages.ShowWarningMessage("It is not currently supported to modify settings of existing monitored folders");
							continue;
						}
					}

					//bool isNew = false;
					if (linkedFolderTag == null)//This was an added linked folder (NOT changed)
					{
						//isNew = true;
						linkedFolderTag = new BinaryDiff.FolderData();
					}
					linkedFolderTag.LocalFolderPath = uc.textBoxLocalRootDirectory.Text;
					linkedFolderTag.ServerRootUri = uc.textBoxFtpRootUrl.Text;
					linkedFolderTag.FtpUsername = uc.textBoxFtpUsername.Text;
					linkedFolderTag.FtpPassword = uc.textBoxFtpPassword.Text;
					if (!linkedFolderTag.InitiateSyncing(textFeedbackHandler))
						UserMessages.ShowWarningMessage("Unable to initiate sync for local folder '" + linkedFolderTag.LocalFolderPath + "'");
					else
					{
						linkedFolderTag.RegisterAsMonitoredPath();
						AppendMessage("Successfully linked folder: " + linkedFolderTag.LocalFolderPath, TextFeedbackType.Success);
						//Process.Start("explorer", "/select,\"" + linkedFolderTag.LocalFolderPath + "\"");
						Process.Start(linkedFolderTag.LocalFolderPath);
					}
				}
				foreach (object removedFolders in tmpform.RemovedUsercontrolNonNullTags)
				{
					BinaryDiff.FolderData linkedFol = removedFolders as BinaryDiff.FolderData;
					if (linkedFol == null) continue;
					linkedFol.UnregisterAsMonitoredPath();
				}
				RefreshListOfMonitoredFolders(textFeedbackHandler);
			}
		}

		private void buttonChooseLocalFolder_Click(object sender, EventArgs e)
		{

		}

		#region Other unused

		const string diff = @"C:\Francois\other\Tmp\_1.bmp_2.bmp_.diff";
		const string file1 = @"C:\Francois\other\Tmp\1.docx";
		const string file2 = @"C:\Francois\other\Tmp\2.docx";
		//const string file1 = @"C:\Francois\other\Tmp\1.bmp";
		//const string file2 = @"C:\Francois\other\Tmp\2.bmp";
		//const string file1 = @"C:\Users\francois\AppData\Local\FJH\NSISinstaller\NSISexports\Setup_QuickAccess_1_0_0_134.exe";
		//const string file2 = @"C:\Users\francois\AppData\Local\FJH\NSISinstaller\NSISexports\Setup_QuickAccess_1_0_0_135.exe";
		private void button1_Click(object sender, EventArgs e)
		{
			/*xDelta3Interop.MakePatch(file1, file2, diff, textFeedbackHandler);*/
		}

		private void button2_Click(object sender, EventArgs e)
		{
			/*string f1 = textBoxFile1.Text;
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
				xDelta3Interop.MakePatch(f1, f2, fdiff, textFeedbackHandler);
				if (File.Exists(fdiff))
					Process.Start("explorer", string.Format("/select,\"{0}\"", fdiff));
			}*/
		}

		private void button3_Click(object sender, EventArgs e)
		{
			/*string f1 = textBoxFile1.Text;
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
				xDelta3Interop.ApplyPatch(f1, fdiff, fpatch, textFeedbackHandler);
				if (File.Exists(fpatch))
					Process.Start("explorer", string.Format("/select,\"{0}\"", fpatch));
			}*/
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

		private void buttonCancelUpload_Click(object sender, EventArgs e)
		{
			//webClient.CancelAsync();
		}

		//WebClient webClient;
		private void buttonUploadHttp_Click(object sender, EventArgs e)
		{
			UserMessages.ShowInfoMessage("This was a temporary function and has been disabled");
			/*ThreadingInterop.PerformVoidFunctionSeperateThread(() =>
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
			true);*/
		}
		#endregion Other unused

		private void buttonShowManualPatchingControls_Click(object sender, EventArgs e)
		{
			UserMessages.ShowWarningMessage("This functionality is not currenlty available");
			//for (int i = 0; i < groupBoxManualPatching.Controls.Count; i++)
			//    groupBoxManualPatching.Controls[i].Visible = true;
			//buttonShowManualPatchingControls.Visible = false;
		}

		private void linkLabelHideManualPatchingControls_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < groupBoxManualPatching.Controls.Count; i++)
				groupBoxManualPatching.Controls[i].Visible = false;
			buttonShowManualPatchingControls.Visible = true;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			BinaryDiff.AutoSyncFtpPortToUse = (int)numericUpDownFtpPort.Value;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!ForceClosing && e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				if (!DoNotShowHiddenToTrayOnClose)
					ShowTrayNotification(TrayActionClass.TrayActions.NotClosedButHiddenToTray);
				HideSelf();
			}
		}

		private void HideSelf()
		{
			this.Hide();
		}
		private void ShowSelf()
		{
			this.Show();
			this.BringToFront();
			this.TopMost = !this.TopMost;
			this.TopMost = !this.TopMost;
		}

		private void ShowTrayNotification(TrayActionClass.TrayActions trayAction)
		{
			notifyIconTrayIcon.Tag = trayAction;
			foreach (var ta in TrayActions)
				if (ta.TrayAction == trayAction)
				{
					notifyIconTrayIcon.Tag = ta;
					notifyIconTrayIcon.ShowBalloonTip(3000, ta.MessageTitle ?? "AutoSync notification", ta.UserMessage, ta.Icon);
					return;
				}
		}

		private void notifyIconTrayIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			TrayActionClass ta = notifyIconTrayIcon.Tag as TrayActionClass;
			if (ta == null) return;

			ta.ActionOnBalloonClicked();
		}

		private void notifyIconTrayIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ToggleShowHide();
			}
		}

		private void ToggleShowHide()
		{
			if (this.Visible)
				this.HideSelf();
			else
				this.ShowSelf();
		}

		private void linkLabelClearMessages_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			textBoxMessages.Clear();
		}
	}

	public class TrayActionClass
	{
		public enum TrayActions { None, NoMonitoredFoldersYet, NotClosedButHiddenToTray/*, ReminderToSync*/ };

		public TrayActions TrayAction;
		public string UserMessage;
		public string MessageTitle = null;
		public Action ActionOnBalloonClicked;
		public ToolTipIcon Icon;
		public TrayActionClass(TrayActions TrayAction, string UserMessage, Action ActionOnBalloonClicked, ToolTipIcon Icon)
		{
			this.TrayAction = TrayAction;
			this.UserMessage = UserMessage;
			this.ActionOnBalloonClicked = ActionOnBalloonClicked;
			this.Icon = Icon;
		}
	}
}
