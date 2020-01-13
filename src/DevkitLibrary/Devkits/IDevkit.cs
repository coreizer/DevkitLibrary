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

using System.Threading.Tasks;

using DevkitLibrary.Enums;

namespace DevkitLibrary.Devkits
{
  public interface IDevkit
  {
    int TargetIndex { get; }

    ConnectState ConnectState { get; }

    ConnectState Connect();

    Task<ConnectState> ConnectAsync();

    bool Disconnect();

    Task<bool> DisconnectAsync();

    bool ProcessAttach();

    Task<bool> ProcessAttachAsync();

    bool SetMemory(uint address, byte[] bytes);

    Task<bool> SetMemoryAsync(uint address, byte[] bytes);

    byte[] GetMemory(uint address, uint length);

    Task<byte[]> GetMemoryAsync(uint address, uint length);

    bool SetPowerState(PowerState state, bool isForce = false);

    Task<bool> SetPowerStateAsync(PowerState state, bool isForce = false);

    PowerState GetPowerState();

    Task<PowerState> GetPowerStateAsync();

    ConnectState GetConnectState();

    Task<ConnectState> GetConnectStateAsync();
  }
}
