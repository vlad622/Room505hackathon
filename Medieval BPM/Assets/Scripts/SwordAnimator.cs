using System;

using UnityEngine;

public class SwordAnimator : MonoBehaviour
{
    public event Action SwordInDamageZone;
    public event Action SwordOutDamageZone;

    #region Singletone

    public static SwordAnimator Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void StartDamageZone()
    {
        SwordInDamageZone?.Invoke();
    }

    private void StopDamageZone()
    {
        SwordOutDamageZone?.Invoke();
    }
}
