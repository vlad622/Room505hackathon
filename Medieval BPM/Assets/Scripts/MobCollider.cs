using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour
{
    private int damage = 5;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Mob collides with Player");
            PlayerLogic.Instance.DamageStarted(damage);
        }
    }
}
