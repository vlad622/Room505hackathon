using System.Collections.Generic;
using UnityEngine;

namespace Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] mobVariants;
        [SerializeField] private Transform[] spawners;
        [SerializeField] private Transform spawnersParent;
        [SerializeField] private Transform mobsParent;
        [SerializeField] private float spawnRadius = 2f;
        [SerializeField] private Transform playerPosition;
        
        private Plane[] planes;
        private MobsPool[] mobPools;
        public float maxMobDistance = 10f;
        
        #region Singleton

        public static MobSpawner Instance; 

        private void Awake()
        {
            Instance = this;
        }

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
            spawners = new Transform[spawnersParent.childCount];

            for (var i = 0; i < spawnersParent.childCount; i++)
            {
                spawners[i] = spawnersParent.GetChild(i);
            }
            
            mobPools = new MobsPool[mobVariants.Length];

            for (var i = 0; i < mobVariants.Length; i++)
            {
                mobPools[i] = new MobsPool(mobVariants[i], 4, 3);
                
                for (var j = 0; j < mobPools[i].PoolSize; j++)
                {
                    var obj = Instantiate(mobPools[i].Prefab, mobsParent);
                    obj.name += j;
                    obj.GetComponent<Mob>().PoolID = j;
                    obj.GetComponent<Mob>().PlayerPosition = playerPosition;
                    
                    mobPools[i].AddSpawnedMob(obj.GetComponent<Mob>());
                }        
            }
            
        }
        
        private void Spawn(Transform[] _spawners)
        {
            if (_spawners.Length <= 0) return;
            
            Debug.Log("Spawn mob");
            
            foreach (var pool in mobPools)
            {
                var newMob = pool.spawnedMobs[0];
                var obj = newMob.gameObject;

                var spawnerIndex = UnityEngine.Random.Range(0, _spawners.Length);
                var randomX = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                var randomZ = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                var spawnPosition = new Vector3(_spawners[spawnerIndex].position.x + randomX,
                    _spawners[spawnerIndex].position.y, _spawners[spawnerIndex].position.z + randomZ);
                obj.transform.position = spawnPosition;
                obj.transform.eulerAngles = Vector3.zero;  //reset npc rotation

                pool.RemoveSpawnedMob(newMob);
                pool.AddActiveMob(newMob);
            }
        }
        
        /* Moves nps to pool */
        public void Pool(Mob _mob)
        {
            _mob.navMeshAgent.ResetPath();
            _mob.ResetPosition();
            mobPools[_mob.PoolID].AddSpawnedMob(_mob);
            mobPools[_mob.PoolID].AddSpawnedMob(_mob);
        }
        
        /* Checks if bounds visible on camera */
        public bool IsVisible(Bounds _bounds)
        {
            //Debug.Log("Is Spawner visible " + GeometryUtility.TestPlanesAABB(planes, _bounds));
            return GeometryUtility.TestPlanesAABB(planes, _bounds);
        }
        
        /* Returns array with spawners transforms that now invisible for camera */
        private Transform[] GetUnseenSpawners()
        {
            var result = new List<Transform>();

            foreach (var spawner in spawners)
            {
                var distance = (Camera.main.transform.position - spawner.transform.position).sqrMagnitude;

                if (!IsVisible(spawner.GetComponent<Collider>().bounds)) //&& distance < maxMobDistance)
                {
                    result.Add(spawner);
                }
            }
            
            //Debug.Log("Unseen spawners" + result.Count);

            return result.ToArray();
        }
        
        private void LateUpdate()
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

            var missingMobsCount = 0;
            
            foreach (var pool in mobPools)
            {
                //Debug.Log(pool.MaxActiveNum + " maxActiveNum" + pool.activeMobs.Count +"activeMobs");
                missingMobsCount = pool.MaxActiveNum - pool.activeMobs.Count;
                //Debug.Log("missingMobsCount" + missingMobsCount);
            }
            
            if (missingMobsCount <= 0) return;
            var _spawners = GetUnseenSpawners();
           
            for (int i = 0; i < missingMobsCount; i++)
            {
                Spawn(_spawners);
            }
        }

    }
}

