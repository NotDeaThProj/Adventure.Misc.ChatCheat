using Reloaded.Hooks.Definitions.X86;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;
using Adventure.SDK.Library.Definitions.Enums;
using static Adventure.SDK.Library.Classes.Native.Player;
using Reloaded.Hooks;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [Function(new[] { Register.eax }, Register.eax, StackCleanup.Caller)]
    public unsafe delegate void PlaceCharacterInCart(GameObject* obj);

    public unsafe class PlaceCharacterInCartHook : Main.Hook<PlaceCharacterInCart>
    {
        static PlaceCharacterInCartHook()
        {
            Address = 0x7981F0;
            Function = ReplacementFunction;
            HookFunction = new Hook<PlaceCharacterInCart>(Function, Address).Activate();
        }

        public static void ReplacementFunction(GameObject* obj)
        {
            Stage* currentLevel = (Stage*)0x3B22DCC;

            switch (*currentLevel)
            {
                case Stage.TwinkleCircuit:
                case Stage.TwinkleCircuitOne:
                case Stage.TwinkleCircuitTwo:
                case Stage.TwinkleCircuitThree:
                case Stage.TwinkleCircuitFour:
                case Stage.TwinkleCircuitFive:
                case Stage.TwinkleCircuitSix:
                case Stage.SkyChaseOne:
                case Stage.SkyChaseTwo:
                    HookFunction.OriginalFunction(obj);
                    break;
                default:
                    ChangePlayerAction(Players.P1, PlayerAction.BumperCar);
                    obj->Info->Action = 3;
                    break;
            }
        }
    }
}
