using Adventure.SDK.Library.API.Objects;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Adventure.SDK.Library.Classes.Native.GameObject;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
{
    public unsafe class SpawnCart : Objects.Cart
    {
        public SpawnCart(Color color) : base()
        {
            // Store information of the player character
            Info* characterInfo = GetCharacterGameObject(Players.P1)->Info;

            /* TODO - FIX SETDATA PROPERTY STUFF */

            // Set Properties of Cart
            IsUnoccupied = true;
            Color = color;
            Size = (characterInfo->CharacterID) switch
            {
                Character.Gamma => Size.Long,
                Character.Big => Size.Wide,
                _ => Size.Normal,
            };

            // Teleport Cart to the player's position
            Position = Info->Position;
        }
    }
}
