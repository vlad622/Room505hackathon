using UnityEngine;
using UnityEngine.AI;

namespace Mobs
{
    public class Mob : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private Transform ragdollBody;
        [SerializeField] private Animator animator;
        public int PoolID { get; set; }
        public NavMeshAgent navMeshAgent;

        //Transform[] pointsArr; // Array of points between witch NPCs should walk 
        float camDistance; // current distance between npc and camera
        Vector3 lastPos; // NPC position on last frame
        float speed; // current npc speed

        void Start()
        {
            // Fill up array of pointsOfInterest
            //Transform _pointsOfInterest = GameObject.FindWithTag("PointsOfInterest").transform;
           // pointsArr = new Transform[_pointsOfInterest.childCount];
            lastPos = body.position;

            //for (int _i = 0; _i < _pointsOfInterest.childCount; _i++)
            //{
            //    pointsArr[_i] = _pointsOfInterest.GetChild(_i);
            //}
        }

        /* Resets position of body and ragdoll to 0 relative to npc root */
        public void ResetPosition()
        {
            body.transform.position = gameObject.transform.position;
            ragdollBody.transform.position = gameObject.transform.position;
        }

        /* Stops navMeshAgent when fallen */
        public void OnLoseBallance()
        {
            navMeshAgent.enabled = false;
        }

        /* Enables navMeshAgent when got up */
        public void OnRegainBallance()
        {
            navMeshAgent.enabled = true;
        }

        /* Selects between Idle and Walk animation when npc moves */
        private void UpdateAnimation()
        {
            animator.SetFloat("Forward", Mathf.Pow(speed, 7));
        }

        /* Finds next point to follow and sets it as destination for navMeshAgent */
        private void GoToPlayer()
        {
            navMeshAgent.SetDestination(PlayerPosition.position);
        }

        public Transform PlayerPosition
        { private get; set; }

        private void Update()
        {
            // Update speed every frame
            speed = (Vector3.Distance(lastPos, body.position) / Time.deltaTime) / 2.0f;
            lastPos = body.position;
        }

        void FixedUpdate()
        {
            camDistance = (Camera.main.transform.position -
                           navMeshAgent.transform.position).sqrMagnitude;

            // If npc is invisible for camera, its animator and ragdoll will be disabled.
            //if (MobSpawner.Instance.IsVisible(collider.bounds))
            //{
            //    animator.enabled = true;
            //    puppetMaster.mode = PuppetMaster.Mode.Active;
            //    UpdateAnimation();
            //}
            //else
            //{
            //    animator.enabled = false;
            //    puppetMaster.mode = PuppetMaster.Mode.Disabled;
//
            //    // If npc to far from camera, it will be pooled;
            //    if (camDistance > MobSpawner.Instance.npcDistance)
            //    {
            //        MobSpawner.Instance.Pool(this);
            //        //Debug.Log("pool " + name);
            //    }
            //}

            // Chooses next destination point 
            if (navMeshAgent.isOnNavMesh)
            {
                GoToPlayer();
            }
        }
    }
}