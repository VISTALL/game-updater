using System;
using System.Threading;
using System.Windows.Forms;
using com.jds.AWLauncher.n_classes.windows.dll;

namespace com.jds.AWLauncher.n_classes.windows
{
    public class Injector
    {
        public static void InjectDLL(IntPtr hProcess, String strDLLName)
        {
            IntPtr bytesout;

            // Length of string containing the DLL file name +1 byte padding  
            Int32 LenWrite = strDLLName.Length + 1;
            // Allocate memory within the virtual address space of the target process  
            IntPtr AllocMem = kernel32.VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000, 0x40); //allocation pour WriteProcessMemory  

            // Write DLL file name to allocated memory in target process  
            kernel32.WriteProcessMemory(hProcess, AllocMem, strDLLName, (UIntPtr)LenWrite, out bytesout);
            // Function pointer "Injector"  
            UIntPtr Injector = kernel32.GetProcAddress(kernel32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (Injector == null)
            {
                MessageBox.Show(" Injector Error! \n ");
                // return failed  
                return;
            }

            // Create thread in target process, and store handle in hThread  
            IntPtr hThread = kernel32.CreateRemoteThread(hProcess, (IntPtr)null, 0, Injector, AllocMem, 0, out bytesout);
            // Make sure thread handle is valid  
            if (hThread == null)
            {
                //incorrect thread handle ... return failed  
                MessageBox.Show(" hThread [ 1 ] Error! \n ");
                return;
            }
            // Time-out is 10 seconds...  
            int Result = kernel32.WaitForSingleObject(hThread, 10 * 1000);
            // Check whether thread timed out...  
            if (Result == 0x00000080L || Result == 0x00000102L || Result == 0xFFFFFFFF)
            {
                /* Thread timed out... */
                MessageBox.Show(" hThread [ 2 ] Error! \n ");
                // Make sure thread handle is valid before closing... prevents crashes.  
                if (hThread != null)
                {
                    //Close thread in target process  
                    kernel32.CloseHandle(hThread);
                }
                return;
            }
            // Sleep thread for 1 second  
            Thread.Sleep(1000);
            // Clear up allocated space ( Allocmem )  
            kernel32.VirtualFreeEx(hProcess, AllocMem, (UIntPtr)0, 0x8000);
            // Make sure thread handle is valid before closing... prevents crashes.  
            if (hThread != null)
            {
                //Close thread in target process  
                kernel32.CloseHandle(hThread);
            }
            MessageBox.Show("Success");
        }  
    }
}
