using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private bool damageble;

    public void Damageble(int value)
    {
        if (value == 1)
        {
            damageble = true;
        }
        else
        {
            damageble = false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
