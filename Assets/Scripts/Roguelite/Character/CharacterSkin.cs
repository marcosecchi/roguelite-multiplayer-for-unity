using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    [AddComponentMenu(menuName: "Roguelite/Character Skin")]
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

        [SyncVar]
        private int _selectedBodyIndex;

        [SyncVar]
        private int _selectedHeadIndex;

        public override void OnStartServer()
        {
            base.OnStartServer();
            _type = GetComponent<ICharacterTypeable>().TypeStringified;
            var list = AssetManager.Instance.GetBodyList(_type);
            _selectedBodyIndex = Random.Range(0, list.Count);
            list = AssetManager.Instance.GetHeadList(_type);
            _selectedHeadIndex = Random.Range(0, list.Count);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            // Body generation
            var list = AssetManager.Instance.GetBodyList(_type);
            var prefab = list[_selectedBodyIndex];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            AddBodyPart(skin.Body, bodySlot);
            AddBodyPart(skin.ArmLeft, armLeftSlot);
            AddBodyPart(skin.ArmRight, armRightSlot);

            // Head generation
            list = AssetManager.Instance.GetHeadList(_type);
            prefab = list[_selectedHeadIndex];
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