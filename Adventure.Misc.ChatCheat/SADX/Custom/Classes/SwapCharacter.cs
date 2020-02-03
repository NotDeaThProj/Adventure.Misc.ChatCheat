using System;
using System.Collections.Generic;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Player;
using static Adventure.SDK.Library.Classes.Native.Player;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
{
    public unsafe class SwapCharacter : Player
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
            // Store original CharacterData
            SDK.Library.Definitions.Structures.GameObject.CharacterData* oldCharacterData = CharacterData;

            NextAction = PlayerAction.ReturnToNormal;

            // Set metal sonic flag
            bool* isMetalSonicFlag = (bool*)0x3B18DB5;
            *isMetalSonicFlag = isMetalSonic;

            // Change main function of the game object
            Handle->mainSub = _characterMainFunctions[character];

            // Delete old character
            if (character == Character.Gamma)
                Handle->DeleteSub(Handle);

            // Set new character ID
            CharacterID = character;

            // Set action to intialize
            Info->Action = (byte)PlayerObjectAction.Initialize;

            // Cancel special states of the character
            Info->Status &= ~(Status.Attack | Status.Ball | Status.LightDash | Status.Unknown3);

            // Free player collision
            Info->CollisionInfo = null;

            // Load new character
            Handle->MainSub(Handle);

            // Copy CharacterData stuff from old character
            CharacterData->Powerups       = oldCharacterData->Powerups;
            CharacterData->JumpTime       = oldCharacterData->JumpTime;
            CharacterData->UnderwaterTime = oldCharacterData->UnderwaterTime;
            CharacterData->PathDistance   = oldCharacterData->PathDistance;
            CharacterData->Speed          = oldCharacterData->Speed;
            CharacterData->HeldObject     = oldCharacterData->HeldObject;

            // Load special player animations
            if (character != Character.Eggman && character != Character.Tikal)
                LoadSpecialPlayerAnimations(character);
        }
    }
}
