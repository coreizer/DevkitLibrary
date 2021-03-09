using System;

namespace DevkitLibrary
{
   public class DevkitNotFoundException : Exception
   {
      public DevkitNotFoundException() { }

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
