using System;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Main;
using static Adventure.SDK.Library.Classes.Native.Player;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
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

        public SwapCharacter(Character character, Players playerID, bool isMetalSonic = false) : base(playerID)
        {
            // Store Original CharacterData
            SDK.Library.Definitions.Structures.GameObject.CharacterData* oldCharacterData = CharacterData;

            NextAction = PlayerAction.ReturnToNormal;

            // Set Metal Sonic Flag 
            bool* isMetalSonicFlag = (bool*)0x3B18DB5;
            *isMetalSonicFlag = isMetalSonic;

            // Change Main of Game Object
            Handle->mainSub = _characterMainFunctions[character];

            CharacterID = character;

            // Set Character Action to Initialize
            Info->Action = 0;

            // Cancel Special State of Character
            Info->Status &= ~(Status.Attack | Status.Ball | Status.LightDash | Status.Unknown3);

            // Free Player Collision
            Info->CollisionInfo = null;

            // Load New Character
            Handle->MainSub(Handle);

            // Copy CharacterData Stuff from Old Character
            CharacterData->Powerups       = oldCharacterData->Powerups;
            CharacterData->JumpTime       = oldCharacterData->JumpTime;
            CharacterData->UnderwaterTime = oldCharacterData->UnderwaterTime;
            CharacterData->PathDistance   = oldCharacterData->PathDistance;
            CharacterData->Speed          = oldCharacterData->Speed;
            CharacterData->HeldObject     = oldCharacterData->HeldObject;

            if (character != Character.Eggman && character != Character.Tikal)
                LoadSpecialPlayerAnimations(character);
        }
    }
}
