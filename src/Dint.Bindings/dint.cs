namespace Dint;

using System;
using System.Runtime.InteropServices;
using System.Security;

public unsafe class dint
{
    [SuppressUnmanagedCodeSecurity, DllImport("dint", EntryPoint = "TEST_Buffer", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr TEST_Buffer(IntPtr buf, int size);

}
