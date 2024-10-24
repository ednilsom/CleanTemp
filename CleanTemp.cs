using System;
using System.IO;
using System.Reflection;

[assembly: AssemblyVersion ( "1.0.0.0" )]

Console.WriteLine ( "Temp Folder Cleanup Tool" );
Console.WriteLine ( );

if ( args.Length == 0 || args[0] != "-r" )
{
   Console.WriteLine ( "Usage: CleanTemp -r = run the cleaning." );
}
else
{
   string[] tempPath = [Path.GetTempPath ( ), "C:\\Windows\\Temp"];
   int removedFolders = 0;
   int removedFiles = 0;
   int openFolders = 0;
   int openFiles = 0;
   string drive = Directory.GetCurrentDirectory ( ).Split ( '\\' )[0];
   DriveInfo d = new ( drive );
   long beforeFreeSpace = d.TotalFreeSpace;
   foreach ( string tempFolder in tempPath )
   {
      string[] folders;
      string[] files;
      try
      {
         Console.WriteLine ( $"Processing {tempFolder}." );
         folders = Directory.GetDirectories ( tempFolder );
         files = Directory.GetFiles ( tempFolder );
      }
      catch
      {
         Console.WriteLine ( $"Failed searching folder {tempFolder}." );
         openFolders++;
         continue;
      }

      foreach ( string file in files )
      {
         try
         {
            File.SetAttributes ( file, FileAttributes.Normal );
            File.Delete ( file );
            removedFiles++;
         }
         catch
         {
            openFiles++;
            continue;
         }
         Console.WriteLine ( $"Removing file {file}." );
      }
      foreach ( string folder in folders )
      {
         try
         {
            Directory.Delete ( folder, true );
            Console.WriteLine ( $"Removing folder {folder}." );
            removedFolders++;
         }
         catch
         {
            openFolders++;
            continue;
         }
      }
   }
   long diffFreeSpace = d.TotalFreeSpace - beforeFreeSpace;
   Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine ( $"{removedFiles} files removed." );
   Console.WriteLine ( $"{removedFolders} folders removed" );
   Console.ResetColor ( );
   Console.ForegroundColor = ConsoleColor.Yellow;
   Console.WriteLine ( $"{openFiles + openFolders} files and folders where open" );
   Console.WriteLine ( $"{diffFreeSpace:N0} bytes saved" );
   Console.ResetColor ( );
}
