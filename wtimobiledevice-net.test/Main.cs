using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.Plist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wtimobiledevice_net.test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void test()
        {
            //NativeLibraries.Load();

            IiDeviceApi idevice = LibiMobileDevice.Instance.iDevice;
            idevice.idevice_set_debug_level(1);
            idevice.idevice_set_debug_callback((ptr) =>
            {
                Console.WriteLine("idevice debug: " + Marshal.PtrToStringAnsi(ptr));
            });

            ReadOnlyCollection<string> udids;
            int count = 0;
            var ret = idevice.idevice_get_device_list(out udids, ref count);
            Console.WriteLine("idevice_get_device_list: " + ret + ", count: " + count);

            if (count < 1) return;

            ILockdownApi lockdown = LibiMobileDevice.Instance.Lockdown;
            iDeviceHandle deviceHandle = null;

            foreach (string udid in udids)
            {
                idevice.idevice_new(out deviceHandle, udid).ThrowOnError();
                Console.WriteLine("idevice_new success: " + udid);

                LockdownClientHandle lockdownHandle;
                LockdownError lockdownError = lockdown.lockdownd_client_new(deviceHandle, out lockdownHandle, "ideviceinfo");
                Console.WriteLine("lockdownd_client_new result: " + lockdownError);
                if (lockdownError == LockdownError.Success)
                {
                    string hostName;
                    lockdown.lockdownd_get_device_name(lockdownHandle, out hostName).ThrowOnError();

                    PlistHandle productTypeHandle;
                    lockdown.lockdownd_get_value(lockdownHandle, null, "ProductType", out productTypeHandle)
                        .ThrowOnError();

                    string productType;
                    LibiMobileDevice.Instance.Plist.plist_get_string_val(productTypeHandle, out productType);

                    Console.WriteLine("ios device: " + hostName + ", product type: " + productType);

                    productTypeHandle.Dispose();
                    lockdownHandle.Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }
    }
}
