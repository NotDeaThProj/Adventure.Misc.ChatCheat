using Reloaded.Hooks.Definitions.X86;
using Reloaded.Hooks.X86;
using System;
using System.Runtime.InteropServices;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Adventure.SDK.Library.Classes.Native.GameObject;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects
{
    public unsafe class CustomEggmanDisplay
    {
        // Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [Function(new[] { Register.edi, Register.esi }, Register.eax, StackCleanup.Caller)]
        private unsafe delegate void EggmanDisplay(CharacterData* characterData, Info* gameObjectInfo);
       
        // Variables/Constants
        private static readonly EggmanDisplay _eggmanDisplay = Wrapper.Create<EggmanDisplay>(0x7B4450);
        private readonly ReverseWrapper<FunctionPointer> _displayFunction;

        // Properties
        public IntPtr DisplayFunction
        {
            get => _displayFunction.WrapperPointer;
        }

        public CustomEggmanDisplay()
        {
            _displayFunction = new ReverseWrapper<FunctionPointer>(Display);
        }

        public virtual void Display(GameObject* obj)
        {
            _eggmanDisplay(obj->ActorData->CharacterData, obj->Info);
        }
    }
}
