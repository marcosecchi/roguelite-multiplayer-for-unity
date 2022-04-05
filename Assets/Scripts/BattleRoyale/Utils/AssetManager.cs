using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TheBitCave.BattleRoyale.Utils
{
    public class AssetManager: PersistentSingleton<AssetManager>
    {
        /// <summary>
        /// A class used to store character data and skin prefabs
        /// </summary>
        class CharacterCatalogue
        {
            public readonly Dictionary<string, List<GameObject>> skinDictionary = new();
            public readonly Dictionary<string, GameObject> characterDictionary = new();
        }

        public delegate void InitComplete();
        public event InitComplete SkinManagerComplete;

        private readonly Dictionary<string, CharacterCatalogue> _characterCatalogue = new();
        
        private void Start()
        {
            StartCoroutine(nameof(Init));
        }

        /// <summary>
        /// Creates a skin and character catalogue from the Addressables system
        /// </summary>
        private IEnumerator Init()
        {
            // Skin catalogue generation
            foreach (var alignment in C.alignmentTypes)
            {
                var catalogue = new CharacterCatalogue();
                _characterCatalogue.Add(alignment, catalogue);

                foreach (var type in C.characterTypes)
                {
                    var skinLabels = new List<string>(){C.ADDRESSABLE_LABEL_SKIN, type, alignment};
            
                    var skinHandle = Addressables.LoadAssetsAsync<GameObject>(skinLabels, null, Addressables.MergeMode.Intersection, true);
                    if (!skinHandle.IsDone) yield return skinHandle;
                    var skinList = new List<GameObject>(skinHandle.Result);
                    catalogue.skinDictionary.Add(type, skinList);
                }
            
                // Character catalogue generation
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
            }

            Debug.Log("Assets Ready");
            
            OnInitComplete();
        }

        /// <summary>
        /// Retrieves the character class prefab from the catalogue
        /// </summary>
        /// <param name="type">The character class/type</param>
        /// <param name="alignment">The character alignment (Good/Evil)</param>
        /// <returns>The character prefab to be instantiated in game</returns>
        public GameObject GetCharacterPrefab(string type, string alignment)
        {
            _characterCatalogue.TryGetValue(alignment, out var catalogue);
            return catalogue != null && catalogue.characterDictionary.TryGetValue(type, out var go) ? go : null;
        }
        
        /// <summary>
        /// Retrieves the available body skins for the character
        /// </summary>
        /// <param name="type">The character class/type</param>
        /// <param name="alignment">The character alignment (Good/Evil)</param>
        /// <returns>A list of available bodies</returns>
        public List<GameObject> GetBodyList(string type, string alignment)
        {
            _characterCatalogue.TryGetValue(alignment, out var catalogue);
            return catalogue != null && catalogue.skinDictionary.TryGetValue(type, out var list) ? list : null;
        }
        
        private void OnInitComplete()
        {
            SkinManagerComplete?.Invoke();
        }
    }
}
