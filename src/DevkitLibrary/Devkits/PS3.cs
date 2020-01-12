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
using System.Threading.Tasks;

using DevkitLibrary.Enums;

namespace DevkitLibrary.Devkits
{

	public class PS3 : IDevkit
	{
		private const PS3TMAPI.UnitType UNIT = PS3TMAPI.UnitType.PPU;

		internal class Params
		{
			public static string usage;
			public static uint processID;
			public static uint[] processIDs;

			public static uint GetProcessID()
			{
				return (processIDs != null && processIDs.Length > 0) ?
					Convert.ToUInt32(processIDs[0]) : 0;
			}
		}

		public int TargetIndex {
			get;
		}

		public ConnectState ConnectState {
			get;
			set;
		}

		public PS3(int targetIndex)
		{
			this.TargetIndex = targetIndex;
			this.ConnectState = ConnectState.Disconnected;
		}

		public async Task<ConnectState> ConnectTarget()
		{
			return await Task.Run(() =>
			{
				PS3TMAPI.SNRESULT result = PS3TMAPI.InitTargetComms();
				if (PS3TMAPI.FAILED(result))
					return this.ConnectState = ConnectState.Unavailable;

				result = PS3TMAPI.Connect(this.TargetIndex, null);
				if (PS3TMAPI.SUCCEEDED(result))
					return this.ConnectState = ConnectState.Connected;

				return this.ConnectState = ConnectState.Unavailable;
			});
		}

		public async Task<bool> ProcessAttach()
		{
			if (this.ConnectState != ConnectState.Connected) return false;

			return await Task.Run(() =>
			{
				PS3TMAPI.SNRESULT result = PS3TMAPI.GetProcessList(this.TargetIndex, out Params.processIDs);
				if (PS3TMAPI.SUCCEEDED(result) && Params.processIDs.Length > 0)
				{
					Params.processID = Params.GetProcessID();
					PS3TMAPI.ProcessAttach(this.TargetIndex, UNIT, Params.processID);
					result = PS3TMAPI.ProcessContinue(this.TargetIndex, Params.processID);
				}

				return PS3TMAPI.SUCCEEDED(result);
			});
		}

		public async Task<string> ProcessInfo()
		{
			if (this.ConnectState != ConnectState.Connected) return "";

			return Params.processID.ToString("X8");
		}

		public async Task<bool> DisconnectTarget()
		{
			if (this.ConnectState != ConnectState.Connected) return false;

			return await Task.Run(() =>
			{
				return PS3TMAPI.SUCCEEDED(PS3TMAPI.Disconnect(this.TargetIndex));
			});
		}

		public async Task<ConnectState> GetConnectState()
		{
			if (this.ConnectState != ConnectState.Connected) return ConnectState.Unavailable;

			return await Task.Run(() =>
			{
				PS3TMAPI.ConnectStatus status = PS3TMAPI.ConnectStatus.Unavailable;
				PS3TMAPI.SNRESULT result = PS3TMAPI.GetConnectStatus(this.TargetIndex, out status, out Params.usage);

				if (PS3TMAPI.SUCCEEDED(result))
				{
					return (ConnectState)status;
				}

				return ConnectState.Unavailable;
			});
		}

		public async Task<byte[]> GetMemory(uint address, uint length)
		{
			if (this.ConnectState != ConnectState.Connected) return new byte[0];

			return await Task.Run(() =>
			{
				byte[] bytes = new byte[length];
				PS3TMAPI.SNRESULT result = PS3TMAPI.ProcessGetMemory(this.TargetIndex, UNIT, Params.processID, 0, address, ref bytes);
				if (PS3TMAPI.SUCCEEDED(result))
				{
					return bytes;
				}

				return new byte[0];
			});
		}

		public async Task<PowerState> GetPowerState()
		{
			if (this.ConnectState != ConnectState.Connected) return PowerState.Unknown;

			return await Task.Run(() =>
			{
				PS3TMAPI.PowerStatus status = PS3TMAPI.PowerStatus.Unknown;
				PS3TMAPI.SNRESULT result = PS3TMAPI.GetPowerStatus(this.TargetIndex, out status);
				if (PS3TMAPI.SUCCEEDED(result))
				{
					return (PowerState)status;
				}

				return PowerState.Unknown;
			});
		}

		public async Task<bool> SetMemory(uint address, byte[] bytes)
		{
			if (this.ConnectState != ConnectState.Connected) return false;

			return await Task.Run(() =>
			{
				return PS3TMAPI.SUCCEEDED(PS3TMAPI.ProcessSetMemory(this.TargetIndex, UNIT, Params.processID, 0, address, bytes));
			});
		}

		public async Task<bool> SetPowerState(PowerState state, bool isForce = false)
		{
			if (this.ConnectState != ConnectState.Connected) return false;

			return await Task.Run(() =>
			{
				PS3TMAPI.SNRESULT result = PS3TMAPI.SNRESULT.SN_E_COMMS_ERR;

				switch (state)
				{
					case PowerState.Off:
						result = PS3TMAPI.PowerOff(this.TargetIndex, isForce);
						break;

					case PowerState.On:
						result = PS3TMAPI.PowerOn(this.TargetIndex);
						break;
				}

				return PS3TMAPI.SUCCEEDED(result);
			});
		}
	}
}
