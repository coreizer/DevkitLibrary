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
   using System;
   using System.Linq;
   using System.Text;
   using DevkitLibrary.Devkits;
   using DevkitLibrary.Enums;

   public sealed class Extensions
   {
      internal readonly IDevkit _devkit;

      public Extensions(IDevkit devkit)
      {
         this._devkit = devkit;
      }

      #region Reader

      public sbyte ReadSByte(uint address)
      {
         return (sbyte)this._devkit.GetMemory(address, 1)[0];
      }

      public byte ReadByte(uint address)
      {
         return this._devkit.GetMemory(address, 1)[0];
      }

      public bool ReadBool(uint address)
      {
         return this._devkit.GetMemory(address, 1)[0] > 0;
      }

      public float ReadFloat(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 4);
         this.Reverse(buffer, endian);
         return BitConverter.ToSingle(buffer, 0);
      }

      public float[] ReadFloats(uint address, int length = 3, Endianness endian = Endianness.Little)
      {
         var floats = new float[length >= 0 ? length : 3];
         for (var i = 0; i < length; i++) {
            var buffer = this._devkit.GetMemory(address + ((uint)i * 4), 4);
            this.Reverse(buffer, endian);
            floats[i] = BitConverter.ToSingle(buffer, 0);
         }

         return floats;
      }

      public byte[] ReadBytes(uint address, int length)
      {
         return this._devkit.GetMemory(address, (uint)length);
      }

      public double ReadDouble(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 8);
         this.Reverse(buffer, endian);
         return BitConverter.ToDouble(buffer, 0);
      }

      public short ReadInt16(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 2);
         this.Reverse(buffer, endian);
         return BitConverter.ToInt16(buffer, 0);
      }

      public int ReadInt32(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 4);
         this.Reverse(buffer, endian);
         return BitConverter.ToInt32(buffer, 0);
      }

      public long ReadInt64(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 8);
         this.Reverse(buffer, endian);
         return BitConverter.ToInt64(buffer, 0);
      }

      public ushort ReadUInt16(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 2);
         this.Reverse(buffer, endian);
         return BitConverter.ToUInt16(buffer, 0);
      }

      public uint ReadUInt32(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 4);
         this.Reverse(buffer, endian);
         return BitConverter.ToUInt32(buffer, 0);
      }

      public ulong ReadUInt64(uint address, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, 8);
         this.Reverse(buffer, endian);
         return BitConverter.ToUInt64(buffer, 0);
      }

      public string ReadString(uint address)
      {
         uint index = 0;
         uint blocksize = 40;
         var text = "";

         while (!text.Contains('\0')) {
            var bytes = this._devkit.GetMemory(address + index, blocksize);
            text += Encoding.UTF8.GetString(bytes);
            index += blocksize;
         }

         return text.Substring(0, text.IndexOf('\0'));
      }

      public string ReadString(uint address, int length, Endianness endian = Endianness.Little)
      {
         var buffer = this._devkit.GetMemory(address, (uint)length);
         this.Reverse(buffer, endian);
         var text = Encoding.UTF8.GetString(buffer);
         return text.Substring(0, text.IndexOf("\0"));
      }

      public string[] ReadStrings(uint address, int length)
      {
         var buffer = this._devkit.GetMemory(address, (uint)length);
         var text = Encoding.UTF8.GetString(buffer);
         return text.Split(new char[1]);
      }

      #endregion

      #region Writer

      public void WriteString(uint address, string value)
      {
         var buffer = Encoding.UTF8.GetBytes(value);
         Array.Resize(ref buffer, buffer.Length + 1);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteSByte(uint address, sbyte value)
      {
         this._devkit.SetMemory(address, new byte[] { (byte)value });
      }

      public void WriteByte(uint address, byte value)
      {
         this._devkit.SetMemory(address, new byte[] { value });
      }

      public void WriteBytes(uint address, params byte[] value)
      {
         this._devkit.SetMemory(address, value);
      }

      public void WriteFillByte(uint address, int length, byte value)
      {
         uint index = 0;

         while (index <= length) {
            this.WriteByte(address + index, value);
            index += 1;
         }
      }
      public void WriteFillBytes(uint address, int length, byte[] value)
      {
         uint index = 0;

         while (index <= length) {
            this.WriteBytes(address + index, value);
            index += 1;
         }
      }

      public void WriteDouble(uint address, double value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[8];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteFloat(uint address, float value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[4];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteFloats(uint address, float[] value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[4];

         for (var i = 0; i < value.Length; i++) {
            BitConverter.GetBytes(value[i]).CopyTo(buffer, 0);
            this.Reverse(buffer, endian);
            this._devkit.SetMemory(address + (uint)i * 4, buffer);
         }
      }

      public void WriteBool(uint address, bool value)
      {
         this._devkit.SetMemory(address, new byte[] { value ? (byte)0x01 : (byte)0x00 });
      }

      public void WriteInt16(uint address, short value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[2];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteInt32(uint address, int value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[4];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteInt64(uint address, long value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[8];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteUInt16(uint address, ushort value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[2];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteUInt32(uint address, uint value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[4];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      public void WriteUInt64(uint address, ulong value, Endianness endian = Endianness.Little)
      {
         var buffer = new byte[8];
         BitConverter.GetBytes(value).CopyTo(buffer, 0);
         this.Reverse(buffer, endian);
         this._devkit.SetMemory(address, buffer);
      }

      private void Reverse(byte[] buffer, Endianness endian)
      {
         if (endian == Endianness.Little || this._devkit.Endian == Endianness.Little && endian == Endianness.Little) {
            Array.Reverse(buffer, 0, buffer.Length);
         }
      }

      #endregion
   }
}
