using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class BPMController : MonoBehaviour
{
    public int BPM;
    //public Animation AnimationIndicator;
    public Animator animtor;
    public AudioSource AudioSource;

    //public  anim
    private float bpmInterval;
    private bool inBeat;
    private Image img;
    
    // Start is called before the first frame update
    void Start()
    {
        bpmInterval =(float) 60 / BPM;
       img = GetComponent<Image>();
       AnimatorSpeed();
       AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (inBeat)
        {
            img.color=Color.green;
        }
        else
        {
            img.color=Color.red;
        }
    }

    public void InBeat(int value)
    {
        //inBeat = value;
        //Debug.Log("InBeat : "+inBeat);
      if (value==1)
      {
          inBeat = true;
          Debug.Log("InBeat : "+inBeat);
      }
      else
      {
          Debug.Log("InBeat : "+inBeat);
          inBeat = false;
      }
    }

    private void AnimatorSpeed()
    {
        if (bpmInterval<0.5f)
        {
            animtor.speed= 2; 
        }
        else if (bpmInterval>=1f)
        {
            animtor.speed= 1; 
        }
        else if (bpmInterval>=0.5f&&bpmInterval<0.6f)
        {
            animtor.speed= 1.8f; 
        }
        else if (bpmInterval>=0.6f&&bpmInterval<0.7f)
        {
            animtor.speed= 1.6f; 
        }
        else if (bpmInterval>=0.7f&&bpmInterval<0.8f)
        {
            animtor.speed= 1.4f; 
        }
        else if (bpmInterval>=0.8f&&bpmInterval<0.9f)
        {
            animtor.speed= 1.2f; 
        }
        else if (bpmInterval>=0.9f&&bpmInterval<1f)
        {
            animtor.speed= 1.1f; 
        }
    }
}
