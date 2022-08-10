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

namespace DevkitLibrary
{
   using System.Threading.Tasks;
   using DevkitLibrary.Devkits;
   using DevkitLibrary.Enums;

   public class DevKitClient
   {
      public int TargetIndex;
      public IDevkit Devkit;
      public DevkitType Type;

      public PS3 PS3
      {
         get {
            return (PS3)this.Devkit;
         }
      }

      public Xbox360 Xbox360
      {
         get {
            return (Xbox360)this.Devkit;
         }
      }

      public Extensions Extensions
      {
         get {
            return new Extensions(this.Devkit);
         }
      }

      public void SetTarget(DevkitType type, int targetIndex)
      {
         this.Type = type;
         this.TargetIndex = targetIndex;
         this.Devkit = CreateDevkit(type, targetIndex);
      }

      internal static IDevkit CreateDevkit(DevkitType type, int targetIndex)
      {
         switch (type) {
            case DevkitType.PS3:
               return new PS3(targetIndex);

            case DevkitType.Xbox360:
               return new Xbox360();
         }

         return null;
      }

      public ConnectionStatus ConnectTarget()
      {
         return this.Devkit.Connect();
      }

      public async Task<ConnectionStatus> ConnectTargetAsync()
      {
         return await this.Devkit.ConnectAsync();
      }

      public bool DisconnectTarget()
      {
         return this.Devkit.Disconnect();
      }

      public async Task<bool> DisconnectTargetAsync()
      {
         return await this.Devkit.DisconnectAsync();
      }

      public ConnectionStatus GetConnectionStatus()
      {
         return this.Devkit.GetConnectionStatus();
      }

      public async Task<ConnectionStatus> GetConnectionStatusAsync()
      {
         return await this.Devkit.GetConnectionStatusAsync().ConfigureAwait(false);
      }

      public byte[] GetMemory(uint address, uint length)
      {
         return this.Devkit.GetMemory(address, length);
      }

      public async Task<byte[]> GetMemoryAsync(uint address, uint length)
      {
         return await this.Devkit.GetMemoryAsync(address, length);
      }

      public PowerState GetPowerState()
      {
         return this.Devkit.GetPowerState();
      }

      public async Task<PowerState> GetPowerStateAsync()
      {
         return await this.Devkit.GetPowerStateAsync();
      }

      public bool ProcessAttach()
      {
         return this.Devkit.AttachProcess();
      }

      public async Task<bool> AttachProcessAsync()
      {
         return await this.Devkit.AttachProcessAsync();
      }

      public bool SetMemory(uint address, byte[] bytes)
      {
         return this.Devkit.SetMemory(address, bytes);
      }

      public async Task<bool> SetMemoryAsync(uint address, byte[] bytes)
      {
         return await this.Devkit.SetMemoryAsync(address, bytes);
      }

      public bool SetPowerState(PowerState state, bool isForce = false)
      {
         return this.Devkit.SetPowerState(state, isForce);
      }

      public async Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false)
      {
         return await this.Devkit.SetPowerStateAsync(state, isForce);
      }
   }
}
