// QStart v1.2 (c) 2021-2022 Sensei (aka 'Q')
// Quietly start a specific executable or script without opening the console.
//
// Usage:
// QStart filename
//
// Compilation:
// %SYSTEMROOT%\Microsoft.NET\Framework\v3.5\csc /target:winexe QStart.cs
//
// Example:
// QStart QScreenCapture -v -d picture.jpg >>output.txt

using System;
using System.Diagnostics;

public class QStart {
   private static string GetNewArgs( string [] args ) {
      string new_args = null;
      if( args.Length >= 2 ) {
        string filename = args[ 0 ];
        int start_index = Environment.CommandLine.IndexOf( filename );
        if( start_index == -1 ) throw( new ArgumentException() );
        int length = filename.Length;

        if( Environment.CommandLine.IndexOf( "\"" + filename + "\"" ) != -1 ) {
           start_index--;
           length += 2;
        }

        new_args = Environment.CommandLine.Substring( start_index + length );
        new_args.Trim( ' ' );
      }
      return( new_args );
   }

   public static void Main( string [] args ) {
      if( args.Length > 0 ) {
         try {
            string filename = args[ 0 ];
            string new_args = GetNewArgs( args );

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = filename;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            process.StartInfo.Arguments = new_args;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler( ( sender, e ) => {
               if( e.Data != null ) {
                  Console.WriteLine( e.Data );
               }
  			   });
            process.ErrorDataReceived += new DataReceivedEventHandler( ( sender, e ) => {
               if( e.Data != null ) {
                  Console.Error.WriteLine( e.Data );
               }
  			   });
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            Environment.Exit( process.ExitCode() );
         } catch( Exception e ) {
            Console.Error.WriteLine( e.Message ); // It won't be called if compiling with /target:winexe
            Environment.Exit( 20 );
         }
      } else {
         // It won't be called if compiling with /target:winexe
         Console.WriteLine( "QStart v1.2 (c) 2021-2022 Sensei (aka 'Q')" );
         Console.WriteLine( "Quietly start a specific executable or script without opening the console." );
         Console.WriteLine();
         Console.WriteLine( "Usage:" );
         Console.WriteLine( "QStart filename [args]" );
         Environment.Exit( 0 );
      }
   }
}
