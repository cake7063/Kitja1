using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BatchFileFramework;
namespace batchFileProcessor
{
	class Program
	{
		static long totalFilesLen = 0;

		static void testNotify(String msg)
		{
			Console.WriteLine(msg);
		}

		static void findTotalFilesLength(IFileAccessLogic lo, System.IO.FileInfo fi)
		{
			totalFilesLen += fi.Length;
		}

		static void Main(string[] args)
		{
			if(args.Length < 1)
			{
				Console.WriteLine("Missing file or folder path: \nUsage: batchFileProcessor filepath(folderpath)");
				return;
			}

			FileAccessLogic accessor = new FileAccessLogic();
			accessor.OnNotify += testNotify;
			accessor.OnProcess += findTotalFilesLength;

			accessor.Recursive = true;
			accessor.Execute(args[0]);

			Console.WriteLine("Total Files length in directory {0} is {1}", args[0], totalFilesLen);
			// Keep the console window open in debug mode.
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

		}		       

	}
}

/* How to call a external exe :
 using System.Diagnostics;

class Program
{
    static void Main()
    {
        LaunchCommandLineApp();
    }

    /// <summary>
    /// Launch the legacy application with some options set.
    /// </summary>
    static void LaunchCommandLineApp()
    {
        // For the example
        const string ex1 = "C:\\";
        const string ex2 = "C:\\Dir";

        // Use ProcessStartInfo class
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = false;
        startInfo.UseShellExecute = false;
        startInfo.FileName = "dcm2jpg.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

        try
        {
            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }
        catch
        {
             // Log error.
        }
    }
}
 
 * 
 */
