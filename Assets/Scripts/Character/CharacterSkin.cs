using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TheBitCave.MultiplayerRoguelite
{
    public class CharacterSkin : NetworkBehaviour
    {
        [Header("Sockets")]
        [SerializeField] protected Transform headSlot;
        [SerializeField] protected Transform bodySlot;
        [SerializeField] protected Transform armLeftSlot;
        [SerializeField] protected Transform armRightSlot;

        [SyncVar(hook = nameof(OnBodyIndexChange))]
        protected int selectedBodyIndex = -1;

        [SyncVar(hook = nameof(OnHeadIndexChange))]
        protected int selectedHeadIndex = -1;

        protected string characterType = C.CHARACTER_NONE;

        public virtual void Generate(string type)
        {
            if (!isServer) return;
            characterType = type;
            var list = SkinManager.Instance.GetBodyList(characterType);
            selectedBodyIndex = Random.Range(0, list.Count);
            list = SkinManager.Instance.GetHeadList(characterType);
            selectedHeadIndex = Random.Range(0, list.Count);
        }
        
        protected virtual void OnBodyIndexChange(int _, int newValue)
        {
            var list = SkinManager.Instance.GetBodyList(characterType);
            var prefab = list[newValue];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            AddBodyPart(skin.Body, bodySlot);
            AddBodyPart(skin.ArmLeft, armLeftSlot);
            AddBodyPart(skin.ArmRight, armRightSlot);
        }

        protected virtual void OnHeadIndexChange(int _, int newValue)
        {
            var list = SkinManager.Instance.GetHeadList(characterType);
            var prefab = list[newValue];
            AddBodyPart(prefab, headSlot);
        }

        protected virtual void AddBodyPart(GameObject prefab, Transform parent)
        {
            parent.RemoveAllChildren();
            var go = Instantiate(prefab, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}
