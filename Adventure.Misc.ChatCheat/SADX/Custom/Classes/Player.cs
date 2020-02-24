using System;
using System.Collections.Generic;
using Ninja.SDK.Library.Enums.Mesh;
using Ninja.SDK.Library.Structures.Mesh;
using Adventure.SDK.Library.API.Game;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects;
using Reloaded.Memory.Pointers;
using static Adventure.SDK.Library.Classes.Native.Player;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Classes
{
    public unsafe class Player : SDK.Library.API.Objects.Player.Player
    {
        private static readonly GameHandler _gameHandler = new GameHandler();
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
        private static readonly CustomEggmanDisplay _eggmanDisplay = new CustomEggmanDisplay();

        public Player() : base(Players.P1) { }

        public Player(Players playerID) : base(playerID) { }

        public void Swap(Character newCharacter, bool isMetalSonic = false)
        {
            Handle = GetCharacterGameObject(PlayerID);

            // Reset character size before swap
            Rescale(1f);

            // Store original CharacterData
            CharacterData* oldCharacterData = CharacterData;

            NextAction = PlayerAction.ReturnToNormal;

            // Set Metal Sonic flag
            _gameHandler.IsMetalSonic = isMetalSonic;

            // Change main function of the game object
            Handle->executor = _characterMainFunctions[newCharacter];

            // Delete old character
            if (newCharacter == Character.Gamma)
                Destructor((IntPtr)Handle);

            // Set new character ID
            CharacterID = newCharacter;

            // Set action to intialize
            Info->Action = (byte)PlayerObjectAction.Initialize;

            // Cancel special states of the character
            Info->Status &= ~(Status.Attack | Status.Ball | Status.LightDash | Status.Unknown3);

            // Free player collision
            Info->CollisionInfo = null;

            // Load new character
            Executor((IntPtr)Handle);

            if (CharacterID == Character.Eggman)
                Handle->displayer = _eggmanDisplay.DisplayFunction;

            // Copy CharacterData stuff from old character
            CharacterData->Powerups = oldCharacterData->Powerups;
            CharacterData->JumpTime = oldCharacterData->JumpTime;
            CharacterData->UnderwaterTime = oldCharacterData->UnderwaterTime;
            CharacterData->PathDistance = oldCharacterData->PathDistance;
            CharacterData->Speed = oldCharacterData->Speed;
            CharacterData->HeldObject = oldCharacterData->HeldObject;

            // Load special player animations
            if (newCharacter != Character.Eggman && newCharacter != Character.Tikal)
                LoadSpecialPlayerAnimations(newCharacter);
        }

        public void Rescale(float scaleMultiplier)
        {
            RefFixedArrayPtr<BlittablePointer<Model>> characterModelArray;

            switch (CharacterID)
            {
                case Character.Sonic:
                    characterModelArray = _gameHandler.SonicModel;

                    if (_gameHandler.IsMetalSonic)
                    {
                        if (scaleMultiplier == 1f)
                            scaleMultiplier = 1f / characterModelArray[68].AsReference().Scale.Y;
                        
                        // Metal Sonic Root
                        characterModelArray[68].AsReference().Scale *= scaleMultiplier;

                        // Metal Sonic Curled
                        characterModelArray[69].AsReference().Scale *= scaleMultiplier;
                    }
                    else
                    {
                        if (scaleMultiplier == 1f)
                            scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                        // Sonic Root
                        characterModelArray[0].AsReference().Scale *= scaleMultiplier;

                        // Sonic Curled
                        characterModelArray[66].AsReference().Scale *= scaleMultiplier;

                        // Sonic Jumpball
                        characterModelArray[67].AsReference().Scale *= scaleMultiplier;
                    }

                    // Light Speed Aura
                    characterModelArray[54].AsReference().Scale *= scaleMultiplier;

                    // Ball Effect Sphere
                    characterModelArray[56].AsReference().Flags &= ~EvalationFlags.Scale;
                    characterModelArray[56].AsReference().Scale *= scaleMultiplier;

                    // Trail Effect
                    characterModelArray[57].AsReference().Flags &= ~EvalationFlags.Scale;
                    characterModelArray[57].AsReference().Scale.Y *= scaleMultiplier;
                    characterModelArray[57].AsReference().Scale.Z *= scaleMultiplier;

                    // Snowboard, TODO: OFFSET IT PROPERLY
                    characterModelArray[71].AsReference().Position *= scaleMultiplier;
                    characterModelArray[71].AsReference().Scale *= scaleMultiplier;

                    // Spindash Charging Effect, TODO: OFFSET IT PROPERLY
                    characterModelArray[72].AsReference().Scale.Y *= scaleMultiplier;
                    characterModelArray[72].AsReference().Scale.Z *= scaleMultiplier;

                    // Light Speed Charging Particles, TODO: OFFSET IT PROPERLY
                    characterModelArray[73].AsReference().Scale.Y *= scaleMultiplier;
                    characterModelArray[73].AsReference().Scale.Z *= scaleMultiplier;
                    break;
                case Character.Eggman:
                    break;
                case Character.Tails:
                    characterModelArray = _gameHandler.TailsModel;

                    if (scaleMultiplier == 1f)
                        scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                    // Root
                    characterModelArray[0].AsReference().Scale *= scaleMultiplier;

                    // Spinning/Flying Root
                    characterModelArray[1].AsReference().Scale *= scaleMultiplier;

                    // Curled
                    characterModelArray[2].AsReference().Scale *= scaleMultiplier;

                    // Jumpball
                    characterModelArray[3].AsReference().Scale *= scaleMultiplier;

                    // TODO: SCALE PARTICLES
                    break;
                case Character.Knuckles:
                    characterModelArray = _gameHandler.KnucklesModel;

                    if (scaleMultiplier == 1f)
                        scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                    // Root
                    characterModelArray[0].AsReference().Scale *= scaleMultiplier;

                    // Gliding
                    characterModelArray[1].AsReference().Scale *= scaleMultiplier;

                    // TODO: SCALE JUMPBALL AND PARTICLES
                    break;
                case Character.Tikal:
                    break;
                case Character.Amy:
                    characterModelArray = _gameHandler.AmyModel;

                    if (scaleMultiplier == 1f)
                        scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                    // Root
                    characterModelArray[0].AsReference().Scale *= scaleMultiplier;

                    // TODO: SCALE WARRIOR FEATHER AND PARTICLES
                    break;
                case Character.Gamma:
                    characterModelArray = _gameHandler.GammaModel;

                    if (scaleMultiplier == 1f)
                        scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                    // Root
                    characterModelArray[0].AsReference().Scale *= scaleMultiplier;

                    // Right Arm Shooting
                    characterModelArray[1].AsReference().Scale *= scaleMultiplier;

                    // Jet Booster
                    characterModelArray[2].AsReference().Flags &= ~EvalationFlags.Scale;
                    characterModelArray[2].AsReference().Scale *= scaleMultiplier;

                    /*_memory.SafeRead((IntPtr)0x7DCCFC, out float verticalOffset);
                    _memory.SafeWrite((IntPtr)0x7DCCFC, verticalOffset * 2f);

                    // ASM Hooks
                    hookString = new string[]
                    {
                        "use32",
                        $"fmul dword [{new IntPtr(_scaleMultiplier.Pointer)}]"
                    };
                    modelOffsetHook = new AsmHook(hookString, 0x4C2F01, AsmHookBehaviour.ExecuteAfter).Activate();

                    hookString = new string[]
                    {
                        "use32",
                        $"push 0x{(5f * _scaleMultiplier.Value).ToHex()}",
                        Utilities.GetAbsoluteCallMnemonics((IntPtr)0x784BE0, false)
                    };
                    modelOffsetHook = new AsmHook(hookString, 0x480208, AsmHookBehaviour.DoNotExecuteOriginal).Activate();*/
                    break;
                case Character.Big:
                    characterModelArray = _gameHandler.BigModel;

                    if (scaleMultiplier == 1f)
                        scaleMultiplier = 1f / characterModelArray[0].AsReference().Scale.Y;

                    // Root
                    characterModelArray[0].AsReference().Scale *= scaleMultiplier;
                    break;
            }

            CharacterData->PhysicsData.CollisionSize *= scaleMultiplier;
            CharacterData->PhysicsData.YOffset *= scaleMultiplier;
        }
    }
}
