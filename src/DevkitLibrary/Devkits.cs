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
				return new Extension(this.DevkitTarget, this.TargetIndex);
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

		public async Task<ConnectState> ConnectTarget()
		{
			return await this.Devkit.ConnectTarget();
		}

		public async Task<bool> DisconnectTarget()
		{
			return await this.Devkit.DisconnectTarget();
		}

		public async Task<ConnectState> GetConnectState()
		{
			return await this.Devkit.GetConnectState();
		}

		public async Task<byte[]> GetMemory(uint address, uint length)
		{
			return await this.Devkit.GetMemory(address, length);
		}

		public async Task<PowerState> GetPowerState()
		{
			return await this.Devkit.GetPowerState();
		}

		public async Task<bool> ProcessAttach()
		{
			return await this.Devkit.ProcessAttach();
		}

		public async Task<bool> SetMemory(uint address, byte[] bytes)
		{
			return await this.Devkit.SetMemory(address, bytes);
		}

		public async Task<bool> SetPowerState(PowerState state, bool isForce = false)
		{
			return await this.Devkit.SetPowerState(state, isForce);
		}
	}
}
