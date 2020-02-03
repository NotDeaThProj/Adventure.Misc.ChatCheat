using System;
using Adventure.SDK.Library.API.Audio;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Player;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using Reloaded.Memory.Interop;
using Reloaded.Memory.Sources;
using static Adventure.SDK.Library.Classes.Native.PVM;
using Adventure.SDK.Library.API.Objects.Player.Physics;
using Adventure.SDK.Library.API.Objects.Common;

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
                    UnsuperPlayer();
            }
        }

        // Variables/Constants
        private readonly AudioManager _audioManager = new AudioManager();
        private bool _isPlayerSuper;
        private Stage _lastStage;
        private Music _levelSong;
        private long _timer = 0;
        private static Pinnable<GameObject> _superStateManager =
            new Pinnable<GameObject>(new GameObject());

        public SuperStateManager(Players playerID) : base(playerID)
        {
            // Initialize Super Sonic Weld Data
            Memory.Instance.SafeWrite((IntPtr)0x49AC6A, (ushort)0x9090);

            LoadPVMFile("SUPERSONIC", (IntPtr)0x142272C);
            _superStateManager.Value = *CreateGameObject(0, 2);
        }

        public override void Main()
        {
            if (_isPlayerSuper && (_audioManager.Song != Music.NoMusic && _lastStage != GetStage()))
                PlaySuperTheme();

            if (IsControllerEnabled && PlayerID == 0 && _isPlayerSuper)
            {
                if (Rings == 0)
                    Delete();

                ++_timer;
                if (_timer % 60 == 0)
                    Rings--;
            }
        }

        public override void Delete()
        {
            UnsuperPlayer();
            _superStateManager.Value.mainSub = _superStateManager.Value.displaySub = _superStateManager.Value.deleteSub = IntPtr.Zero;
        }

        private void TurnPlayerSuper()
        {
            Info->Status &= ~Status.LightDash;
            CharacterData->Upgrades |= Upgrade.SuperSonic;
            new SuperAura();
            new SuperPhysics();

            switch (CharacterID)
            {
                // TODO: DO VOICES
                case Character.Sonic:
                    NextAction = PlayerAction.SuperSonic;
                    _audioManager.Voice = 396;
                    break;
                default:
                    break;
            }

            _isPlayerSuper = true;
        }

        private void UnsuperPlayer()
        {
            _timer = 0;

            if (CharacterID == Character.Sonic)
            {
                NextAction = PlayerAction.UnsuperSonic;
            }
            CharacterData->Upgrades &= ~Upgrade.SuperSonic;

            if (PlayerID == 0 && _levelSong != 0)
            {
                RestoreStageSong();
            }

            _isPlayerSuper = false;
        }

        private void RestoreStageSong()
        {
            _audioManager.Song = _levelSong;
        }

        private void PlaySuperTheme()
        {
            _levelSong = _audioManager.Song;

            _lastStage = GetStage();

            _audioManager.Song = Music.SuperSonic;
        }

        private Stage GetStage()
        {
            byte* currentAct = (byte*)0x3B22DEC;
            Stage* currentLevel = (Stage*)0x3B22DCC;
            switch (*currentLevel)
            {
                case Stage.HedgehogHammer:
                case Stage.EmeraldCoast:
                case Stage.WindyValley:
                case Stage.TwinklePark:
                case Stage.SpeedHighway:
                case Stage.RedMountain:
                case Stage.SkyDeck:
                case Stage.LostWorld:
                case Stage.IceCap:
                case Stage.Casinopolis:
                case Stage.FinalEgg:
                case Stage.HotShelter:
                case Stage.StationSquare:
                case Stage.EggCarrierOutside:
                case Stage.EggCarrierInside:
                case Stage.MysticRuins:
                case Stage.Past:
                case Stage.TwinkleCircuit:
                    return (Stage)(((byte)*currentLevel << 8) + *currentAct);
                default:
                    return (Stage)((byte)*currentLevel + *currentAct);
            }
        }
    }
}
