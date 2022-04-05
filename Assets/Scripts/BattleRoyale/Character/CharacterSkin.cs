using Mirror;
using TheBitCave.BattleRoyale.Interfaces;
using TheBitCave.BattleRoyale.Utils;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    [AddComponentMenu(menuName: "BattleRoyale/Character Skin")]
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
        private string _alignment;

        [SyncVar]
        private int _selectedBodyIndex;

        [SyncVar]
        private int _selectedHeadIndex;

        public override void OnStartServer()
        {
            base.OnStartServer();
            _type = GetComponent<ICharacterTypeable>().TypeStringified;
            _alignment = GetComponent<ICharacterTypeable>().AlignmentStringified;
            var list = AssetManager.Instance.GetBodyList(_type, _alignment);
            _selectedBodyIndex = Random.Range(0, list.Count);
            var prefab = list[_selectedBodyIndex];
            Debug.Log(prefab);
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            Debug.Log(skin);
            _selectedHeadIndex = Random.Range(0, skin.Heads.Length);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            // Body generation
            var list = AssetManager.Instance.GetBodyList(_type, _alignment);
            var prefab = list[_selectedBodyIndex];
            var skin = prefab.GetComponent<CharacterSkinBodyElements>();
            AddBodyPart(skin.Body, bodySlot);
            AddBodyPart(skin.ArmLeft, armLeftSlot);
            AddBodyPart(skin.ArmRight, armRightSlot);

            // Head generation
            prefab = skin.Heads[_selectedHeadIndex];
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
