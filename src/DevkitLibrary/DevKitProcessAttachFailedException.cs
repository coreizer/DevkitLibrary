using System;

namespace DevkitLibrary
{
   public class DevKitProcessAttachFailedException : Exception
   {
      public DevKitProcessAttachFailedException()
      {
      }

      public DevKitProcessAttachFailedException(string message)
          : base(message)
      {
      }

      public DevKitProcessAttachFailedException(string message, Exception inner)
                  : base(message, inner)
      {
      }
   }
}
