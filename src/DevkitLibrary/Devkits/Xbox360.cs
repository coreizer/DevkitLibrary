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

using System;
using System.Threading.Tasks;
using DevkitLibrary.Enums;
using XDevkit;

namespace DevkitLibrary.Devkits
{
   public class Xbox360 : IDevkit
   {
      internal class Params
      {
         public static string UserName;
         public static IXboxManager XboxManager;
         public static IXboxConsole XboxConsole;
         public static string DebuggerName;
      }

      private const string NAME = "coreizer_xdevkit";
      private const string GUID = "A5EB45D8-F3B6-49B9-984A-0D313AB60342";

      public Endianness Endian { get; set; } = Endianness.Little;

      public int TargetIndex => throw new NotImplementedException();

      public ConnectionStatus ConnectionStatus { get; private set; }

      public Xbox360(Endianness endian = Endianness.Little) {
         this.Endian = endian;
         this.ConnectionStatus = ConnectionStatus.Disconnected;
      }

      public ConnectionStatus Connect() {
         this.ConnectionStatus = ConnectionStatus.Unavailable;

         if (Params.XboxManager != null) {
            this.ConnectionStatus = Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Params.DebuggerName, out Params.UserName) ?
              ConnectionStatus.Connected : ConnectionStatus.Unavailable;
         }
         else {
            var clsid = new Guid(GUID);
            Params.XboxManager = (XboxManager)Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
            Params.XboxConsole = Params.XboxManager.OpenConsole(Params.XboxManager.DefaultConsole);

            if (Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Params.DebuggerName, out Params.UserName)) {
               Params.XboxConsole.DebugTarget.ConnectAsDebugger(NAME, XboxDebugConnectFlags.Force);
               this.ConnectionStatus = Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Params.DebuggerName, out Params.UserName) ?
                 ConnectionStatus.Connected : ConnectionStatus.Unavailable;
            }
         }

         return this.ConnectionStatus;
      }

      public async Task<ConnectionStatus> ConnectAsync() => await Task.Run(() => this.Connect()).ConfigureAwait(true);

      public bool Disconnect() {
         if (this.ConnectionStatus != ConnectionStatus.Connected) return false;

         try {
            Params.XboxConsole.DebugTarget.DisconnectAsDebugger();
         }
         catch {
            return false;
         }

         return true;
      }

      public async Task<bool> DisconnectAsync() => await Task.Run(() => this.Disconnect()).ConfigureAwait(true);

      public ConnectionStatus GetConnectionStatus() {
         if (this.ConnectionStatus != ConnectionStatus.Connected) return ConnectionStatus.Unavailable;

         return Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Params.DebuggerName, out Params.UserName) ?
             ConnectionStatus.Connected : ConnectionStatus.Disconnected;
      }

      public async Task<ConnectionStatus> GetConnectionStatusAsync() => await Task.Run(() => this.GetConnectionStatus());

      public byte[] GetMemory(uint address, uint length) {
         if (this.ConnectionStatus != ConnectionStatus.Connected) return new byte[length];

         var buffer = new byte[length];
         Params.XboxConsole.DebugTarget.GetMemory(address, length, buffer, out _);
         return buffer;
      }

      public async Task<byte[]> GetMemoryAsync(uint address, uint length) => await Task.Run(() => this.GetMemory(address, length));

      public bool SetMemory(uint address, byte[] bytes) {
         if (this.ConnectionStatus != ConnectionStatus.Connected) return false;
         Params.XboxConsole.DebugTarget.SetMemory(address, (uint)bytes.Length, bytes, out _);
         return true;
      }

      public async Task<bool> SetMemoryAsync(uint address, byte[] bytes) => await Task.Run(() => this.SetMemory(address, bytes));

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public PowerState GetPowerState() => throw new NotImplementedException();

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public Task<PowerState> GetPowerStateAsync() => throw new NotImplementedException();

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public bool AttachProcess() => throw new NotImplementedException();

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public Task<bool> AttachProcessAsync() => throw new NotImplementedException();

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public bool SetPowerState(PowerState state, bool isForce = false) => throw new NotImplementedException();

      /// <summary>
      /// Not Supported.
      /// </summary>
      /// <returns>NotImplementedException</returns>
      [NotSupported]
      public Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false) => throw new NotImplementedException();
   }
}
