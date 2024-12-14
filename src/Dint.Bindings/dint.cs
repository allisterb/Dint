namespace Dint;

using System;
using System.Runtime.InteropServices;
using System.Security;

public unsafe static class dint
{
    [SuppressUnmanagedCodeSecurity, DllImport("dint.dll", EntryPoint = "__Buffer_Test", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Buffer_Test(IntPtr buf);

}
