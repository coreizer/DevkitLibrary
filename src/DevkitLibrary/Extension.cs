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
using System.Linq;
using System.Text;

using DevkitLibrary.Devkits;

namespace DevkitLibrary
{
	public sealed class Extension
	{
		private IDevkit devkit;

		public Extension(IDevkit devkit)
		{
			this.devkit = devkit;
		}

		public sbyte ReadSByte(uint address)
		{
			return (sbyte)this.devkit.GetMemory(address, 1)[0];
		}

		public byte ReadByte(uint address)
		{
			return (byte)this.devkit.GetMemory(address, 1)[0];
		}

		public bool ReadBool(uint address)
		{
			return this.ReadByte(address) > 0;
		}

		public float ReadFloat(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 4);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToSingle(bytes, 0);
		}

		public float[] ReadFloats(uint address, int length = 3)
		{
			float[] floats = new float[length >= 0 ? length : 3];
			for (int i = 0; i < length; i++)
			{
				byte[] bytes = this.devkit.GetMemory(address + ((uint)i * 4), 4);
				Array.Reverse(bytes, 0, bytes.Length);
				floats[i] = BitConverter.ToSingle(bytes, 0);
			}

			return floats;
		}

		public byte[] ReadBytes(uint address, int length)
		{
			return this.devkit.GetMemory(address, (uint)length);
		}

		public double ReadDouble(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 8);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToDouble(bytes, 0);
		}

		public short ReadInt16(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 2);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToInt16(bytes, 0);
		}

		public int ReadInt32(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 4);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToInt32(bytes, 0);
		}

		public long ReadInt64(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 8);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToInt64(bytes, 0);
		}

		public ushort ReadUInt16(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 2);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToUInt16(bytes, 0);
		}

		public uint ReadUInt32(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 4);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToUInt32(bytes, 0);
		}

		public ulong ReadUInt64(uint address)
		{
			byte[] bytes = this.devkit.GetMemory(address, 8);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToUInt64(bytes, 0);
		}

		public string ReadString(uint address)
		{
			uint index = 0;
			uint blocksize = 40;
			string text = "";

			while (!text.Contains('\0'))
			{
				byte[] bytes = this.devkit.GetMemory(address + (uint)index, blocksize);
				text += Encoding.UTF8.GetString(bytes);
				index += blocksize;
			}

			return text.Substring(0, text.IndexOf('\0'));
		}

		public string ReadString(uint address, int length)
		{
			byte[] bytes = this.devkit.GetMemory(address, (uint)length);
			Array.Reverse(bytes, 0, bytes.Length);
			string text = Encoding.UTF8.GetString(bytes);
			return text.Substring(0, text.IndexOf("\0"));
		}

		public string[] ReadStrings(uint address, int length)
		{
			byte[] bytes = this.devkit.GetMemory(address, (uint)length);
			string text = Encoding.UTF8.GetString(bytes);
			return text.Split(new char[1]);
		}

		public void WriteString(uint address, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			Array.Resize(ref bytes, bytes.Length + 1);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteSByte(uint address, sbyte value)
		{
			this.devkit.SetMemory(address, new byte[] { (byte)value });
		}

		public void WriteByte(uint address, byte value)
		{
			this.devkit.SetMemory(address, new byte[] { value });
		}

		public void WriteBytes(uint address, byte[] value)
		{
			this.devkit.SetMemory(address, value);
		}

		public void WriteFillByte(uint address, int length, byte value)
		{
			uint index = 0;

			while (index <= length)
			{
				this.WriteByte(address + index, value);
				index += 1;
			}
		}
		public void WriteFillBytes(uint address, int length, byte[] value)
		{
			uint index = 0;

			while (index <= length)
			{
				this.WriteBytes(address + index, value);
				index += 1;
			}
		}

		public void WriteDouble(uint address, double value)
		{
			byte[] bytes = new byte[8];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteFloat(uint address, float value)
		{
			byte[] bytes = new byte[4];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteFloats(uint address, float[] value)
		{
			byte[] bytes = new byte[4];

			for (int i = 0; i < value.Length; i++)
			{
				BitConverter.GetBytes(value[i]).CopyTo(bytes, 0);
				Array.Reverse(bytes, 0, bytes.Length);
				this.devkit.SetMemory(address + (uint)i * 4, bytes);
			}
		}

		public void WriteBool(uint address, bool value)
		{
			this.devkit.SetMemory(address, new byte[] { value ? (byte)0x01 : (byte)0x00 });
		}

		public void WriteInt16(uint address, short value)
		{
			byte[] bytes = new byte[2];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteInt32(uint address, int value)
		{
			byte[] bytes = new byte[4];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteInt64(uint address, long value)
		{
			byte[] bytes = new byte[8];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteUInt16(uint address, ushort value)
		{
			byte[] bytes = new byte[2];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteUInt32(uint address, uint value)
		{
			byte[] bytes = new byte[4];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}

		public void WriteUInt64(uint address, ulong value)
		{
			byte[] bytes = new byte[8];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			Array.Reverse(bytes, 0, bytes.Length);
			this.devkit.SetMemory(address, bytes);
		}
	}
}
