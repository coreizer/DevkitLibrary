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
   using System;
   using System.Windows.Forms;
   using DarkUI.Forms;
   using DevkitLibrary.Enums;

   public partial class Main : DarkForm
   {
      private readonly DevKitClient client = new DevKitClient();

      public Main()
      {
         this.InitializeComponent();
      }

      private void darkComboBoxDevkit_SelectedIndexChanged(object sender, EventArgs e)
      {
         switch ((sender as ComboBox).Text.ToUpper()) {
            case "PS3":
               this.darkButtonAttach.Text = "Process Attach";
               this.client.SetTarget(DevkitType.PS3, 0);
               break;

            case "XBOX360":
               this.darkButtonAttach.Text = "Not Supported";
               this.client.SetTarget(DevkitType.Xbox360, 0);
               break;
         }

         this.darkButtonConnect.Enabled = true;
      }

      private async void darkButtonConnect_Click(object sender, EventArgs e)
      {
         this.darkButtonConnect.Enabled = false;
         this.darkComboBoxDevkit.Enabled = false;

         try {
            var status = await this.client.ConnectTargetAsync();

            switch (status) {
               case ConnectionStatus.Connected:
                  if (this.client.Type == DevkitType.PS3) {
                     this.darkButtonAttach.Enabled = true;
                  }

                  this.darkButtonConnect.Enabled = false;
                  this.darkButtonConnect.Text = "Connected";
                  DarkMessageBox.ShowInformation($"Connected to Target", "Devkit", DarkDialogButton.Ok);
                  break;

               default:
                  this.darkButtonConnect.Enabled = true;
                  DarkMessageBox.ShowWarning("Connection error", "Devkit", DarkDialogButton.Ok);
                  break;
            }
         }
         catch (Exception ex) {
            DarkMessageBox.ShowError(ex.Message, $"Error - {Application.ProductName}", DarkDialogButton.Ok);
            this.darkButtonConnect.Enabled = true;
         }
         finally {
            this.darkComboBoxDevkit.Enabled = true;
         }
      }

      private async void darkButtonAttach_Click(object sender, EventArgs e)
      {
         try {
            this.darkButtonAttach.Enabled = false;
            var successfully = await this.client.AttachProcessAsync();
            if (successfully) {
               DarkMessageBox.ShowInformation("Current game process is attached successfully !", Application.ProductName, DarkDialogButton.Ok);
            }
            else {
               DarkMessageBox.ShowWarning($"No game process found", Application.ProductName, DarkDialogButton.Ok);
            }
         }
         catch (DevKitAttachProcessFailedException ex) {
            DarkMessageBox.ShowWarning($"No game process found\r\n{ex.Message}", Application.ProductName, DarkDialogButton.Ok);
         }
         catch (Exception ex) {
            DarkMessageBox.ShowError(ex.Message, $"Error - {Application.ProductName}", DarkDialogButton.Ok);
         }

         finally {
            this.darkButtonAttach.Enabled = true;
         }
      }
   }
}
