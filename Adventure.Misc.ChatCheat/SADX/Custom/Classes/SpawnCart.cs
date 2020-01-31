using System;
using Adventure.SDK.Library.API.Objects;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Reloaded.Memory.Interop;
using static Adventure.SDK.Library.Classes.Native.Player;
using static Adventure.SDK.Library.Classes.Native.PVM;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
{
    public unsafe class SpawnCart : Cart
    {
        private static Pinnable<SETObjectData> _setData = new Pinnable<SETObjectData>(new SETObjectData()
        {
            LoadCount = 1,
            field_1 = 0,
            Flags = -32767,
            Distance = 4000100
        });

        public SpawnCart(CartColor color) : base()
        {
            // Load Cart textures every time
            LoadPVMFile("OBJ_SHAREOBJ", (IntPtr)0x38AEB70);

            // Store information of the player character
            Info* characterInfo = GetCharacterGameObject(Players.P1)->Info;

            // Fixes cart despawning when far away from camera
            _setData.Value.ObjectInstance = Handle;
            Handle->SETData = new SETDataUnion()
            {
                SETData = _setData.Pointer
            };

            // Set Properties of Cart
            IsUnoccupied = true;
            Color = color;
            Size = (characterInfo->CharacterID) switch
            {
                Character.Gamma => CartSize.Long,
                Character.Big => CartSize.Wide,
                _ => CartSize.Normal,
            };

            // Teleport Cart to the player's position
            Position = characterInfo->Position;
        }
    }
}
