using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TheBitCave.BattleRoyale.Prototype
{
    [AddComponentMenu("")]
    public class ProtoSpawner : NetworkBehaviour
    {
        [SerializeField] private BoxCollider spawnArea;

        [SerializeField] private GameObject objectPrefab;

        [SerializeField] private float minSpawnTime = .5f;
        [SerializeField] private float maxSpawnTime = 2f;
        [SerializeField] private bool autoStart;

        private Bounds _bounds;

        private bool _isSpawning;
        
        private void Awake()
        {
            _bounds = spawnArea.bounds;
            spawnArea.enabled = false;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            if(autoStart) StartSpawning();
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            _isSpawning = false;
        }

        public void StartSpawning()
        {
            _isSpawning = true;
            if (isServer) StartCoroutine(nameof(SpawnSequence));
        }
        
        private IEnumerator SpawnSequence()
        {
            while (_isSpawning)
            {
                var posX = Random.Range(_bounds.min.x, _bounds.max.x);
                var posZ = Random.Range(_bounds.min.z, _bounds.max.z);
                var pos = new Vector3(posX, transform.position.y, posZ);
                var go = Instantiate(objectPrefab, pos, Quaternion.identity);
                NetworkServer.Spawn(go);

                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }
    }
}
