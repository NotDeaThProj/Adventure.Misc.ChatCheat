using System;
using Adventure.SDK.Library.API.Game;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.Definitions.Enums.Objects;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Reloaded.Memory.Interop;
using static Adventure.SDK.Library.Classes.Native.PVM;
using static Adventure.SDK.Library.Classes.Native.Player;
using static Adventure.SDK.Library.Classes.Native.GameObject;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
{
    public unsafe class Cart : SDK.Library.API.Objects.StageObjects.TwinklePark.Cart
    {
        private static GameHandler _gameHandler = new GameHandler();

        public Cart(CartColor color) : base()
        {
            // Make sure that the cart is controllable
            _gameHandler.IsCartOnAutoPilot = false;

            // Load cart textures every time
            LoadPVMFile("OBJ_SHAREOBJ", (IntPtr)0x38AEB70);

            // Delete cart that is occupied by player before spawning a new one
            GameObject* NextGameObject = Handle->Next;
            while ((IntPtr)NextGameObject != IntPtr.Zero)
            {
                if (NextGameObject->executor == MainFunction && NextGameObject->Info->Action == (byte)CartAction.OccupiedByPlayer)
                {
                    DeleteNativeGameObject(NextGameObject);
                    break;
                }
                else
                {
                    NextGameObject = NextGameObject->Next;
                }
            }

            // Store information of the player character
            Info* characterInfo = GetCharacterGameObject(Players.P1)->Info;

            // Fixes cart despawning when far away from camera
            SETData->LoadCount = 1;
            SETData->Flags = -32767;
            SETData->Distance = 4000100;
            SETData->ObjectInstance = Handle;

            // Set properties of the cart
            IsUnoccupied = true;
            Color = color;
            Size = (characterInfo->CharacterID) switch
            {
                Character.Gamma => CartSize.Long,
                Character.Big => CartSize.Wide,
                _ => CartSize.Normal,
            };

            // Teleport cart to the player's position
            Info->Position = characterInfo->Position;
        }
    }
}
