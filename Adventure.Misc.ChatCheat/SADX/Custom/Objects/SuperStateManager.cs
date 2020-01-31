using System;
using Adventure.SDK.Library.API.Audio;
using Adventure.SDK.Library.API.Objects.Main;
using Adventure.SDK.Library.Definitions.Enums;
using static Adventure.SDK.Library.Classes.Native.PVM;

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
        private AudioManager _audioManager = new AudioManager();
        private bool _isPlayerSuper;
        private Players _playerID;
        private Stage* _lastLevel;
        private byte* _lastAct;
        private Music _levelSong;
        private long _timer = 0;
        private SDK.Library.Definitions.Structures.GameObject.GameObject* _superStateManager;

        public SuperStateManager(Players playerID) : base(playerID)
        {
            LoadPVMFile("SUPERSONIC", (IntPtr)0x142272C);
            _superStateManager = CreateGameObject(0, 2);
            _playerID = playerID;
        }

        public override void Main()
        {
            if (_isPlayerSuper)
            {
                byte* currentAct = (byte*)0x3B22DEC;
                Stage* currentLevel = (Stage*)0x3B22DCC;
                if (_audioManager.Song != Music.NoMusic && *currentLevel != *_lastLevel && *currentAct != *_lastAct)
                    PlaySuperTheme();
            }
        }

        public override void Delete()
        {
            UnsuperPlayer();
            _superStateManager->mainSub = _superStateManager->displaySub = _superStateManager->deleteSub = IntPtr.Zero;
        }

        private void TurnPlayerSuper()
        {
            Info->Status &= ~Status.LightDash;
            CharacterData->Upgrades |= Upgrade.SuperSonic;

            /*if (CharacterID != Character.Sonic)
			{
				GameObject superAura = new GameObject(2, 2, (IntPtr)0x55FAF0);
				SuperPhysics superPhysics = new SuperPhysics(PlayerID);
			}*/
            GameObject superAura = new GameObject(2, 2, (IntPtr)0x55FAF0);
            //SuperPhysics superPhysics = new SuperPhysics(PlayerID);

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

            if (_playerID == 0 && _levelSong != 0)
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

            _lastAct = (byte*)0x3B22DEC;
            _lastLevel = (Stage*)0x3B22DCC;

            _audioManager.Song = Music.SuperSonic;
        }
    }
}
