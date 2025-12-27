using System;
using System.Runtime.InteropServices;
using System.Text;

using static IO.Kernel32;

namespace IO {

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Guid("B4C35607-C68F-4C54-B438-F2EE2C239033")]
    [ProgId("Console.Writer")]
    public class StdWriter {

        private IntPtr stdoutHandle;
        private IntPtr stderrHandle;

        public StdWriter() {
            stdoutHandle = GetStdHandle(-11); // STD_OUTPUT_HANDLE
            stderrHandle = GetStdHandle(-12); // STD_ERROR_HANDLE
        }

        public void WriteOut(string text) {
            WriteToHandle(stdoutHandle, text);
        }

        public void WriteErr(string text) {
            WriteToHandle(stderrHandle, text);
        }

        private void WriteToHandle(IntPtr handle, string text) {
            if (handle == new IntPtr(-1)) return; // INVALID_HANDLE_VALUE
            
            byte[] bytes = Encoding.Default.GetBytes(text);
            uint bytesWritten = 0;
            WriteFile(handle, bytes, (uint)bytes.Length, out bytesWritten, IntPtr.Zero);
        }
    }
}