using System;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Main;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX
{
    public unsafe class SwapCharacter : GameObject
    {
        private static readonly Dictionary<Character, IntPtr> _characterMainFunctions = new Dictionary<Character, IntPtr>()
        {
            { Character.Sonic,    (IntPtr)0x49A9B0 },
            { Character.Eggman,   (IntPtr)0x7B4EF0 },
            { Character.Tails,    (IntPtr)0x461700 },
            { Character.Knuckles, (IntPtr)0x47A770 },
            { Character.Tikal,    (IntPtr)0x7B40C0 },
            { Character.Amy,      (IntPtr)0x48ABF0 },
            { Character.Gamma,    (IntPtr)0x483430 },
            { Character.Big,      (IntPtr)0x490A00 }
        };

        public SwapCharacter(Character character, byte playerID, bool isMetalSonic = false, bool isSuper = false) : base(playerID)
        {
            SDK.Library.Definitions.Structures.GameObject.CharacterData* oldCharacterData = CharacterData;

            NextAction = PlayerAction.ReturnToNormal;

            bool* isMetalSonicFlag = (bool*)0x3B18DB5;
            *isMetalSonicFlag = isMetalSonic;

            Handle->mainSub = _characterMainFunctions[character];

            CharacterID = character;

            Info->Action = 0;
            Info->Status &= ~(Status.Attack | Status.Ball | Status.LightDash | Status.Unknown3);

            Info->CollisionInfo = null;

            Handle->MainSub(Handle);

            CharacterData->Powerups       = oldCharacterData->Powerups;
            CharacterData->JumpTime       = oldCharacterData->JumpTime;
            CharacterData->UnderwaterTime = oldCharacterData->UnderwaterTime;
            CharacterData->PathDistance   = oldCharacterData->PathDistance;
            CharacterData->Speed          = oldCharacterData->Speed;
            CharacterData->HeldObject     = oldCharacterData->HeldObject;
        }
    }
}
