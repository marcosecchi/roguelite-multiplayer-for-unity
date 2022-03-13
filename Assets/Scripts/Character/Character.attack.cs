using UnityEngine.InputSystem;

namespace TheBitCave.MultiplayerRoguelite
{
    public partial class Character
    {
        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Attack.started += OnAttackStarted;
        }
        
        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Attack.started -= OnAttackStarted;
        }
        
        private void OnAttackStarted(InputAction.CallbackContext obj)
        {
            if(abilityAttack != null) abilityAttack.Attack();
        }
    }
}
