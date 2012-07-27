namespace TestingByteArrayDiff
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.textBoxFile1 = new System.Windows.Forms.TextBox();
			this.textBoxFile2 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.buttonManuallySyncWithServer = new System.Windows.Forms.Button();
			this.buttonUploadHttp = new System.Windows.Forms.Button();
			this.buttonCancelUpload = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.textBoxMessages = new System.Windows.Forms.TextBox();
			this.textBoxLocalDir = new System.Windows.Forms.TextBox();
			this.buttonChooseLocalFolder = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBarSyncingProgress = new System.Windows.Forms.ProgressBar();
			this.buttonAddLinkedFolders = new System.Windows.Forms.Button();
			this.linkLabelClearMessages = new System.Windows.Forms.LinkLabel();
			this.groupBoxAutoSyncing = new System.Windows.Forms.GroupBox();
			this.numericUpDownFtpPort = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBoxManualPatching = new System.Windows.Forms.GroupBox();
			this.linkLabelHideManualPatchingControls = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonShowManualPatchingControls = new System.Windows.Forms.Button();
			this.notifyIconTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.checkBoxTopmost = new System.Windows.Forms.CheckBox();
			this.groupBoxAutoSyncing.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFtpPort)).BeginInit();
			this.groupBoxManualPatching.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(545, 102);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBoxFile1
			// 
			this.textBoxFile1.AllowDrop = true;
			this.textBoxFile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxFile1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textBoxFile1.Location = new System.Drawing.Point(73, 29);
			this.textBoxFile1.Name = "textBoxFile1";
			this.textBoxFile1.Size = new System.Drawing.Size(312, 20);
			this.textBoxFile1.TabIndex = 2;
			this.textBoxFile1.Visible = false;
			this.textBoxFile1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFile1_DragDrop);
			this.textBoxFile1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFile1_DragEnter);
			// 
			// textBoxFile2
			// 
			this.textBoxFile2.AllowDrop = true;
			this.textBoxFile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFile2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxFile2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textBoxFile2.Location = new System.Drawing.Point(73, 55);
			this.textBoxFile2.Name = "textBoxFile2";
			this.textBoxFile2.Size = new System.Drawing.Size(312, 20);
			this.textBoxFile2.TabIndex = 3;
			this.textBoxFile2.Visible = false;
			this.textBoxFile2.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFile1_DragDrop);
			this.textBoxFile2.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFile1_DragEnter);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button2.Location = new System.Drawing.Point(9, 81);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Create diff";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button3.Location = new System.Drawing.Point(372, 81);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "Make patch";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// buttonManuallySyncWithServer
			// 
			this.buttonManuallySyncWithServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonManuallySyncWithServer.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonManuallySyncWithServer.Location = new System.Drawing.Point(9, 37);
			this.buttonManuallySyncWithServer.Name = "buttonManuallySyncWithServer";
			this.buttonManuallySyncWithServer.Size = new System.Drawing.Size(113, 23);
			this.buttonManuallySyncWithServer.TabIndex = 6;
			this.buttonManuallySyncWithServer.Text = "Manually sync now";
			this.buttonManuallySyncWithServer.UseVisualStyleBackColor = true;
			this.buttonManuallySyncWithServer.Click += new System.EventHandler(this.button4_Click);
			// 
			// buttonUploadHttp
			// 
			this.buttonUploadHttp.Location = new System.Drawing.Point(443, 46);
			this.buttonUploadHttp.Name = "buttonUploadHttp";
			this.buttonUploadHttp.Size = new System.Drawing.Size(136, 23);
			this.buttonUploadHttp.TabIndex = 7;
			this.buttonUploadHttp.Text = "Upload file via Http";
			this.buttonUploadHttp.UseVisualStyleBackColor = true;
			this.buttonUploadHttp.Visible = false;
			this.buttonUploadHttp.Click += new System.EventHandler(this.buttonUploadHttp_Click);
			// 
			// buttonCancelUpload
			// 
			this.buttonCancelUpload.Location = new System.Drawing.Point(443, 76);
			this.buttonCancelUpload.Name = "buttonCancelUpload";
			this.buttonCancelUpload.Size = new System.Drawing.Size(106, 23);
			this.buttonCancelUpload.TabIndex = 8;
			this.buttonCancelUpload.Text = "Cancel upload";
			this.buttonCancelUpload.UseVisualStyleBackColor = true;
			this.buttonCancelUpload.Visible = false;
			this.buttonCancelUpload.Click += new System.EventHandler(this.buttonCancelUpload_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(443, 105);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(168, 16);
			this.progressBar1.TabIndex = 9;
			this.progressBar1.Visible = false;
			// 
			// textBoxMessages
			// 
			this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxMessages.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textBoxMessages.Location = new System.Drawing.Point(6, 82);
			this.textBoxMessages.Multiline = true;
			this.textBoxMessages.Name = "textBoxMessages";
			this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxMessages.Size = new System.Drawing.Size(528, 221);
			this.textBoxMessages.TabIndex = 10;
			this.textBoxMessages.WordWrap = false;
			// 
			// textBoxLocalDir
			// 
			this.textBoxLocalDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLocalDir.Location = new System.Drawing.Point(503, 20);
			this.textBoxLocalDir.Name = "textBoxLocalDir";
			this.textBoxLocalDir.Size = new System.Drawing.Size(0, 20);
			this.textBoxLocalDir.TabIndex = 12;
			this.textBoxLocalDir.Text = "c:\\_AutoSync";
			this.textBoxLocalDir.Visible = false;
			// 
			// buttonChooseLocalFolder
			// 
			this.buttonChooseLocalFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonChooseLocalFolder.Location = new System.Drawing.Point(430, 18);
			this.buttonChooseLocalFolder.Name = "buttonChooseLocalFolder";
			this.buttonChooseLocalFolder.Size = new System.Drawing.Size(28, 23);
			this.buttonChooseLocalFolder.TabIndex = 13;
			this.buttonChooseLocalFolder.Text = "...";
			this.buttonChooseLocalFolder.UseVisualStyleBackColor = true;
			this.buttonChooseLocalFolder.Visible = false;
			this.buttonChooseLocalFolder.Click += new System.EventHandler(this.buttonChooseLocalFolder_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(440, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Local path:";
			this.label1.Visible = false;
			// 
			// progressBarSyncingProgress
			// 
			this.progressBarSyncingProgress.Location = new System.Drawing.Point(10, 66);
			this.progressBarSyncingProgress.MarqueeAnimationSpeed = 30;
			this.progressBarSyncingProgress.Name = "progressBarSyncingProgress";
			this.progressBarSyncingProgress.Size = new System.Drawing.Size(112, 10);
			this.progressBarSyncingProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBarSyncingProgress.TabIndex = 15;
			this.progressBarSyncingProgress.Visible = false;
			// 
			// buttonAddLinkedFolders
			// 
			this.buttonAddLinkedFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddLinkedFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonAddLinkedFolders.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonAddLinkedFolders.Location = new System.Drawing.Point(433, 37);
			this.buttonAddLinkedFolders.Name = "buttonAddLinkedFolders";
			this.buttonAddLinkedFolders.Size = new System.Drawing.Size(101, 23);
			this.buttonAddLinkedFolders.TabIndex = 16;
			this.buttonAddLinkedFolders.Text = "&Add linked folders";
			this.buttonAddLinkedFolders.UseVisualStyleBackColor = true;
			this.buttonAddLinkedFolders.Click += new System.EventHandler(this.buttonAddLinkedFolders_Click);
			// 
			// linkLabelClearMessages
			// 
			this.linkLabelClearMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelClearMessages.AutoSize = true;
			this.linkLabelClearMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabelClearMessages.Location = new System.Drawing.Point(453, 66);
			this.linkLabelClearMessages.Name = "linkLabelClearMessages";
			this.linkLabelClearMessages.Size = new System.Drawing.Size(81, 13);
			this.linkLabelClearMessages.TabIndex = 17;
			this.linkLabelClearMessages.TabStop = true;
			this.linkLabelClearMessages.Text = "Clear messages";
			this.linkLabelClearMessages.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClearMessages_LinkClicked);
			// 
			// groupBoxAutoSyncing
			// 
			this.groupBoxAutoSyncing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxAutoSyncing.Controls.Add(this.numericUpDownFtpPort);
			this.groupBoxAutoSyncing.Controls.Add(this.label6);
			this.groupBoxAutoSyncing.Controls.Add(this.textBoxMessages);
			this.groupBoxAutoSyncing.Controls.Add(this.linkLabelClearMessages);
			this.groupBoxAutoSyncing.Controls.Add(this.buttonAddLinkedFolders);
			this.groupBoxAutoSyncing.Controls.Add(this.buttonManuallySyncWithServer);
			this.groupBoxAutoSyncing.Controls.Add(this.progressBarSyncingProgress);
			this.groupBoxAutoSyncing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxAutoSyncing.ForeColor = System.Drawing.Color.Gray;
			this.groupBoxAutoSyncing.Location = new System.Drawing.Point(14, 156);
			this.groupBoxAutoSyncing.Name = "groupBoxAutoSyncing";
			this.groupBoxAutoSyncing.Size = new System.Drawing.Size(540, 309);
			this.groupBoxAutoSyncing.TabIndex = 18;
			this.groupBoxAutoSyncing.TabStop = false;
			this.groupBoxAutoSyncing.Text = "Auto syncing";
			// 
			// numericUpDownFtpPort
			// 
			this.numericUpDownFtpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numericUpDownFtpPort.Location = new System.Drawing.Point(154, 40);
			this.numericUpDownFtpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numericUpDownFtpPort.Name = "numericUpDownFtpPort";
			this.numericUpDownFtpPort.Size = new System.Drawing.Size(43, 20);
			this.numericUpDownFtpPort.TabIndex = 18;
			this.numericUpDownFtpPort.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(128, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 13);
			this.label6.TabIndex = 19;
			this.label6.Text = "Port:";
			// 
			// groupBoxManualPatching
			// 
			this.groupBoxManualPatching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxManualPatching.Controls.Add(this.linkLabelHideManualPatchingControls);
			this.groupBoxManualPatching.Controls.Add(this.label4);
			this.groupBoxManualPatching.Controls.Add(this.label5);
			this.groupBoxManualPatching.Controls.Add(this.textBoxFile1);
			this.groupBoxManualPatching.Controls.Add(this.textBoxFile2);
			this.groupBoxManualPatching.Controls.Add(this.label3);
			this.groupBoxManualPatching.Controls.Add(this.label2);
			this.groupBoxManualPatching.Controls.Add(this.button2);
			this.groupBoxManualPatching.Controls.Add(this.button3);
			this.groupBoxManualPatching.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBoxManualPatching.ForeColor = System.Drawing.Color.Gray;
			this.groupBoxManualPatching.Location = new System.Drawing.Point(14, 12);
			this.groupBoxManualPatching.MinimumSize = new System.Drawing.Size(453, 122);
			this.groupBoxManualPatching.Name = "groupBoxManualPatching";
			this.groupBoxManualPatching.Size = new System.Drawing.Size(453, 122);
			this.groupBoxManualPatching.TabIndex = 19;
			this.groupBoxManualPatching.TabStop = false;
			this.groupBoxManualPatching.Text = "xDelta3 manual patching (drag-drop supported)";
			// 
			// linkLabelHideManualPatchingControls
			// 
			this.linkLabelHideManualPatchingControls.AutoSize = true;
			this.linkLabelHideManualPatchingControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabelHideManualPatchingControls.ForeColor = System.Drawing.Color.DarkGray;
			this.linkLabelHideManualPatchingControls.LinkColor = System.Drawing.Color.DarkGray;
			this.linkLabelHideManualPatchingControls.Location = new System.Drawing.Point(161, 90);
			this.linkLabelHideManualPatchingControls.Name = "linkLabelHideManualPatchingControls";
			this.linkLabelHideManualPatchingControls.Size = new System.Drawing.Size(150, 13);
			this.linkLabelHideManualPatchingControls.TabIndex = 10;
			this.linkLabelHideManualPatchingControls.TabStop = true;
			this.linkLabelHideManualPatchingControls.Text = "&Hide manual patching controls";
			this.linkLabelHideManualPatchingControls.Visible = false;
			this.linkLabelHideManualPatchingControls.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHideManualPatchingControls_LinkClicked);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(386, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = ":Patch file";
			this.label4.Visible = false;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(386, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(61, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = ":Original file";
			this.label5.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(19, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "New file:";
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(6, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Original file:";
			this.label2.Visible = false;
			// 
			// buttonShowManualPatchingControls
			// 
			this.buttonShowManualPatchingControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonShowManualPatchingControls.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonShowManualPatchingControls.Location = new System.Drawing.Point(142, 70);
			this.buttonShowManualPatchingControls.Name = "buttonShowManualPatchingControls";
			this.buttonShowManualPatchingControls.Size = new System.Drawing.Size(177, 23);
			this.buttonShowManualPatchingControls.TabIndex = 10;
			this.buttonShowManualPatchingControls.Text = "Show &manual patching controls";
			this.buttonShowManualPatchingControls.UseVisualStyleBackColor = true;
			this.buttonShowManualPatchingControls.Click += new System.EventHandler(this.buttonShowManualPatchingControls_Click);
			// 
			// notifyIconTrayIcon
			// 
			this.notifyIconTrayIcon.Text = "Auto Sync";
			this.notifyIconTrayIcon.Visible = true;
			this.notifyIconTrayIcon.BalloonTipClicked += new System.EventHandler(this.notifyIconTrayIcon_BalloonTipClicked);
			this.notifyIconTrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconTrayIcon_MouseClick);
			// 
			// checkBoxTopmost
			// 
			this.checkBoxTopmost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxTopmost.AutoSize = true;
			this.checkBoxTopmost.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBoxTopmost.Location = new System.Drawing.Point(494, 12);
			this.checkBoxTopmost.Name = "checkBoxTopmost";
			this.checkBoxTopmost.Size = new System.Drawing.Size(60, 16);
			this.checkBoxTopmost.TabIndex = 20;
			this.checkBoxTopmost.Text = "Topmost";
			this.checkBoxTopmost.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 477);
			this.Controls.Add(this.checkBoxTopmost);
			this.Controls.Add(this.buttonShowManualPatchingControls);
			this.Controls.Add(this.groupBoxManualPatching);
			this.Controls.Add(this.groupBoxAutoSyncing);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.buttonCancelUpload);
			this.Controls.Add(this.buttonUploadHttp);
			this.Controls.Add(this.textBoxLocalDir);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonChooseLocalFolder);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto syncing";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.groupBoxAutoSyncing.ResumeLayout(false);
			this.groupBoxAutoSyncing.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFtpPort)).EndInit();
			this.groupBoxManualPatching.ResumeLayout(false);
			this.groupBoxManualPatching.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBoxFile1;
		private System.Windows.Forms.TextBox textBoxFile2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button buttonManuallySyncWithServer;
		private System.Windows.Forms.Button buttonUploadHttp;
		private System.Windows.Forms.Button buttonCancelUpload;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox textBoxMessages;
		private System.Windows.Forms.TextBox textBoxLocalDir;
		private System.Windows.Forms.Button buttonChooseLocalFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBarSyncingProgress;
		private System.Windows.Forms.Button buttonAddLinkedFolders;
		private System.Windows.Forms.LinkLabel linkLabelClearMessages;
		private System.Windows.Forms.GroupBox groupBoxAutoSyncing;
		private System.Windows.Forms.GroupBox groupBoxManualPatching;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonShowManualPatchingControls;
		private System.Windows.Forms.LinkLabel linkLabelHideManualPatchingControls;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDownFtpPort;
		private System.Windows.Forms.NotifyIcon notifyIconTrayIcon;
		private System.Windows.Forms.CheckBox checkBoxTopmost;

	}
}

