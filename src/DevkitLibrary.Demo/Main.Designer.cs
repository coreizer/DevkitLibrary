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
			this.comboBoxDevkit = new System.Windows.Forms.ComboBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonProcessAttach = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxDevkit
			// 
			this.comboBoxDevkit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDevkit.FormattingEnabled = true;
			this.comboBoxDevkit.Items.AddRange(new object[] {
            "PS3",
            "Xbox360"});
			this.comboBoxDevkit.Location = new System.Drawing.Point(28, 84);
			this.comboBoxDevkit.Name = "comboBoxDevkit";
			this.comboBoxDevkit.Size = new System.Drawing.Size(392, 26);
			this.comboBoxDevkit.TabIndex = 0;
			this.comboBoxDevkit.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevkit_SelectedIndexChanged);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(28, 148);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(193, 40);
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Devkit";
			// 
			// buttonProcessAttach
			// 
			this.buttonProcessAttach.Enabled = false;
			this.buttonProcessAttach.Location = new System.Drawing.Point(227, 148);
			this.buttonProcessAttach.Name = "buttonProcessAttach";
			this.buttonProcessAttach.Size = new System.Drawing.Size(193, 40);
			this.buttonProcessAttach.TabIndex = 3;
			this.buttonProcessAttach.Text = "Process Attach";
			this.buttonProcessAttach.UseVisualStyleBackColor = true;
			this.buttonProcessAttach.Click += new System.EventHandler(this.buttonProcessAttach_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 251);
			this.Controls.Add(this.buttonProcessAttach);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.comboBoxDevkit);
			this.MaximizeBox = false;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Devkit Lib by coreizer v0.1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxDevkit;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonProcessAttach;
	}
}

