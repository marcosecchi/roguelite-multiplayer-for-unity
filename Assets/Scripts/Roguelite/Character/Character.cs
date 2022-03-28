using Mirror;
using TheBitCave.BattleRoyale.Abilities;
using TheBitCave.BattleRoyale.Data;
using TheBitCave.BattleRoyale.Interfaces;
using TheBitCave.BattleRoyale.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheBitCave.BattleRoyale
{
    /// <summary>
    /// The main controller for every character: handles movement, attacks and other features.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu(menuName: "BattleRoyale/Character")]
    public partial class Character : NetworkBehaviour, ICharacterTypeable
    {
        [Header("Stats")]
        [SerializeField]
        protected CharacterStatsSO stats;

        [Header("Components")]
        [SerializeField]
        protected Animator animator;
        
        protected InputActions inputActions;
        protected CharacterController characterController;
        protected AbilityRangedAttack rangedAttack;
        protected AbilityCloseCombatAttack closeCombatAttack;
        protected DamageableProximityChecker damageableProximityChecker;

        [SyncVar]
        protected bool isInCloseCombatRange;
        
        #region Mirror Callbacks

        public override void OnStartServer()
        {
            var health = GetComponent<Health>();
            if (health != null) health.StartingHitPoints = stats.StartingHitPoints;
        }
        
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

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            inputActions.Player.Enable();
        }

        #endregion

        #region Unity Callbacks

        protected virtual void Awake()
        {
            inputActions = InputManager.Instance.Actions;
            characterController = GetComponent<CharacterController>();
            rangedAttack = GetComponent<AbilityRangedAttack>();
            closeCombatAttack = GetComponent<AbilityCloseCombatAttack>();
            damageableProximityChecker = GetComponent<DamageableProximityChecker>();
            OnDamageableOut();
        }

        protected virtual void OnEnable()
        {
            if (isLocalPlayer)
            {
                inputActions.Player.Enable();
            }
            if (damageableProximityChecker != null)
            {
                damageableProximityChecker.OnDamageableEnter += OnDamageableIn;
                damageableProximityChecker.OnDamageableExit += OnDamageableOut;
                
            }
        }

        protected virtual void OnDisable()
        {
            if (isLocalPlayer)
            {
                inputActions.Player.Disable();
            }
            if (damageableProximityChecker != null)
            {
                damageableProximityChecker.OnDamageableEnter -= OnDamageableIn;
                damageableProximityChecker.OnDamageableExit -= OnDamageableOut;
            }
        }
        
        /// <summary>
        /// Handles local player input
        /// </summary>
        protected virtual void FixedUpdate()
        {
            // Handle local player input
            if (isLocalPlayer)
            {
                // Player Input
                var input = inputActions.Player.Move.ReadValue<Vector2>();
                var isRunning = inputActions.Player.Run.inProgress;
                var speed = isRunning ? Stats.RunSpeed : Stats.WalkSpeed;
            
                // Character Movement
                var t = transform;
                var move = Mathf.Clamp01(input.y) * speed * t.forward;
                characterController.SimpleMove(move);

                var rotation = input.x * Stats.RotationSpeed * t.up;
                transform.Rotate(rotation);
            
                // Animator update
                if (animator == null) return;
                animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, characterController.velocity.magnitude);
            }
            // Handle server operations 
            else if (isServer)
            {
                // TODO: Implement close combat range checking
                isInCloseCombatRange = false;
            }
        }

        #endregion

        #region Attack

        protected virtual void OnAttackStarted(InputAction.CallbackContext obj)
        {
            if (isInCloseCombatRange && closeCombatAttack)
            {
                closeCombatAttack.Attack();
            }
            else if (rangedAttack != null)
            {
                rangedAttack.Attack();
            }
        }

        protected virtual void OnDamageableIn()
        {
            isInCloseCombatRange = true;
            UpdateWeaponModels();
        }

        protected virtual void OnDamageableOut()
        {
            isInCloseCombatRange = false;
            UpdateWeaponModels();
        }

        /// <summary>
        /// Shows/hides weapons depending on character state (in close combat/ranged)
        /// </summary>
        public virtual void UpdateWeaponModels()
        {
            closeCombatAttack.SetWeaponVisibility(isInCloseCombatRange);
            rangedAttack.SetWeaponVisibility(!isInCloseCombatRange);
        }
        
        #endregion
        
        #region Getters and Setters

        public CharacterStatsSO Stats => stats;

        #endregion

        #region ITypeable implementation 
        
        public CharacterType Type => stats.Type;
        public virtual string TypeStringified => C.GetCharacterTypeLabel(stats.Type);
        public CharacterAlignment Alignment => stats.Alignment;
        public virtual string AlignmentStringified => C.GetCharacterAlignmentLabel(stats.Alignment);
        
        #endregion
    }
}
