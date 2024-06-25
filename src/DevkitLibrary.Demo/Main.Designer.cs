#region License Information (GPL v3)

/**
 * Copyright (C) 2017-2022 coreizer
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

#endregion

namespace DevkitLibrary.Demo
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
         this.darkLabel1 = new DarkUI.Controls.DarkLabel();
         this.SuspendLayout();
         // 
         // darkButtonConnect
         // 
         this.darkButtonConnect.Enabled = false;
         this.darkButtonConnect.Location = new System.Drawing.Point(14, 94);
         this.darkButtonConnect.Margin = new System.Windows.Forms.Padding(2);
         this.darkButtonConnect.Name = "darkButtonConnect";
         this.darkButtonConnect.Padding = new System.Windows.Forms.Padding(3);
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
         this.darkComboBoxDevkit.Location = new System.Drawing.Point(14, 43);
         this.darkComboBoxDevkit.Margin = new System.Windows.Forms.Padding(2);
         this.darkComboBoxDevkit.Name = "darkComboBoxDevkit";
         this.darkComboBoxDevkit.Size = new System.Drawing.Size(273, 20);
         this.darkComboBoxDevkit.TabIndex = 6;
         this.darkComboBoxDevkit.SelectedIndexChanged += new System.EventHandler(this.darkComboBoxDevkit_SelectedIndexChanged);
         // 
         // darkButtonAttach
         // 
         this.darkButtonAttach.Enabled = false;
         this.darkButtonAttach.Location = new System.Drawing.Point(153, 94);
         this.darkButtonAttach.Margin = new System.Windows.Forms.Padding(2);
         this.darkButtonAttach.Name = "darkButtonAttach";
         this.darkButtonAttach.Padding = new System.Windows.Forms.Padding(3);
         this.darkButtonAttach.Size = new System.Drawing.Size(134, 30);
         this.darkButtonAttach.TabIndex = 7;
         this.darkButtonAttach.Text = "Process Attach";
         this.darkButtonAttach.Click += new System.EventHandler(this.darkButtonAttach_Click);
         // 
         // darkLabel1
         // 
         this.darkLabel1.AutoSize = true;
         this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
         this.darkLabel1.Location = new System.Drawing.Point(12, 20);
         this.darkLabel1.Name = "darkLabel1";
         this.darkLabel1.Size = new System.Drawing.Size(61, 12);
         this.darkLabel1.TabIndex = 10;
         this.darkLabel1.Text = "Select to ...";
         // 
         // Main
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
         this.ClientSize = new System.Drawing.Size(303, 148);
         this.Controls.Add(this.darkLabel1);
         this.Controls.Add(this.darkComboBoxDevkit);
         this.Controls.Add(this.darkButtonAttach);
         this.Controls.Add(this.darkButtonConnect);
         this.Margin = new System.Windows.Forms.Padding(2);
         this.MaximizeBox = false;
         this.Name = "Main";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "DevkitLib by coreizer v0.2.8";
         this.ResumeLayout(false);
         this.PerformLayout();

		}

		#endregion
    private DarkUI.Controls.DarkButton darkButtonConnect;
    private DarkUI.Controls.DarkComboBox darkComboBoxDevkit;
    private DarkUI.Controls.DarkButton darkButtonAttach;
      private DarkUI.Controls.DarkLabel darkLabel1;
   }
}

