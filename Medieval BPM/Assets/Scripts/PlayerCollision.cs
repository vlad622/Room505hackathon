using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action OnPlayerCollision;
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("OnCollisionEneter");
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Found Enemy");
            OnPlayerCollision?.Invoke();
        }
    }
}
