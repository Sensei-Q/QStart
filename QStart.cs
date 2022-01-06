// QStart v1.0.1 (c) 2021-2022 Sensei (aka 'Q')
// Quietly start a specific executable or script without opening the console.
//
// Usage:
// QStart filename
//
// Compilation:
// %SYSTEMROOT%\Microsoft.NET\Framework\v3.5\csc /target:winexe QStart.cs
//

using System;
using System.Diagnostics;

public class QStart {
   public static void Main( string [] args ) {
      if( args.Length > 0 ) {
         try {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = args[0];
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            process.Start();
            process.WaitForExit();
         } catch( Exception e ) {
            Console.WriteLine( e.Message ); // It won't be called if compiling with /target:winexe
         }
      } else {
           // It won't be called if compiling with /target:winexe
           Console.WriteLine( "QStart v1.0.1 (c) 2021-2022 Sensei (aka 'Q')" );
           Console.WriteLine( "Usage:" );
           Console.WriteLine( "QStart filename" );
      }
   }
}
