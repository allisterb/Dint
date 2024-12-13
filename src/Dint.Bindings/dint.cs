namespace Dint;

using System;
using System.Runtime.InteropServices;
using System.Security;

public unsafe class dint
{
    [SuppressUnmanagedCodeSecurity, DllImport("dint", EntryPoint = "Get", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr NewStrategicFormGame([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(CppSharp.Runtime.UTF8Marshaller))] string title, int pc, [MarshalAs(UnmanagedType.LPArray)] string[] players, int[] strategies);

}
