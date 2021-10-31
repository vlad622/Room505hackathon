using System;
using Mobs;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public delegate void MobCollision (Mob mob);
    public event MobCollision OnMobCollision;
    private void OnCollisionEnter(Collision other)
    {
        var mobBody = other.gameObject;
        Debug.Log("OnCollisionEneter");
        if (mobBody.CompareTag("Enemy"))
        {
            var mob = mobBody.GetComponentInParent<Mob>();
            //Debug.Log("Found Enemy");
            OnMobCollision?.Invoke(mob);
        }
    }
}
