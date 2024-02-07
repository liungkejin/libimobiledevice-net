#!/bin/bash

# 先下载 MySYS2
# 如果要编译 x64 就打开 MySYS2
# 如果要编译 x86 打开 MySYS2 WINGW32
# 执行 prepare

function prepare() {
    pacman -S mingw-w64-i686-toolchain 
    pacman -S --needed base-devel msys2-devel
    pacman -S git mingw-w64-i686-cython0 mingw-w64-i686-libzip
    pacman -S make automake autoconf libtool pkgconf openssl openssl-devel
}

host=$(uname)

case $host in
    "MINGW32*")
        echo "build for x86"
        ;;
    *)
        echo "build for x64"
        ;;
esac

function build() {
    local name="$1"
    echo "build $name -------------------------------------------------------"
    if ! [ -d $name ]; then
        git clone "https://github.com/liungkejin/$name.git"
    fi

    if ! [ -f $name/autogen.sh ]; then
        echo "autogen.sh not exist!"
        exit -1;
    fi

    cd $name

    ./autogen.sh
    if [ $? -ne 0]; then
        exit -1;
    fi

    make clean
    make
    if [ $? -ne 0 ]; then
        exit -1;
    fi

    make install
    if [ $? -ne 0 ]; then
        exit -1;
    fi

    cd ..
    echo "build $name success -------------------------------------------"
}

mkdir -p libimobiledevice
cd libimobiledevice

build libplist
build libimobiledevice-glue
build libusbmuxd
build libimobiledevice

echo "编译完成"

# 编译完成后，生成的 dll 不能直接被 visual studio C++ 项目 使用，需要进行转换
# pexports.exe xxxx.dll > xxxx.def
# 打开 x86 native tools command prompt
# lib.exe /def:xxxx.def /out:xxxx.lib
# 然后就可以使用 xxxx.lib 了, 他们的引用关系：
# libimobiledevice.lib
#    \_ libimobiledevice-1.0.dll
#               \_ libcrypto-3.dll
#               \_ libssl-3.dll
#               \_ libimobiledevice-glue.dll
#               \_ libplist-2.0.dll
#               \_ libusbmuxd-2.0.dll