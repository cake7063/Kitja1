using System;
using System.IO;
using System.IO.Compression;

namespace ConsoleApplication
{
    class Program
    {
        private static string directoryPath = @"C:\Users\Kitja\Desktop\Kitja_366081_GameTools_Final\Kitja_366081_GameTools_Final\MSBUILD";

        static void Main(string[] args)
        {
            string startPath = @"C:\Users\Kitja\Desktop\Kitja_366081_GameTools_Final\Kitja_366081_GameTools_Final\MSBUILD";
            string zipPath = @"C:\Users\Kitja\Desktop\Kitja_366081_GameTools_Final\Kitja_366081_GameTools_Final\Release.zip";
            string extractPath = @"C:\Users\Kitja\Desktop\Kitja_366081_GameTools_Final\Kitja_366081_GameTools_Final\bin";

            ZipFile.CreateFromDirectory(startPath, zipPath);

            ZipFile.ExtractToDirectory(zipPath, extractPath);

            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            Compress(directorySelected);

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
            }
            Console.WriteLine(" ");
            Console.WriteLine("msbuild files has been copied to bin.");
            Console.WriteLine(".gz files has been created in MSBUILD folder");
            Console.WriteLine("Release.zip has been created.");
            Console.WriteLine(" ");
            Console.WriteLine("In order to run it again, you need to delete the .exe files in the bin, .gz files in the MSBUILD folder, and Release.zip. ");
            Console.WriteLine(" ");

        }


        public static void Compress(DirectoryInfo directorySelected)
        {
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);

                            }
                        }
                        FileInfo info = new FileInfo(directoryPath + "\\" + fileToCompress.Name + ".gz");
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());
                    }

                }
            }
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }


        }
    }
}