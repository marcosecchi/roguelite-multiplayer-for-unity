using Mirror;
using TheBitCave.BattleRoyale.Utils;
using TheBitCave.BattleRoyale;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Prototype
{
    public class ProtoNetworkManager : NetworkManager
    {

        public enum SkinSpawnMethod
        {
            Random,
            EditorSelection
        }
        
        public SkinSpawnMethod skinSpawnMethod = SkinSpawnMethod.Random;
        public CharacterType characterType = CharacterType.Thief;
        public CharacterAlignment characterAlignment = CharacterAlignment.Evil;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
        
            NetworkServer.RegisterHandler<ProtoMessage>(OnCharacterCreationReady);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            foreach (var alignment in C.alignmentTypes)
            {
                foreach (var type in C.characterTypes)
                {
//                Debug.Log("Registering character: " + type);
                    var go = AssetManager.Instance.GetCharacterPrefab(type, alignment);
                    if(go == null) continue;
                    NetworkClient.RegisterPrefab(go);
                }
            }

            var cType = C.GetCharacterTypeLabel(characterType);
            if (skinSpawnMethod == SkinSpawnMethod.Random)
            {
                cType = C.GetRandomCharacterLabel();
            }
            var message = new ProtoMessage
            {
                characterType = cType,
                characterAlignment = C.GetCharacterAlignmentLabel(characterAlignment)
            };
            NetworkClient.Send(message);
        }

        private void OnCharacterCreationReady(NetworkConnection conn, ProtoMessage message)
        {
            var prefab = AssetManager.Instance.GetCharacterPrefab(message.characterType, message.characterAlignment);
            var go = Instantiate(prefab);
            go.transform.position = GetStartPosition().position;
            NetworkServer.AddPlayerForConnection(conn, go);
        }
    }

    public struct ProtoMessage : NetworkMessage
    {
        public string characterType;
        public string characterAlignment;
    }

}

