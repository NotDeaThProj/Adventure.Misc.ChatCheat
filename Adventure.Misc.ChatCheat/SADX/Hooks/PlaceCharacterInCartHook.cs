using System.Runtime.InteropServices;
using Reloaded.Hooks;
using Reloaded.Hooks.Definitions.X86;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;
using static Adventure.SDK.Library.Classes.Native.Player;
using Adventure.SDK.Library.API.Game;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Hooks
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [Function(new[] { Register.eax }, Register.eax, StackCleanup.Caller)]
    public unsafe delegate void PlaceCharacterInCart(GameObject* obj);

    public unsafe class PlaceCharacterInCartHook : Main.Hook<PlaceCharacterInCart>
    {
        private static GameHandler _gameHandler = new GameHandler();

        static PlaceCharacterInCartHook()
        {
            Address = 0x7981F0;
            Function = ReplacementFunction;
            HookFunction = new Hook<PlaceCharacterInCart>(Function, Address).Activate();
        }

        public static void ReplacementFunction(GameObject* obj)
        {
            switch (_gameHandler.CurrentStage)
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
