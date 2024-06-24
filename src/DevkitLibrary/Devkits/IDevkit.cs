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
using DevkitLibrary.Enums;

namespace DevkitLibrary.Devkits
{
   public interface IDevkit
   {
      int TargetIndex { get; }

      Endianness Endian { get; set; }

      ConnectionStatus ConnectionStatus { get; }

      ConnectionStatus Connect();

      Task<ConnectionStatus> ConnectAsync();

      bool Disconnect();

      Task<bool> DisconnectAsync();

      bool AttachProcess();

      Task<bool> AttachProcessAsync();

      bool SetMemory(uint address, byte[] bytes);

      Task<bool> SetMemoryAsync(uint address, byte[] bytes);

      byte[] GetMemory(uint address, uint length);

      Task<byte[]> GetMemoryAsync(uint address, uint length);

      bool SetPowerState(PowerState state, bool isForce = false);

      Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false);

      PowerState GetPowerState();

      Task<PowerState> GetPowerStateAsync();

      ConnectionStatus GetConnectionStatus();

      Task<ConnectionStatus> GetConnectionStatusAsync();
   }
}
