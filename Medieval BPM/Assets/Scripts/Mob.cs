using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

namespace Mobs
{
    enum MobAnimation
    {
        Idle = 0,
        Walk = 1,
        Attack = 2
    }

    enum MobType
    {
        Skeleton,
        Slime,
        Turtle
    }
    
    public class Mob : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private Transform ragdollBody;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider collider;
        [SerializeField] private MobAnimation animation;
        [SerializeField] private MobType type;
        [NonSerialized] private float attackDistance = 5f;
        public int PoolID { get; set; }
        public Transform PlayerPosition { private get; set; }
        public NavMeshAgent navMeshAgent;

        private float distanceToPlayer;
        private int health = 100;
        private bool dead = false;

        public void GetDamage(int damage)
        {
            if (health > damage)
                health -= damage;
            else
                Die();
        }

        private void Die()
        {
            dead = true;
            navMeshAgent.enabled = false;
            animator.Play("Die");
        }

        /* Resets position of body and ragdoll to 0 relative to npc root */
        public void ResetPosition()
        {
            body.transform.position = gameObject.transform.position;
            ragdollBody.transform.position = gameObject.transform.position;
        }
        
        
        /* Resets position of body and ragdoll to 0 relative to npc root */
        public void ResetHealth()
        {
            dead = false;
            health = 100;
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
            if (!dead)
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
            }

            UpdateAnimation();
        }
    }
}