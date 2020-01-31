using Reloaded.Hooks;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using System.Runtime.InteropServices;
using Adventure.Misc.ChatCheat.ReloadedII.Chat;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks
{
    // Delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [Function(CallingConventions.Cdecl)]
    public delegate int OnFrame();

    public class OnFrameHook : Main.Hook<OnFrame>
    {
        static OnFrameHook()
        {
            Address = 0x426050;
            Function = CommandExecution.ExecuteCommands;
            HookFunction = new Hook<OnFrame>(Function, Address).Activate();
        }

        public int OriginalFunction
        {
            get => HookFunction.OriginalFunction();
        }
    }
}
