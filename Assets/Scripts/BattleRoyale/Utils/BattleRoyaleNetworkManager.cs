using System.Linq;
using Mirror;
using TheBitCave.BattleRoyale.Utils;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public class BattleRoyaleNetworkManager : NetworkManager
    {

        public enum SkinSpawnMethod
        {
            Random,
            EditorSelection
        }
        
        public SkinSpawnMethod skinSpawnMethod = SkinSpawnMethod.Random;
        public CharacterType characterType = CharacterType.Thief;
        public bool keepAlignmentBalanced = true;
        
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

            var cAlignment = C.GetRandomAlignment();
            if (keepAlignmentBalanced && NetworkServer.connections.Count > 0)
            {
                cAlignment = HasGoodMorePlayers() ? C.ALIGNMENT_EVIL : C.ALIGNMENT_GOOD;
            }
            var message = new ProtoMessage
            {
                CharacterType = cType,
                CharacterAlignment = cAlignment
            };
            NetworkClient.Send(message);
        }

        private void OnCharacterCreationReady(NetworkConnection conn, ProtoMessage message)
        {
            var prefab = AssetManager.Instance.GetCharacterPrefab(message.CharacterType, message.CharacterAlignment);
            var go = Instantiate(prefab);
            go.transform.position = GetStartPosition().position;
            NetworkServer.AddPlayerForConnection(conn, go);
        }

        private static bool HasGoodMorePlayers()
        {
            var list = FindObjectsOfType<Character>();
            var count = list.Count(ch => ch.Alignment == CharacterAlignment.Good);
            return count > list.Length / 2;
        }
    }
    
    public struct ProtoMessage : NetworkMessage
    {
        public string CharacterType;
        public string CharacterAlignment;
    }

}

