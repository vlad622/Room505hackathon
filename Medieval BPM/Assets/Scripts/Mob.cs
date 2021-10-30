using System;
using UnityEngine;
using UnityEngine.AI;

namespace Mobs
{
    enum MobAnimation
    {
        Idle = 0,
        Walk = 1,
        Attack = 2
    }
    public class Mob : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private Transform ragdollBody;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider collider;
        [SerializeField] private MobAnimation animation;
        [NonSerialized] private float attackDistance = 5f;
        public int PoolID { get; set; }
        public Transform PlayerPosition { private get; set; }
        public NavMeshAgent navMeshAgent;

        private float distanceToPlayer;

        /* Resets position of body and ragdoll to 0 relative to npc root */
        public void ResetPosition()
        {
            body.transform.position = gameObject.transform.position;
            ragdollBody.transform.position = gameObject.transform.position;
        }
        

        /* Selects between Idle and Walk animation when npc moves */
        private void UpdateAnimation()
        {
            animator.SetFloat("Blend", (float)animation);
        }

        /* Finds next point to follow and sets it as destination for navMeshAgent */
        private void GoToPlayer()
        {
            animation = MobAnimation.Walk;
            navMeshAgent.SetDestination(PlayerPosition.position);
        }

        private void Attack()
        {
            animation = MobAnimation.Attack;
        }
        
        void FixedUpdate()
        {
            distanceToPlayer = (Camera.main.transform.position -
                           navMeshAgent.transform.position).magnitude;
            //Debug.Log(distanceToPlayer+ " " + attackDistance);
            
            // Chooses next destination point 
            if (navMeshAgent.isOnNavMesh)
            {
                if (distanceToPlayer < attackDistance)
                {
                    Attack();
                }
                else
                {
                    GoToPlayer();
                }
            }
            else
            {
                animation = MobAnimation.Idle;
            }
            
            UpdateAnimation();
        }
    }
}