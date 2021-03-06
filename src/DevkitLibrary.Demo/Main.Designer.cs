﻿namespace DevkitLibrary.Demo
{
	partial class Main
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
      this.darkButtonConnect = new DarkUI.Controls.DarkButton();
      this.darkComboBoxDevkit = new DarkUI.Controls.DarkComboBox();
      this.darkButtonAttach = new DarkUI.Controls.DarkButton();
      this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
      this.darkGroupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // darkButtonConnect
      // 
      this.darkButtonConnect.Enabled = false;
      this.darkButtonConnect.Location = new System.Drawing.Point(61, 113);
      this.darkButtonConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.darkButtonConnect.Name = "darkButtonConnect";
      this.darkButtonConnect.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.darkButtonConnect.Size = new System.Drawing.Size(134, 30);
      this.darkButtonConnect.TabIndex = 4;
      this.darkButtonConnect.Text = "Connect";
      this.darkButtonConnect.Click += new System.EventHandler(this.darkButtonConnect_Click);
      // 
      // darkComboBoxDevkit
      // 
      this.darkComboBoxDevkit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
      this.darkComboBoxDevkit.FormattingEnabled = true;
      this.darkComboBoxDevkit.Items.AddRange(new object[] {
            "PS3",
            "Xbox360"});
      this.darkComboBoxDevkit.Location = new System.Drawing.Point(61, 63);
      this.darkComboBoxDevkit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.darkComboBoxDevkit.Name = "darkComboBoxDevkit";
      this.darkComboBoxDevkit.Size = new System.Drawing.Size(273, 20);
      this.darkComboBoxDevkit.TabIndex = 6;
      this.darkComboBoxDevkit.SelectedIndexChanged += new System.EventHandler(this.darkComboBoxDevkit_SelectedIndexChanged);
      // 
      // darkButtonAttach
      // 
      this.darkButtonAttach.Enabled = false;
      this.darkButtonAttach.Location = new System.Drawing.Point(198, 113);
      this.darkButtonAttach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.darkButtonAttach.Name = "darkButtonAttach";
      this.darkButtonAttach.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.darkButtonAttach.Size = new System.Drawing.Size(134, 30);
      this.darkButtonAttach.TabIndex = 7;
      this.darkButtonAttach.Text = "Process Attach";
      this.darkButtonAttach.Click += new System.EventHandler(this.darkButtonAttach_Click);
      // 
      // darkGroupBox1
      // 
      this.darkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
      this.darkGroupBox1.Controls.Add(this.darkComboBoxDevkit);
      this.darkGroupBox1.Controls.Add(this.darkButtonAttach);
      this.darkGroupBox1.Controls.Add(this.darkButtonConnect);
      this.darkGroupBox1.Location = new System.Drawing.Point(7, 8);
      this.darkGroupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.darkGroupBox1.Name = "darkGroupBox1";
      this.darkGroupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.darkGroupBox1.Size = new System.Drawing.Size(392, 197);
      this.darkGroupBox1.TabIndex = 8;
      this.darkGroupBox1.TabStop = false;
      this.darkGroupBox1.Text = "Devkit :";
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(407, 213);
      this.Controls.Add(this.darkGroupBox1);
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.MaximizeBox = false;
      this.Name = "Main";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DevkitLib by coreizer v0.2.5";
      this.darkGroupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion
    private DarkUI.Controls.DarkButton darkButtonConnect;
    private DarkUI.Controls.DarkComboBox darkComboBoxDevkit;
    private DarkUI.Controls.DarkButton darkButtonAttach;
    private DarkUI.Controls.DarkGroupBox darkGroupBox1;
  }
}

