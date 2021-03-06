using Mirror;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Prototype
{
    [AddComponentMenu("")]
    public class ProtoPickup : BasePickup
    {
        public int points = 1;
        
        // ServerCallback because we don't want a warning
        // if OnTriggerEnter is called on the client
        protected override void OnTriggerEnter(Collider other)
        {
            if (!isServer) return;
            var controller = other.GetComponent<ProtoCharacter>();
            if (controller == null) return;
            controller.AddPoints(points);
            NetworkServer.Destroy(gameObject);            
        }

        protected override void Pick(GameObject picker)
        {
            var controller = picker.GetComponent<ProtoCharacter>();
            if (controller == null) return;
            controller.AddPoints(points);
            NetworkServer.Destroy(gameObject);            
        }
    }
}
