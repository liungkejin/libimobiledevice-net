# wtlibmobiledevice-net

因为 [imobiledevice-win32](https://github.com/libimobiledevice-win32/imobiledevice-net) 项目已经停止更新，而我又需要在windows系统上使用 `libimobiledevice` 的最新代码，
所以拷贝了 `imobiledevice-win32` 的代码，重新编译了 `libimobiledevice` 并修改了 `imobiledevice-win32` 的一点点代码

此项目只编译了 x86 的 `libimobiledevice`，没有编译 x64 版本，且没有编译 `libirecovery`` 和 `libimobiledevice-activation`。
所以如果你想要使用 x64 版本 或者 `libirecovery`、`libimobiledevice-activation`，需要自己编译

## 编译 libimobiledevice (可直接使用 `build_libimobiledevice.sh`)

libimobiledevice 依赖于

* libplist
* libimobiledevice-glue
* libusbmuxd

首先我们需要下载安装 [MSYS2](https://www.msys2.org/). 

然后，如果我们要编译 x86 版本，则打开 MSYS2 MINGW32
如果编译 x64 版本，则打开 MSYS2 MINGW64

首先我们需要安装编译工具和相关依赖

```shell
# for x86
pacman -S mingw-w64-i686-toolchain 
pacman -S --needed base-devel msys2-devel
pacman -S git mingw-w64-i686-cython0 mingw-w64-i686-libzip
pacman -S make automake autoconf libtool pkgconf openssl openssl-devel

# for x64
pacman -S mingw-w64-x64-toolchain 
pacman -S --needed base-devel msys2-devel
pacman -S git mingw-w64-x64-cython0 mingw-w64-x64-libzip
pacman -S make automake autoconf libtool pkgconf openssl openssl-devel
```

### 编译 libplist

```shell
cd libimobiledevice
git clone https://github.com/liungkejin/libplist.git
cd libplist
./autogen.sh && make && make install
```

### 编译 libimobiledevice-glue

```shell
cd libimobiledevice
git clone https://github.com/liungkejin/libimobiledevice-glue.git
cd libimobiledevice-glue
./autogen.sh && make && make install
```

### 编译 libusbmuxd

```shell
cd libimobiledevice
git clone https://github.com/liungkejin/libusbmuxd.git
cd libusbmuxd
./autogen.sh && make && make install
```

### 编译 libimobiledevice

```shell
cd libimobiledevice
git clone https://github.com/liungkejin/libimobiledevice.git
cd libimobiledevice
./autogen.sh && make && make install
```

编译完成后，dll库文件可以在 `MSYS2安装目录\mingw32\bin` 中找到（x64 为 `MSYS2安装目录\mingw64\bin`）

dll 不能直接在 Visual Studio C++ 工程中使用（但是可以直接使用在C#工程中使用）

需要使用 `pexports.exe` 生成 `libxxx.def`

```shell
pexports.exe xxxx.dll > xxxx.def
```

再打开 `x86 native tools command prompat` 或者 `x64 native tools command prompat`, 执行

```shell
lib.exe /def:xxxx.def /out:xxxx.lib
```


### wtimobiledevice-net

因为我只需要在 Windows .net framework 上运行，所以我修改了部分代码

```C#
    public static class NativeLibraries
    {
        private const string WindowsRuntime64Bit = "win-x64";
        private const string WindowsRuntime32Bit = "win-x86";

        // 直接写死路径，暂时不支持动态路径
        private const string LIBDIRECTORY               = "win-x86\\";
        public const string LIBIMOBILEDEVICE_DLL        = LIBDIRECTORY + "libimobiledevice-1.0.dll";
        public const string LIBIMOBILEDEVICE_GLUE_DLL   = LIBDIRECTORY + "libimobiledevice-glue-1.0.dll";
        public const string LIBUSBMUXD_DLL              = LIBDIRECTORY + "libusbmuxd-2.0.dll";
        public const string LIBPLIST_DLL                = LIBDIRECTORY + "libplist-2.0.dll";

        // not supported
        public const string LIBIRECOVERY_DLL = "libirecovery";
        public const string LIBIMOBILEDEVICE_ACTIVATION_DLL = "libimobiledevice-activation";

        ...

        private static void Load(string directory)
        {
            return; // not supported
            ...
        }

        ...
    }
```

```C#
    public partial class MobileSyncNativeMethods
    {
        ...

        /// <summary>
        /// Allocates memory for a new anchors struct initialized with the passed anchors.
        /// MOBILESYNC_E_SUCCESS on success
        /// </summary>
        /// <param name="device_anchor">
        /// An anchor the device reported the last time or NULL
        /// if none is known yet which for instance is true on first synchronization.
        /// </param>
        /// <param name="computer_anchor">
        /// An arbitrary string to use as anchor for the computer.
        /// </param>
        /// <param name="client">
        /// Pointer that will be set to a newly allocated
        /// #mobilesync_anchors_t struct. Must be freed using mobilesync_anchors_free().
        /// </param>
        [System.Runtime.InteropServices.DllImportAttribute(MobileSyncNativeMethods.LibraryName, EntryPoint="mobilesync_anchors_new", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern MobileSyncAnchorsHandle mobilesync_anchors_new([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string deviceAnchor, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string computerAnchor);
        
        /// <summary>
        /// Free memory used by anchors.
        /// MOBILESYNC_E_SUCCESS on success
        /// </summary>
        /// <param name="anchors">
        /// The anchors to free.
        /// </param>
        [System.Runtime.InteropServices.DllImportAttribute(MobileSyncNativeMethods.LibraryName, EntryPoint="mobilesync_anchors_free", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void mobilesync_anchors_free(System.IntPtr anchors);

        ...
    }

    
    public partial class MobileSyncApi : IMobileSyncApi
    {
        ...

        /// <summary>
        /// Allocates memory for a new anchors struct initialized with the passed anchors.
        /// MOBILESYNC_E_SUCCESS on success
        /// </summary>
        /// <param name="device_anchor">
        /// An anchor the device reported the last time or NULL
        /// if none is known yet which for instance is true on first synchronization.
        /// </param>
        /// <param name="computer_anchor">
        /// An arbitrary string to use as anchor for the computer.
        /// </param>
        /// <param name="client">
        /// Pointer that will be set to a newly allocated
        /// #mobilesync_anchors_t struct. Must be freed using mobilesync_anchors_free().
        /// </param>
        public virtual MobileSyncError mobilesync_anchors_new(string deviceAnchor, string computerAnchor, out MobileSyncAnchorsHandle anchor)
        {
            anchor = MobileSyncNativeMethods.mobilesync_anchors_new(deviceAnchor, computerAnchor);
            anchor.Api = this.Parent;
            return MobileSyncError.Success;
        }
        
        /// <summary>
        /// Free memory used by anchors.
        /// MOBILESYNC_E_SUCCESS on success
        /// </summary>
        /// <param name="anchors">
        /// The anchors to free.
        /// </param>
        public virtual MobileSyncError mobilesync_anchors_free(System.IntPtr anchors)
        {
            MobileSyncNativeMethods.mobilesync_anchors_free(anchors);
            return MobileSyncError.Success;
        }

        ...

    }
```