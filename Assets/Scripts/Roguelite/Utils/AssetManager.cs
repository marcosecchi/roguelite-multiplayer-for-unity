using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TheBitCave.BattleRoyale.Utils
{
    public class AssetManager: PersistentSingleton<AssetManager>
    {
        public delegate void InitComplete();
        public event InitComplete SkinManagerComplete;

        private Dictionary<string, List<GameObject>> _headDictionary;
        private Dictionary<string, List<GameObject>> _bodyDictionary;
        private Dictionary<string, GameObject> _characterDictionary;

        private void Start()
        {
            StartCoroutine(nameof(Init));
        }

        public IEnumerator Init()
        {
            // Skin catalogue generation
            _headDictionary = new Dictionary<string, List<GameObject>>();
            _bodyDictionary = new Dictionary<string, List<GameObject>>();
            
            foreach (var type in C.characterTypes)
            {
                var skinLabels = new List<string>(){C.ADDRESSABLE_LABEL_BODY, type};
            
                var skinHandle = Addressables.LoadAssetsAsync<GameObject>(skinLabels, null, Addressables.MergeMode.Intersection, true);
                if (!skinHandle.IsDone) yield return skinHandle;
                var skinList = new List<GameObject>(skinHandle.Result);
                _bodyDictionary.Add(type, skinList);
            
                skinLabels = new List<string>(){C.ADDRESSABLE_LABEL_HEAD, type};
                skinHandle = Addressables.LoadAssetsAsync<GameObject>(skinLabels, null, Addressables.MergeMode.Intersection, true);
                if (!skinHandle.IsDone) yield return skinHandle;
                skinList = new List<GameObject>(skinHandle.Result);
                _headDictionary.Add(type, skinList);
            }
            
            // Character catalogue generation
            _characterDictionary = new Dictionary<string, GameObject>();
            
            var characterLabels = new List<string>(){C.ADDRESSABLE_LABEL_CHARACTER};
            var characterHandle = Addressables.LoadAssetsAsync<GameObject>(characterLabels, null, Addressables.MergeMode.Intersection, true);
            if (!characterHandle.IsDone) yield return characterHandle;
            var characterList = new List<GameObject>(characterHandle.Result);
            foreach (var character in characterList)
            {
                var chComponent = character.GetComponent<Character>();
                if(chComponent == null) continue;
                _characterDictionary.Add(chComponent.TypeStringified, character);
            }

            Debug.Log("Assets Ready");
            
            OnInitComplete();
        }

        public GameObject GetCharacterPrefab(string type)
        {
            return _characterDictionary.TryGetValue(type, out var go) ? go : null;
        }

        public List<GameObject> GetHeadList(string type)
        {
            return _headDictionary.TryGetValue(type, out var list) ? list : null;
        }

        public List<GameObject> GetBodyList(string type)
        {
            return _bodyDictionary.TryGetValue(type, out var list) ? list : null;
        }
        
        protected virtual void OnInitComplete()
        {
            SkinManagerComplete?.Invoke();
        }
    }
    
}
