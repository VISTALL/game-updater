using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using zlib.BZip2;
using zlib.Tar;

namespace Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start write arhive");

            Stream outStream = File.Open("GUpdater.exe.tar.bz2", FileMode.Create);
            outStream = new BZip2OutputStream(outStream, 9);

            TarArchive archive = TarArchive.CreateOutputTarArchive(outStream);

            TarEntry entry = TarEntry.CreateTarEntry(("GUpdater.exe"));
            entry.File = "test/GUpdater.exe";
            
            // entry.GetFileTarHeader(entry.TarHeader, "test/zlib1.dll");
            //entry.TarHeader.LinkName
            archive.WriteEntry(entry, true);

            archive.Close();

            Console.WriteLine("Write success arhive");

            Thread.Sleep(5000);
        }
    }
}
