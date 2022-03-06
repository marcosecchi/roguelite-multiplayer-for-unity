using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    public static class GameObjectUtils
    {
        #region GameObjects

        public static void RemoveAllChildren(this GameObject go)
        {
            RemoveAllChildren(go.transform);
        }
        
        #endregion
        
        #region Transform

        public static void RemoveAllChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }
        
        #endregion
    }
}
