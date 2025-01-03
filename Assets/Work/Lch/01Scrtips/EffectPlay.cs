using GGMPool;
using System.Collections;
using UnityEngine;

public class EffectPlay : MonoBehaviour, IPoolable
{

    [SerializeField] private PoolTypeSO _poolType;

    [SerializeField] private ParticleSystem _particle;

    private Pool _myPool;

    public PoolTypeSO PoolType => _poolType;

    public GameObject GameObject => gameObject;

    public void EffectPlayer(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        _particle.Play();
        //_particle.transform.position = pos;
        float duration = _particle.main.duration;
        StartCoroutine(DelayToPool(duration));
    }

    private IEnumerator DelayToPool(float time)
    {
        yield return new WaitForSeconds(time);
        _myPool.Push(this);
    }

    public void ResetItem()
    {
        transform.position = Vector3.zero;
    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }
}
