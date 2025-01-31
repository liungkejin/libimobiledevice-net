//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// <copyright file="WebInspectorApi.cs" company="Quamotion">
// Copyright (c) 2016-2020 Quamotion. All rights reserved.
// </copyright>
#pragma warning disable 1591
#pragma warning disable 1572
#pragma warning disable 1573

namespace iMobileDevice.WebInspector
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class WebInspectorApi : IWebInspectorApi
    {
        
        /// <summary>
        /// Backing field for the <see cref="Parent"/> property
        /// </summary>
        private ILibiMobileDevice parent;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WebInspectorApi"/> class
        /// </summary>
        /// <param name="parent">
        /// The <see cref="ILibiMobileDeviceApi"/> which owns this <see cref="WebInspector"/>.
        /// </param>
        public WebInspectorApi(ILibiMobileDevice parent)
        {
            this.parent = parent;
        }
        
        /// <inheritdoc/>
        public ILibiMobileDevice Parent
        {
            get
            {
                return this.parent;
            }
        }
        
        /// <summary>
        /// Connects to the webinspector service on the specified device.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="service">
        /// The service descriptor returned by lockdownd_start_service.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// webinspector_client_t upon successful return. Must be freed using
        /// webinspector_client_free() after use.
        /// </param>
        /// <returns>
        /// WEBINSPECTOR_E_SUCCESS on success, WEBINSPECTOR_E_INVALID_ARG when
        /// client is NULL, or an WEBINSPECTOR_E_* error code otherwise.
        /// </returns>
        public virtual WebInspectorError webinspector_client_new(iDeviceHandle device, LockdownServiceDescriptorHandle service, out WebInspectorClientHandle client)
        {
            WebInspectorError returnValue;
            returnValue = WebInspectorNativeMethods.webinspector_client_new(device, service, out client);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Starts a new webinspector service on the specified device and connects to it.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// webinspector_client_t upon successful return. Must be freed using
        /// webinspector_client_free() after use.
        /// </param>
        /// <param name="label">
        /// The label to use for communication. Usually the program name.
        /// Pass NULL to disable sending the label in requests to lockdownd.
        /// </param>
        /// <returns>
        /// WEBINSPECTOR_E_SUCCESS on success, or an WEBINSPECTOR_E_* error
        /// code otherwise.
        /// </returns>
        public virtual WebInspectorError webinspector_client_start_service(iDeviceHandle device, out WebInspectorClientHandle client, string label)
        {
            WebInspectorError returnValue;
            returnValue = WebInspectorNativeMethods.webinspector_client_start_service(device, out client, label);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Disconnects a webinspector client from the device and frees up the
        /// webinspector client data.
        /// </summary>
        /// <param name="client">
        /// The webinspector client to disconnect and free.
        /// </param>
        /// <returns>
        /// WEBINSPECTOR_E_SUCCESS on success, WEBINSPECTOR_E_INVALID_ARG when
        /// client is NULL, or an WEBINSPECTOR_E_* error code otherwise.
        /// </returns>
        public virtual WebInspectorError webinspector_client_free(System.IntPtr client)
        {
            return WebInspectorNativeMethods.webinspector_client_free(client);
        }
        
        /// <summary>
        /// Sends a plist to the service.
        /// </summary>
        /// <param name="client">
        /// The webinspector client
        /// </param>
        /// <param name="plist">
        /// The plist to send
        /// </param>
        /// <returns>
        /// DIAGNOSTICS_RELAY_E_SUCCESS on success,
        /// DIAGNOSTICS_RELAY_E_INVALID_ARG when client or plist is NULL
        /// </returns>
        public virtual WebInspectorError webinspector_send(WebInspectorClientHandle client, PlistHandle plist)
        {
            return WebInspectorNativeMethods.webinspector_send(client, plist);
        }
        
        /// <summary>
        /// Receives a plist from the service.
        /// </summary>
        /// <param name="client">
        /// The webinspector client
        /// </param>
        /// <param name="plist">
        /// The plist to store the received data
        /// </param>
        /// <returns>
        /// DIAGNOSTICS_RELAY_E_SUCCESS on success,
        /// DIAGNOSTICS_RELAY_E_INVALID_ARG when client or plist is NULL
        /// </returns>
        public virtual WebInspectorError webinspector_receive(WebInspectorClientHandle client, out PlistHandle plist)
        {
            WebInspectorError returnValue;
            returnValue = WebInspectorNativeMethods.webinspector_receive(client, out plist);
            plist.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Receives a plist using the given webinspector client.
        /// </summary>
        /// <param name="client">
        /// The webinspector client to use for receiving
        /// </param>
        /// <param name="plist">
        /// pointer to a plist_t that will point to the received plist
        /// upon successful return
        /// </param>
        /// <param name="timeout">
        /// Maximum time in milliseconds to wait for data.
        /// </param>
        /// <returns>
        /// WEBINSPECTOR_E_SUCCESS on success,
        /// WEBINSPECTOR_E_INVALID_ARG when client or *plist is NULL,
        /// WEBINSPECTOR_E_PLIST_ERROR when the received data cannot be
        /// converted to a plist, WEBINSPECTOR_E_MUX_ERROR when a
        /// communication error occurs, or WEBINSPECTOR_E_UNKNOWN_ERROR
        /// when an unspecified error occurs.
        /// </returns>
        public virtual WebInspectorError webinspector_receive_with_timeout(WebInspectorClientHandle client, out PlistHandle plist, uint timeoutMs)
        {
            WebInspectorError returnValue;
            returnValue = WebInspectorNativeMethods.webinspector_receive_with_timeout(client, out plist, timeoutMs);
            plist.Api = this.Parent;
            return returnValue;
        }
    }
}
