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
        
        private void Update()
        {
            if (!isServer) return;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            var type = GetComponent<ICharacterTypeable>().Type;
            StartCoroutine(GenerateSkin(type));
        }
        
        private IEnumerator GenerateSkin(string type)
        {
            yield return new WaitForSeconds(.5f);
            var list = SkinManager.Instance.GetBodyList(type);
            var selectedBodyIndex = Random.Range(0, list.Count);
            RpcSkinBody(type, selectedBodyIndex);
            list = SkinManager.Instance.GetHeadList(type);
            var selectedHeadIndex = Random.Range(0, list.Count);
            RpcSkinHead(type, selectedHeadIndex);
        }
        
        [ClientRpc]
        private void RpcSkinBody(string type, int index)
        {
            var list = SkinManager.Instance.GetBodyList(type);
            var prefab = list[index];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            AddBodyPart(skin.Body, bodySlot);
            AddBodyPart(skin.ArmLeft, armLeftSlot);
            AddBodyPart(skin.ArmRight, armRightSlot);
        }

        [ClientRpc]
        private void RpcSkinHead(string type, int index)
        {
            var list = SkinManager.Instance.GetHeadList(type);
            var prefab = list[index];
            AddBodyPart(prefab, headSlot);
        }

        private void AddBodyPart(GameObject prefab, Transform parent)
        {
            parent.RemoveAllChildren();
            var go = Instantiate(prefab, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}
