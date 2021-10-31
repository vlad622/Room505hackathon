using System.Collections.Generic;
using UnityEngine;

namespace Mobs
{
    public class MobsPool
    {
        public GameObject Prefab { get; }
        public int PoolSize { get; }
        public int MaxActiveNum { get; }

        public List<Mob> spawnedMobs = new List<Mob>();
        public List<Mob> activeMobs = new List<Mob>();

        SkinnedMeshRenderer[] bodies;

        public MobsPool(GameObject _prefab, int _poolSize, int _maxActive)
        {
            Prefab = _prefab;
            PoolSize = _poolSize;
            MaxActiveNum = _maxActive;
        }
        
        public void AddSpawnedMob(Mob _Mob)
        {
            spawnedMobs.Add(_Mob);
            _Mob.gameObject.SetActive(false);
            _Mob.ResetHealth();
        }

        public void RemoveSpawnedMob(Mob _Mob)
        {
            spawnedMobs.Remove(_Mob);
        }

        public void AddActiveMob(Mob _Mob)
        {
            Debug.Log("Activate mob");
            activeMobs.Add(_Mob);
            _Mob.gameObject.SetActive(true);
            _Mob.ResetHealth();
        }

        public void RemoveActiveMob(Mob _Mob)
        {
            activeMobs.Remove(_Mob);
            _Mob.gameObject.SetActive(false);
        }
    }
}