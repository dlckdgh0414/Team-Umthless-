using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGMPool;

public class EffectFeedback : Feedback
{
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _effectType;

    public override void PlayFeedback()
    {
        var effect = _poolManager.Pop(_effectType) as EffectPlay;
        effect.EffectPlayer(transform.position, transform.rotation);
    }

    public override void StopFeedback()
    {
       
    }
}
