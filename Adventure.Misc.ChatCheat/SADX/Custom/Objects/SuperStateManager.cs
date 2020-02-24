using System;
using Adventure.SDK.Library.API.Audio;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Common;
using Adventure.SDK.Library.API.Objects.Player;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Reloaded.Memory.Interop;
using Reloaded.Memory.Sources;
using static Adventure.SDK.Library.Classes.Native.PVM;
using static Adventure.SDK.Library.Classes.Native.GameObject;
using Reloaded.Hooks.X86;
using Adventure.SDK.Library.API.Game;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects
{
    public unsafe class SuperStateManager : Player
    {
        public bool IsPlayerSuper
        {
            get => _isPlayerSuper;
            set
            {
                if (value)
                    TurnPlayerSuper();
                else
                    Destructor((IntPtr)Handle);
            }
        }

        // Variables/Constants
        private static ReverseWrapper<FunctionPointer> _executorFunction;
        private static ReverseWrapper<FunctionPointer> _destructorFunction;
        private static GameHandler _gameHandler = new GameHandler();

        private readonly AudioManager _audioManager = new AudioManager();
        private bool _isPlayerSuper;
        private Stage _lastStage;
        private Music _levelSong;
        private long _timer = 0;
        private static readonly Pinnable<GameObject> _superStateManager = new Pinnable<GameObject>(new GameObject());
        private static readonly CustomSuperPhysics _superPhysics = new CustomSuperPhysics(Players.P1);
        private const ushort OriginalSuperWeldDataMethods = 0x7521;

        public SuperStateManager() : base(Players.P1) { Initializer(); }
        public SuperStateManager(Players playerID) : base(playerID) { Initializer(); }

        public void Initializer()
        {
            _executorFunction = new ReverseWrapper<FunctionPointer>((obj) => Executor((IntPtr)Handle));
            _destructorFunction = new ReverseWrapper<FunctionPointer>((obj) => Destructor((IntPtr)Handle));
            LoadPVMFile("SUPERSONIC", (IntPtr)0x142272C);
            _superStateManager.Value = *LoadNativeGameObject(0, 2, _executorFunction.WrapperPointer);
        }

        public override void Executor(IntPtr handle)
        {
            // Play Super Sonic theme
            if (_isPlayerSuper && (_audioManager.Song != Music.NoMusic && _lastStage != _gameHandler.CurrentStage))
                PlaySuperTheme();

            if (IsControllerEnabled && PlayerID == Players.P1 && _isPlayerSuper)
            {
                // Delete manager if rings reach 0
                if (Rings == 0)
                    Destructor((IntPtr)Handle);

                // Remove rings from counter
                ++_timer;
                if (_timer % 60 == 0)
                    Rings = -1;
            }
        }

        public override void Destructor(IntPtr handle)
        {
            if (CharacterID == Character.Sonic)
            {
                // Unsuper player
                NextAction = PlayerAction.UnsuperSonic;

                // Restore super weld data methods to original
                Memory.Instance.SafeWrite((IntPtr)0x49AC6A, OriginalSuperWeldDataMethods);
            }

            // Remove Super Sonic upgrade
            Handle->ActorData->CharacterData->Upgrades &= ~Upgrade.SuperSonic;

            // Restore stage song
            _audioManager.Song = _levelSong;

            // Turn back physics to normal
            _superPhysics.Destructor((IntPtr)Handle);

            // Delete Game Object
            DeleteNativeGameObject(_superStateManager.Pointer);

            _isPlayerSuper = false;
        }

        private void TurnPlayerSuper()
        {
            if (CharacterID == Character.Sonic)
            {
                // NOP out if statements so super weld data initializes
                Memory.Instance.SafeWrite((IntPtr)0x49AC6A, (ushort)0x9090);

                // Remove light dashing state
                Handle->Info->Status &= ~Status.LightDash;

                NextAction = PlayerAction.SuperSonic;
                _audioManager.Voice = 396;
            }

            // Add Super Sonic upgrade
            Handle->ActorData->CharacterData->Upgrades |= Upgrade.SuperSonic;

            // Add super aura if player is not Sonic
            if (CharacterID != Character.Sonic)
                new SuperAura();

            _isPlayerSuper = true;
        }

        private void PlaySuperTheme()
        {
            // Store level song
            _levelSong = _audioManager.Song;

            // Store current stage
            _lastStage = _gameHandler.CurrentStage;

            // Play Super Sonic theme
            _audioManager.Song = Music.SuperSonic;
        }
    }
}
