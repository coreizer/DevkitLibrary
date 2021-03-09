using System;

namespace DevkitLibrary
{
   public class DevkitConnectFailedException : Exception 
   {
      public DevkitConnectFailedException() { }

      public DevkitConnectFailedException(string message)
         :base(message)
      {

      }

      public DevkitConnectFailedException(string message, Exception inner)
         : base(message, inner)
      {

      }
   }
}
