using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    public class ProtoSpawner : NetworkBehaviour
    {
        [SerializeField] private BoxCollider spawnArea;

        [SerializeField] private GameObject objectPrefab;

        [SerializeField] private float minSpawnTime = .5f;
        [SerializeField] private float maxSpawnTime = 2f;

        private Bounds _bounds;
        
        private void Awake()
        {
            _bounds = spawnArea.bounds;
            spawnArea.enabled = true;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            StartCoroutine(nameof(SpawnSequence));
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            StopCoroutine(nameof(SpawnSequence));
        }

        private IEnumerator SpawnSequence()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

                var posX = Random.Range(_bounds.min.x, _bounds.max.x);
                var posZ = Random.Range(_bounds.min.z, _bounds.max.z);
                var pos = new Vector3(posX, transform.position.y, posZ);
                var go = Instantiate(objectPrefab, pos, Quaternion.identity);
     //          NetworkServer.Spawn(go);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, .5f);
            Gizmos.DrawCube(spawnArea.center, spawnArea.size);
        }
    }
}
