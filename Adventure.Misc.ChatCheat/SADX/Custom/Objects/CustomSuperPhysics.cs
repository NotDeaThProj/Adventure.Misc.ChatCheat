using System;
using Adventure.SDK.Library.Definitions.Enums;
using Adventure.SDK.Library.API.Objects.Player.Physics;
using Adventure.SDK.Library.Definitions.Structures.GameObject;
using static Adventure.SDK.Library.Classes.Native.Player;

namespace Adventure.Misc.ChatCheat.ReloadedII.SADX.Custom.Objects
{
    public unsafe class CustomSuperPhysics : SuperPhysics
    {
        private readonly Players _playerID;

        public CustomSuperPhysics(Players playerID) : base() { _playerID = playerID; }

        public override void Delete()
        {
            ReadOnlySpan<PhysicsData> characterPhysicsArray = new ReadOnlySpan<PhysicsData>((void*)0x9154E8, 8);
            GameObject* characterObject = GetCharacterGameObject(_playerID);
            characterObject->ActorData->CharacterData->PhysicsData = characterPhysicsArray[(int)characterObject->Info->CharacterID];
        }
    }
}
