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

        protected CharacterType characterType;

        public virtual void Init(CharacterType type)
        {
            this.characterType = type;
            StartCoroutine(nameof(CreateCharacter));
        }

        private IEnumerator CreateCharacter()
        {
            // TODO: Remove from Sync operation
            // TODO: Add skin randomization
            var characterLabel = CharacterUtils.GetCharacterLabel(characterType);
            var labels = new List<string>(){C.ADDRESSABLE_LABEL_BODY, characterLabel};
            
            var handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
            if (!handle.IsDone) yield return handle;
            var list = new List<GameObject>(handle.Result);
            var prefab = list[0];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            if (skin != null)
            {
                AddBodyPart(skin.Body, bodySlot);
                AddBodyPart(skin.ArmLeft, armLeftSlot);
                AddBodyPart(skin.ArmRight, armRightSlot);
            }
            Addressables.Release(handle);
            
            labels = new List<string>(){C.ADDRESSABLE_LABEL_HEAD, characterLabel};
            handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
            if (!handle.IsDone) yield return handle;
            list = new List<GameObject>(handle.Result);
            prefab = list[0];
            AddBodyPart(prefab, headSlot);
            Addressables.Release(handle);
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
