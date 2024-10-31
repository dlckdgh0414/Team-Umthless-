using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGMPool;

public class EffectFeedback : Feedback
{
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _effectType;
    [SerializeField] private Vector2 _particleSize;

    public override void PlayFeedback()
    {
        var effect = _poolManager.Pop(_effectType) as EffectPlay;
        effect.EffectPlayer(transform.position, transform.rotation);

        var shape = effect.GetComponent<ParticleSystem>().shape;
        shape.scale = _particleSize;
    }

    public override void StopFeedback()
    {
       
    }
}
