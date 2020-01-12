﻿/*
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
using XDevkit;

namespace DevkitLibrary.Devkits
{
	public class Xbox360 : IDevkit
	{
		private const string NAME = "coreizer_xdevkit";
		private const string GUID = "A5EB45D8-F3B6-49B9-984A-0D313AB60342";

		internal class Params
		{
			public static string UserName;
			public static IXboxManager XboxManager;
			public static IXboxConsole XboxConsole;
			public static string DebuggerName;
		}

		[NotSupported]
		public int TargetIndex {
			get {
				return int.MinValue;
			}
		}

		public ConnectState ConnectState {
			get;
			private set;
		}

		public async Task<ConnectState> ConnectTarget()
		{
			return await Task.Run(() =>
			{
				this.ConnectState = ConnectState.Unavailable;

				if (Params.XboxManager != null)
				{
					this.ConnectState = Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Xbox360.Params.DebuggerName, out Xbox360.Params.UserName) ?
						ConnectState.Connected : ConnectState.Disconnected;
				}
				else
				{
					Guid clsid = new Guid(GUID);
					Params.XboxManager = (XboxManager)Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
					Params.XboxConsole = Params.XboxManager.OpenConsole(Params.XboxManager.DefaultConsole);

					if (Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Xbox360.Params.DebuggerName, out Xbox360.Params.UserName))
					{
						Params.XboxConsole.DebugTarget.ConnectAsDebugger(NAME, XboxDebugConnectFlags.Force);
						this.ConnectState = Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Xbox360.Params.DebuggerName, out Xbox360.Params.UserName) ?
							ConnectState.Connected : ConnectState.Disconnected;
					}
				}

				return this.ConnectState;
			});
		}

		public async Task<bool> DisconnectTarget()
		{
			return await Task.Run(() =>
			{
				if (this.ConnectState != ConnectState.Connected) return false;

				try
				{
					Params.XboxConsole.DebugTarget.DisconnectAsDebugger();
				}
				catch
				{
					return false;
				}

				return true;
			});
		}

		public async Task<ConnectState> GetConnectState()
		{
			return await Task.Run(() =>
			{
				if (this.ConnectState != ConnectState.Connected) return ConnectState.Unavailable;

				return Params.XboxConsole.DebugTarget.IsDebuggerConnected(out Xbox360.Params.DebuggerName, out Xbox360.Params.UserName) ?
						ConnectState.Connected : ConnectState.Disconnected;
			});
		}

		public async Task<byte[]> GetMemory(uint address, uint length)
		{
			return await Task.Run(() =>
			{
				if (this.ConnectState != ConnectState.Connected) return new byte[0];

				byte[] bytes = new byte[length];
				Params.XboxConsole.DebugTarget.GetMemory(address, length, bytes, out uint bytesRead);
				return bytes;
			});
		}

		/// <summary>
		/// Not Supported
		/// </summary>
		/// <returns>NotImplementedException</returns>
		[NotSupported]
		public Task<PowerState> GetPowerState()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Not Supported
		/// </summary>
		/// <returns>NotImplementedException</returns>
		[NotSupported]
		public Task<bool> ProcessAttach()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> SetMemory(uint address, byte[] bytes)
		{
			return await Task.Run(() =>
			{
				if (this.ConnectState != ConnectState.Connected) return false;

				Params.XboxConsole.DebugTarget.SetMemory(address, (uint)bytes.Length, bytes, out uint bytesWritten);
				return true;
			});
		}

		/// <summary>
		/// Not Supported
		/// </summary>
		/// <returns>NotImplementedException</returns>
		[NotSupported]
		public Task<bool> SetPowerState(PowerState state, bool isForce = false)
		{
			throw new NotImplementedException();
		}
	}
}
