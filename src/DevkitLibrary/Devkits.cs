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

using DevkitLibrary.Devkits;
using DevkitLibrary.Enums;

namespace DevkitLibrary
{
  public class DevKits
  {
    public int TargetIndex;
    public IDevkit Devkit;
    public DevkitTarget DevkitTarget;

    public PS3 PS3 {
      get {
        return (PS3)this.Devkit;
      }
    }

    public Xbox360 Xbox360 {
      get {
        return (Xbox360)this.Devkit;
      }
    }

    public Extension Extension {
      get {
        return new Extension(this.Devkit);
      }
    }

    public void SetTarget(DevkitTarget target, int targetIndex)
    {
      this.DevkitTarget = target;
      this.TargetIndex = targetIndex;
      this.Devkit = CreateDevkit(target, targetIndex);
    }

    internal static IDevkit CreateDevkit(DevkitTarget target, int targetIndex)
    {
      switch (target)
      {
        case DevkitTarget.PS3:
          return new PS3(targetIndex);

        case DevkitTarget.Xbox360:
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
      return await this.Devkit.GetConnectionStatusAsync();
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
      return this.Devkit.ProcessAttach();
    }

    public async Task<bool> ProcessAttachAsync()
    {
      return await this.Devkit.ProcessAttachAsync();
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
