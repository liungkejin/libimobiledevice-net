//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// <copyright file="MobileBackup2NativeMethods.cs" company="Quamotion">
// Copyright (c) 2016-2020 Quamotion. All rights reserved.
// </copyright>
#pragma warning disable 1591
#pragma warning disable 1572
#pragma warning disable 1573

namespace iMobileDevice.MobileBackup2
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class MobileBackup2NativeMethods
    {
        
        public static MobileBackup2Error mobilebackup2_receive_message(MobileBackup2ClientHandle client, out PlistHandle msgPlist, out string dlmessage)
        {
            System.Runtime.InteropServices.ICustomMarshaler dlmessageMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr dlmessageNative = System.IntPtr.Zero;
            MobileBackup2Error returnValue = MobileBackup2NativeMethods.mobilebackup2_receive_message(client, out msgPlist, out dlmessageNative);
            dlmessage = ((string)dlmessageMarshaler.MarshalNativeToManaged(dlmessageNative));
            dlmessageMarshaler.CleanUpNativeData(dlmessageNative);
            return returnValue;
        }
    }
}
