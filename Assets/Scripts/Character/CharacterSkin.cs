using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(ICharacterTypeable))]
    public class CharacterSkin : NetworkBehaviour
    {
        [Header("Sockets")]
        [SerializeField] protected Transform headSlot;
        [SerializeField] protected Transform bodySlot;
        [SerializeField] protected Transform armLeftSlot;
        [SerializeField] protected Transform armRightSlot;

        [SyncVar]
        private string _type;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            _type = GetComponent<ICharacterTypeable>().Type;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            var list = SkinManager.Instance.GetBodyList(_type);
            var selectedBodyIndex = Random.Range(0, list.Count);
            GenerateBody(_type, selectedBodyIndex);
            list = SkinManager.Instance.GetHeadList(_type);
            var selectedHeadIndex = Random.Range(0, list.Count);
            GenerateHead(_type, selectedHeadIndex);
        }
        
        private void GenerateBody(string type, int index)
        {
            if (!isClient) return;
            var list = SkinManager.Instance.GetBodyList(type);
            var prefab = list[index];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            AddBodyPart(skin.Body, bodySlot);
            AddBodyPart(skin.ArmLeft, armLeftSlot);
            AddBodyPart(skin.ArmRight, armRightSlot);
        }

        private void GenerateHead(string type, int index)
        {
            if (!isClient) return;
            var list = SkinManager.Instance.GetHeadList(type);
            var prefab = list[index];
            AddBodyPart(prefab, headSlot);
        }

        private static void AddBodyPart(GameObject prefab, Transform parent)
        {
            parent.RemoveAllChildren();
            var go = Instantiate(prefab, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}
