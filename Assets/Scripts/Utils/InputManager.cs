using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    /// <summary>
    /// A persistent singleton class used to initialize and to handle player input 
    /// </summary>
    public class InputManager : PersistentSingleton<InputManager>
    {
        private InputActions _actions;
        
        public override void Awake()
        {
            base.Awake();
            _actions = new InputActions();
        }

        public InputActions Actions => _actions;
    }
}
