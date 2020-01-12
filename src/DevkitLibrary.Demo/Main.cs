/*
 * Copyright (c) 2020 Coreizer
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
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Forms;

using DevkitLibrary.Enums;

namespace DevkitLibrary.Demo
{
	public partial class Main : Form
	{
		private DevKits devkit = new DevKits();

		public Main()
		{
			this.InitializeComponent();
			this.comboBoxDevkit.SelectedIndex = 0;
		}

		private async void buttonConnect_Click(object sender, EventArgs e)
		{
			this.buttonConnect.Enabled = false;
			this.comboBoxDevkit.Enabled = false;

			try
			{
				ConnectState state = await this.devkit.ConnectTarget();

				switch (state)
				{
					case ConnectState.Connected:
					{
						if (this.devkit.DevkitTarget == DevkitTarget.PS3) this.buttonProcessAttach.Enabled = true;
						MessageBox.Show("Connect to Target", "Devkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					}

					default:
					{
						MessageBox.Show("Connection error", "Devkit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.comboBoxDevkit.Enabled = true;
				this.buttonConnect.Enabled = true;
			}
		}

		private void comboBoxDevkit_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.comboBoxDevkit.Text.ToLower())
			{
				case "ps3":
				{
					this.buttonProcessAttach.Text = "Process Attach";
					this.devkit.SetTarget(DevkitTarget.PS3, 0);
					break;
				}

				case "xbox360":
				{
					this.buttonProcessAttach.Text = "Not Supported";
					this.devkit.SetTarget(DevkitTarget.Xbox360, 0);
					break;
				}
			}
		}

		private async void buttonProcessAttach_Click(object sender, EventArgs e)
		{
			try
			{
				this.buttonProcessAttach.Enabled = true;
				await this.devkit.ProcessAttach();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.buttonProcessAttach.Enabled = false;
			}
		}
	}
}
