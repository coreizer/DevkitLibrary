using System;

namespace DevkitLibrary
{
   public class DevKitAttachProcessFailedException : Exception
   {
      public DevKitAttachProcessFailedException()
      {
      }

      public DevKitAttachProcessFailedException(string message)
          : base(message)
      {
      }

      public DevKitAttachProcessFailedException(string message, Exception inner)
                  : base(message, inner)
      {
      }
   }
}
