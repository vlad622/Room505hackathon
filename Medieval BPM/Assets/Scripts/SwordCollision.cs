using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public event Action OnMobCollision;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEneter");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Found Enemy");
            OnMobCollision?.Invoke();
        }
    }
}
