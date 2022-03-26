using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public class SelfDestroy : MonoBehaviour
    {
        public float delay = 0.5f;
    
        private void Awake()
        {
            Destroy(gameObject, delay);
        }
    }
}
