using System;
using Reloaded.Hooks;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Adventure.SDK.Library.Classes.Native.GameObject;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks
{
    public unsafe class LoadObjectHook : Main.Hook<LoadGameObject>
    {
        static LoadObjectHook()
        {
            Address = 0x40B860;
            Function = ReplacementFunction;
            HookFunction = new Hook<LoadGameObject>(Function, Address).Activate();
        }

        private static GameObject* ReplacementFunction(byte flags, int index, IntPtr loadSub)
        {
            GameObject* loadedObject = HookFunction.OriginalFunction(flags, index, loadSub);
            
            /* TODO STUFF*/

            return loadedObject;
        }
    }
}
