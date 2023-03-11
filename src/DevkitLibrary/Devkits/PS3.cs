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

namespace DevkitLibrary.Devkits
{
   using System;
   using System.Threading.Tasks;
   using DevkitLibrary.Enums;

   public class PS3 : IDevkit
   {
      internal class Params
      {
         public static string usage = "";
         public static uint processID = 0;
         public static uint[] processIDs = new uint[0];
      }

      private const PS3TMAPI.UnitType UNIT = PS3TMAPI.UnitType.PPU;

      public int TargetIndex { get; }

      public ConnectionStatus ConnectionStatus { get; set; }

      public PS3(int targetIndex)
      {
         this.TargetIndex = targetIndex;
         this.ConnectionStatus = ConnectionStatus.Disconnected;
      }

      public ConnectionStatus Connect()
      {
         var result = PS3TMAPI.InitTargetComms();
         if (PS3TMAPI.FAILED(result)) {
            throw new DevkitNotFoundException();
         }

         result = PS3TMAPI.Connect(this.TargetIndex, null);
         if (PS3TMAPI.FAILED(result)) {
            throw new DevkitConnectFailedException();
         }

         return this.ConnectionStatus = ConnectionStatus.Connected;
      }

      public async Task<ConnectionStatus> ConnectAsync()
      {
         var result = PS3TMAPI.InitTargetComms();
         if (PS3TMAPI.SUCCEEDED(result)) {
            return await Task.Run(() => this.Connect()).ConfigureAwait(false);
         }

         return this.ConnectionStatus = ConnectionStatus.Unavailable;
      }

      public bool Disconnect()
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return false;
         }

         return PS3TMAPI.SUCCEEDED(PS3TMAPI.Disconnect(this.TargetIndex));
      }

      public async Task<bool> DisconnectAsync()
      {
         return await Task.Run(() => this.Disconnect()).ConfigureAwait(false);
      }

      public ConnectionStatus GetConnectionStatus()
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return ConnectionStatus.Unavailable;
         }

         var status = PS3TMAPI.ConnectStatus.Unavailable;
         var result = PS3TMAPI.GetConnectStatus(this.TargetIndex, out status, out Params.usage);

         return (PS3TMAPI.SUCCEEDED(result)) ? (ConnectionStatus)status : ConnectionStatus.Unavailable;
      }

      public async Task<ConnectionStatus> GetConnectionStatusAsync()
      {
         return await Task.Run(() => this.GetConnectionStatus()).ConfigureAwait(false);
      }

      public byte[] GetMemory(uint address, uint length)
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return new byte[1] { 0x00 };
         }

         var bytes = new byte[length];
         var result = PS3TMAPI.ProcessGetMemory(this.TargetIndex, UNIT, Params.processID, 0, address, ref bytes);
         return (PS3TMAPI.SUCCEEDED(result)) ? bytes : new byte[1] { 0x00 };
      }

      public async Task<byte[]> GetMemoryAsync(uint address, uint length)
      {
         return await Task.Run(() => this.GetMemory(address, length)).ConfigureAwait(false);
      }

      public PowerState GetPowerState()
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return PowerState.Unknown;
         }

         var status = PS3TMAPI.PowerStatus.Unknown;
         var result = PS3TMAPI.GetPowerStatus(this.TargetIndex, out status);
         return (PS3TMAPI.SUCCEEDED(result)) ?
            (PowerState)status :
            PowerState.Unknown;
      }

      public async Task<PowerState> GetPowerStateAsync()
      {
         return await Task.Run(() => this.GetPowerState()).ConfigureAwait(false);
      }

      public bool AttachProcess()
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return false;
         }

         var result = PS3TMAPI.GetProcessList(this.TargetIndex, out Params.processIDs);
         if (PS3TMAPI.FAILED(result)) {
            throw new DevKitAttachProcessFailedException($"{Enum.GetName(typeof(PS3TMAPI.SNRESULT), result)}");
         }

         if (Params.processIDs.Length > 0) {
            Params.processID = Convert.ToUInt32(Params.processIDs[0]);
            PS3TMAPI.ProcessAttach(this.TargetIndex, UNIT, Params.processID);
            Console.WriteLine(Params.processID);

            return PS3TMAPI.SUCCEEDED(PS3TMAPI.ProcessContinue(this.TargetIndex, Params.processID));
         }

         return false;
      }

      public async Task<bool> AttachProcessAsync()
      {
         return await Task.Run(() => {
            PS3TMAPI.InitTargetComms();
            return this.AttachProcess();
         }).ConfigureAwait(false); ;
      }

      public bool SetMemory(uint address, byte[] bytes)
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return false;
         }

         return PS3TMAPI.SUCCEEDED(PS3TMAPI.ProcessSetMemory(this.TargetIndex, UNIT, Params.processID, 0, address, bytes));
      }

      public async Task<bool> SetMemoryAsync(uint address, byte[] bytes)
      {
         return await Task.Run(() => this.SetMemory(address, bytes)).ConfigureAwait(false);
      }

      public bool SetPowerState(PowerState state, bool isForce = false)
      {
         if (this.ConnectionStatus != ConnectionStatus.Connected) {
            return false;
         }

         var result = PS3TMAPI.SNRESULT.SN_E_COMMS_ERR;
         switch (state) {
            case PowerState.Off:
               result = PS3TMAPI.PowerOff(this.TargetIndex, isForce);
               break;

            case PowerState.On:
               result = PS3TMAPI.PowerOn(this.TargetIndex);
               break;
         }

         return PS3TMAPI.SUCCEEDED(result);
      }

      public async Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false)
      {
         return await Task.Run(() => this.SetPowerState(state, isForce)).ConfigureAwait(false);
      }

      public string ProcessInfo()
      {
         return (this.ConnectionStatus != ConnectionStatus.Connected) ?
            string.Empty
            : Params.processID.ToString("X8");
      }

      public PS3TMAPI.SNRESULT InitTargetComms()
      {
         return PS3TMAPI.InitTargetComms();
      }
   }
}
