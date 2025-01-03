using GGMPool;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _poolType;
    [SerializeField] private SoundSO _soundData;

    private Pool _pool;

    public override void PlayFeedback()
    {
        SoundPlayer soundPlayer = _poolManager.Pop(_poolType) as SoundPlayer;

        soundPlayer.PlaySound(_soundData);
    }

    public override void StopFeedback()
    {

    }
}
