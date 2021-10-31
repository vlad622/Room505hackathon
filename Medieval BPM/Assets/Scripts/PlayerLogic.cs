using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]private int health=100 ;

    public int Health
    {
        get { return health; }
        set { health = value;
            healthImage.transform.localScale = new Vector3((float)health / 100, 1 ,1) ;
        }
    }

    [SerializeField]private int shield=0;
    public int Shield
    {
        get { return shield; }
        set { shield = value;
            shieldImage.transform.localScale = new Vector3((float)shield / 100, 1 ,1) ;
        }
    }
    
    [SerializeField]private Animator SwordAnimator;
    [SerializeField] private InputActionProperty attack;
    [SerializeField] private InputActionProperty damage;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image shieldImage;
    [SerializeField] private bool sadistAchievement = false;
    private bool defending=false;
    
    // Start is called before the first frame update
    void Start()
    {
        attack.action.started+= AttackActionOnstarted;
        damage.action.started+= DamageActionOnstarted;
        
    }

    private void AttackActionOnstarted(InputAction.CallbackContext obj)
    {
        Debug.Log("GetMouseButtonDown");
        SwordAnimator.Play("Attack", 0, 0.25f);
        Healing();
        defending = true;
    }
    
    private void DamageActionOnstarted(InputAction.CallbackContext obj)
    {
        Debug.Log("DamageActionOnstarted");
        Damage(20);
    }

    public void Healing()
    {
        Debug.Log("Healing enter");
        int ammount = 10;
        int n = health + ammount;
        int shieldHeal=0;
        int healthHeal=0;
        if (n > 100)
        {
            shieldHeal = n - 100;
            healthHeal = ammount - shieldHeal;
        }
        else
        {
            healthHeal = ammount;
        }
        Debug.Log("Healing healthHeal"+healthHeal);
        Debug.Log("Healing shieldHeal"+shieldHeal);
        Health += healthHeal;
        Shield += shieldHeal;

    }
    
    public void Damage(int value )
    {
        Debug.Log("Damage enter");
       // int ammount = 10;
        int damage = shield - value;
        int shieldDamage=0;
        int healthDamage=0;
        if (damage <=0)
        {
            shieldDamage = value+damage ;
            healthDamage = damage ;
        }
        else
        {
            shieldDamage = value;
        }
        Debug.Log("Healing healthHeal"+healthDamage);
        Debug.Log("Healing shieldHeal"+shieldDamage);
        Health += healthDamage;
        Shield -= shieldDamage;
        if (Health<=0)
        {
            Debug.Log("Dead");
            //TODO acievement
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
