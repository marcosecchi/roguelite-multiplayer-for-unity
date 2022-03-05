using System.Collections;
using System.Collections.Generic;
using Mirror;
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

        public override void OnStartServer()
        {
            base.OnStartServer();

            // TODO: Remove from Sync operation
            // TODO: Add skin randomization
            
            var op = Addressables.LoadAssetAsync<GameObject>("body");
            var prefab = op.WaitForCompletion();
            AddBodyPart(prefab, bodySlot);

            op = Addressables.LoadAssetAsync<GameObject>("armLeft");
            prefab = op.WaitForCompletion();
            AddBodyPart(prefab, armLeftSlot);
            
            op = Addressables.LoadAssetAsync<GameObject>("armRight");
            prefab = op.WaitForCompletion();
            AddBodyPart(prefab, armRightSlot);
            
            op = Addressables.LoadAssetAsync<GameObject>("head");
            prefab = op.WaitForCompletion();
            AddBodyPart(prefab, headSlot);
            
            Addressables.Release(op);
        }

        protected virtual void AddBodyPart(GameObject prefab, Transform parent)
        {
            var go = Instantiate(prefab, parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        }
    }
}
