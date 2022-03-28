using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TheBitCave.BattleRoyale.Utils
{
    public class AssetManager: PersistentSingleton<AssetManager>
    {
        class CharacterCatalogue
        {
            public Dictionary<string, List<GameObject>> headDictionary = new Dictionary<string, List<GameObject>>();
            public Dictionary<string, List<GameObject>> bodyDictionary = new Dictionary<string, List<GameObject>>();
            public Dictionary<string, GameObject> characterDictionary = new Dictionary<string, GameObject>();
        }

        public delegate void InitComplete();
        public event InitComplete SkinManagerComplete;

        private Dictionary<string, CharacterCatalogue> _characterCatalogue = new();
        
        
//        private Dictionary<string, List<GameObject>> _headDictionary;
//        private Dictionary<string, List<GameObject>> _bodyDictionary;
//        private Dictionary<string, GameObject> _characterDictionary;

        private void Start()
        {
            StartCoroutine(nameof(Init));
        }

        public IEnumerator Init()
        {
            var evil = C.GetCharacterAlignmentLabel(CharacterAlignment.Evil);
            var good = C.GetCharacterAlignmentLabel(CharacterAlignment.Good);
            _characterCatalogue.Add(evil, new CharacterCatalogue());
            _characterCatalogue.Add(good, new CharacterCatalogue());

            // Skin catalogue generation
//            _headDictionary = new Dictionary<string, List<GameObject>>();
//            _bodyDictionary = new Dictionary<string, List<GameObject>>();

// TODO: Add Good  catalogue generation
            var alignment = evil;
            _characterCatalogue.TryGetValue(alignment, out var catalogue);

            foreach (var type in C.characterTypes)
            {
                var skinLabels = new List<string>(){C.ADDRESSABLE_LABEL_BODY, type, alignment};
            
                var skinHandle = Addressables.LoadAssetsAsync<GameObject>(skinLabels, null, Addressables.MergeMode.Intersection, true);
                if (!skinHandle.IsDone) yield return skinHandle;
                var skinList = new List<GameObject>(skinHandle.Result);
                catalogue.bodyDictionary.Add(type, skinList);
            
                skinLabels = new List<string>(){C.ADDRESSABLE_LABEL_HEAD, type};
                skinHandle = Addressables.LoadAssetsAsync<GameObject>(skinLabels, null, Addressables.MergeMode.Intersection, true);
                if (!skinHandle.IsDone) yield return skinHandle;
                skinList = new List<GameObject>(skinHandle.Result);
                catalogue.headDictionary.Add(type, skinList);
            }
            
            // Character catalogue generation
//            _characterDictionary = new Dictionary<string, GameObject>();
            
            var characterLabels = new List<string>(){C.ADDRESSABLE_LABEL_CHARACTER, alignment};
            var characterHandle = Addressables.LoadAssetsAsync<GameObject>(characterLabels, null, Addressables.MergeMode.Intersection, true);
            if (!characterHandle.IsDone) yield return characterHandle;
            var characterList = new List<GameObject>(characterHandle.Result);
            foreach (var character in characterList)
            {
                var chComponent = character.GetComponent<Character>();
                if(chComponent == null) continue;
                catalogue.characterDictionary.Add(chComponent.TypeStringified, character);
            }

            Debug.Log("Assets Ready");
            
            OnInitComplete();
        }

        public GameObject GetCharacterPrefab(string type, string alignment)
        {
            _characterCatalogue.TryGetValue(alignment, out var catalogue);
            return catalogue != null && catalogue.characterDictionary.TryGetValue(type, out var go) ? go : null;
        }

        public List<GameObject> GetHeadList(string type, string alignment)
        {
            _characterCatalogue.TryGetValue(alignment, out var catalogue);
            return catalogue != null && catalogue.headDictionary.TryGetValue(type, out var list) ? list : null;
        }

        public List<GameObject> GetBodyList(string type, string alignment)
        {
            _characterCatalogue.TryGetValue(alignment, out var catalogue);
            return catalogue != null && catalogue.bodyDictionary.TryGetValue(type, out var list) ? list : null;
        }
        
        protected virtual void OnInitComplete()
        {
            SkinManagerComplete?.Invoke();
        }
    }
}
