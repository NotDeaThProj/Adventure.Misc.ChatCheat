using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using System.Runtime.InteropServices;
using Adventure.Misc.ChatCheat.ReloadedII.Chat;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks
{
    public class OnFrameHook
    {
        // Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(CallingConventions.Cdecl)]
        private delegate int OnFrame();

        // Variables/Constants
        private static long _hookAddress = 0x426050;
        private static OnFrame _function = CommandExecution.ExecuteCommands;
        private static IHook<OnFrame> _onFrame = new Hook<OnFrame>(_function, _hookAddress).Activate();

        public long HookAddress { get => _hookAddress; }

        public bool IsEnabled
        {
            get => _onFrame.IsHookEnabled;
            set
            {
                if (value)
                    _onFrame.Enable();
                else
                    _onFrame.Disable();
            }
        }

        public int OriginalFunction
        {
            get => _onFrame.OriginalFunction();
        }
    }
}
