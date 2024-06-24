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

using System.Threading.Tasks;
using DevkitLibrary.Devkits;
using DevkitLibrary.Enums;

namespace DevkitLibrary
{
   public class DevKitClient
   {
      public int TargetIndex { get; set; }

      public IDevkit Devkit { get; set; }

      public DevkitType Type { get; set; }

      public Endianness Endian { get => this.Devkit.Endian; set => this.Devkit.Endian = value; }

      public PS3 PS3 => (PS3)this.Devkit;

      public Xbox360 Xbox360 => (Xbox360)this.Devkit;

      public Extensions Extensions => new Extensions(this.Devkit);

      public void SetTarget(DevkitType type, int targetIndex) {
         this.Type = type;
         this.TargetIndex = targetIndex;
         this.Devkit = CreateDevkit(type, targetIndex);
      }

      internal static IDevkit CreateDevkit(DevkitType type, int targetIndex) {
         switch (type) {
            case DevkitType.PS3:
               return new PS3(targetIndex, Endianness.Little);

            case DevkitType.Xbox360:
               return new Xbox360(Endianness.Little);
         }

         return null;
      }

      public ConnectionStatus ConnectTarget() => this.Devkit.Connect();

      public async Task<ConnectionStatus> ConnectTargetAsync() => await this.Devkit.ConnectAsync();

      public bool DisconnectTarget() => this.Devkit.Disconnect();

      public async Task<bool> DisconnectTargetAsync() => await this.Devkit.DisconnectAsync();

      public ConnectionStatus GetConnectionStatus() => this.Devkit.GetConnectionStatus();

      public async Task<ConnectionStatus> GetConnectionStatusAsync() => await this.Devkit.GetConnectionStatusAsync().ConfigureAwait(true);

      public byte[] GetMemory(uint address, uint length) => this.Devkit.GetMemory(address, length);

      public async Task<byte[]> GetMemoryAsync(uint address, uint length) => await this.Devkit.GetMemoryAsync(address, length);

      public PowerState GetPowerState() => this.Devkit.GetPowerState();

      public async Task<PowerState> GetPowerStateAsync() => await this.Devkit.GetPowerStateAsync();

      public bool ProcessAttach() => this.Devkit.AttachProcess();

      public async Task<bool> AttachProcessAsync() => await this.Devkit.AttachProcessAsync();

      public bool SetMemory(uint address, byte[] bytes) => this.Devkit.SetMemory(address, bytes);

      public async Task<bool> SetMemoryAsync(uint address, byte[] bytes) => await this.Devkit.SetMemoryAsync(address, bytes);

      public bool SetPowerState(PowerState state, bool isForce = false) => this.Devkit.SetPowerState(state, isForce);

      public async Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false) => await this.Devkit.SetPowerStateAsync(state, isForce);
   }
}
