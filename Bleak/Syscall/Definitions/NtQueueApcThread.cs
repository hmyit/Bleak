﻿using Bleak.Handlers;
using Bleak.Native;
using Bleak.SafeHandle;
using System;
using System.Runtime.InteropServices;

namespace Bleak.Syscall.Definitions
{
    internal class NtQueueApcThread
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate Enumerations.NtStatus NtQueueApcThreadDefinition(SafeThreadHandle threadHandle, IntPtr apcRoutine, IntPtr parameter, IntPtr statusBlockBuffer, ulong reserved);

        private readonly NtQueueApcThreadDefinition _ntQueueApcThreadDelegate;

        internal NtQueueApcThread(Tools syscallTools)
        {
            _ntQueueApcThreadDelegate = syscallTools.CreateDelegateForSyscall<NtQueueApcThreadDefinition>();
        }

        internal void Invoke(SafeThreadHandle threadHandle, IntPtr apcRoutine, IntPtr parameter)
        {
            // Perform the syscall

            var syscallResult = _ntQueueApcThreadDelegate(threadHandle, apcRoutine, parameter, IntPtr.Zero, 0);

            if (syscallResult != Enumerations.NtStatus.Success)
            {
                ExceptionHandler.ThrowWin32Exception("Failed to queue an apc to the apc queue of a thread in the target process", syscallResult);
            }
        }
    }
}
