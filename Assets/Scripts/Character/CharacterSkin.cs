using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
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

        protected CharacterType characterType;
        
        public virtual void Init(CharacterType type)
        {
            this.characterType = type;
            // TODO: Remove from Sync operation
            // TODO: Add skin randomization
            var characterLabel = CharacterUtils.GetCharacterLabel(type);
       //     var labels = new string[] {C.ADDRESSABLE_LABEL_BODY, characterLabel};
            var op = Addressables.LoadAssetAsync<GameObject>(C.ADDRESSABLE_LABEL_BODY);
            var prefab = op.WaitForCompletion();
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            if (skin != null)
            {
                AddBodyPart(skin.Body, bodySlot);
                AddBodyPart(skin.ArmLeft, armLeftSlot);
                AddBodyPart(skin.ArmRight, armRightSlot);
            }

       //     labels = new string[] {C.ADDRESSABLE_LABEL_HEAD, characterLabel};
            op = Addressables.LoadAssetAsync<GameObject>(C.ADDRESSABLE_LABEL_HEAD);
            prefab = op.WaitForCompletion();
            AddBodyPart(prefab, headSlot);
            Addressables.Release(op);
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
