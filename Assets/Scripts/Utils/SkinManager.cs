using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    public class SkinManager: PersistentSingleton<SkinManager>
    {
        public delegate void InitComplete();
        public event InitComplete SkinManagerComplete;

        private Dictionary<CharacterType, List<GameObject>> _headDictionary;
        private Dictionary<CharacterType, List<GameObject>> _bodyDictionary;

        private void Start()
        {
            StartCoroutine(nameof(Init));
        }

        public IEnumerator Init()
        {
            _headDictionary = new Dictionary<CharacterType, List<GameObject>>();
            _bodyDictionary = new Dictionary<CharacterType, List<GameObject>>();

            var types = CharacterUtils.CharacterTypes;

            foreach (var type in types)
            {
                var characterLabel = CharacterUtils.GetCharacterLabel(type);
                var labels = new List<string>(){C.ADDRESSABLE_LABEL_BODY, characterLabel};
            
                var handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
                if (!handle.IsDone) yield return handle;
                var list = new List<GameObject>(handle.Result);
                _bodyDictionary.Add(type, list);
                Addressables.Release(handle);
            
                labels = new List<string>(){C.ADDRESSABLE_LABEL_HEAD, characterLabel};
                handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
                if (!handle.IsDone) yield return handle;
                list = new List<GameObject>(handle.Result);
                _headDictionary.Add(type, list);
                Addressables.Release(handle);
            }
            
            OnSkinManagerComplete();
        }

        public List<GameObject> GetHeadList(CharacterType type)
        {
            return _headDictionary.TryGetValue(type, out var list) ? list : null;
        }

        public List<GameObject> GetBodyList(CharacterType type)
        {
            return _bodyDictionary.TryGetValue(type, out var list) ? list : null;
        }
        
        protected virtual void OnSkinManagerComplete()
        {
            SkinManagerComplete?.Invoke();
        }
    }
    
}
