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

        private Dictionary<string, List<GameObject>> _headDictionary;
        private Dictionary<string, List<GameObject>> _bodyDictionary;

        private void Start()
        {
            StartCoroutine(nameof(Init));
        }

        public IEnumerator Init()
        {
            _headDictionary = new Dictionary<string, List<GameObject>>();
            _bodyDictionary = new Dictionary<string, List<GameObject>>();

            foreach (var type in C.characterTypes)
            {
                var labels = new List<string>(){C.ADDRESSABLE_LABEL_BODY, type};
            
                var handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
                if (!handle.IsDone) yield return handle;
                var list = new List<GameObject>(handle.Result);
                _bodyDictionary.Add(type, list);
            
                labels = new List<string>(){C.ADDRESSABLE_LABEL_HEAD, type};
                handle = Addressables.LoadAssetsAsync<GameObject>(labels, null, Addressables.MergeMode.Intersection, true);
                if (!handle.IsDone) yield return handle;
                list = new List<GameObject>(handle.Result);
                _headDictionary.Add(type, list);
            }
            OnSkinManagerComplete();
        }

        public List<GameObject> GetHeadList(string type)
        {
            return _headDictionary.TryGetValue(type, out var list) ? list : null;
        }

        public List<GameObject> GetBodyList(string type)
        {
            return _bodyDictionary.TryGetValue(type, out var list) ? list : null;
        }
        
        protected virtual void OnSkinManagerComplete()
        {
            SkinManagerComplete?.Invoke();
        }
    }
    
}
