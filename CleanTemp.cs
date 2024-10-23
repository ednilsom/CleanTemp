using System;
using System.IO;

class CleanTemp
{
   public class Global
   {
      public static string[] TempPath = [Path.GetTempPath ( ), "C:\\Windows\\Temp"];
   }

   public static void Main ( string[] args )
   {
      string arg = string.Empty;
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine ( "Temp Folder Cleanup Tool" );
      Console.WriteLine ( );

      if ( args.Length > 0 )
      {
         arg = args[0];
      }
      if ( args.Length == 0 || arg != "-r" )
      {
         Console.WriteLine ( "Usage: CleanTemp -r = run the cleaning." );
         return;
      }

      TempCleanup ( );

   }
   public static void TempCleanup ( )
   {
      int removedFolders = 0;
      int removedFiles = 0;
      int openFolders = 0;
      int openFiles = 0;
      foreach ( string tempFolder in Global.TempPath )
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
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine ( $"{removedFiles} files removed." );
      Console.WriteLine ( $"{removedFolders} folders removed" );
      Console.ResetColor ( );
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine ( $"{openFiles + openFolders} files and folders where open" );
      Console.ResetColor ( );
   }
}
