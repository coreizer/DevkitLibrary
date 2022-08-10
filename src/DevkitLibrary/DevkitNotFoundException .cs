﻿#region License Information (GPL v3)

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

   public class DevkitNotFoundException : Exception
   {
      public DevkitNotFoundException()
      {
      }

      public DevkitNotFoundException(string message)
          : base(message)
      {
      }

      public DevkitNotFoundException(string message, Exception inner)
          : base(message, inner)
      {
      }
   }
}
