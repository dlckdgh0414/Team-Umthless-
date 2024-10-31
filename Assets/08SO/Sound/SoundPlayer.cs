using DG.Tweening;
using GGMPool;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private AudioMixerGroup _sfxGroup, _BGM;
    [SerializeField] private string _poolName;
    [SerializeField] private PoolTypeSO _poolType;
    private Pool _myPool;

    public PoolTypeSO PoolType => _poolType;

    public GameObject GameObject => gameObject;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundSO data)
    {
        //재생해야할 그룹을 정해주고
        if (data.audioType == AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = _sfxGroup;
        }
        else if (data.audioType == AudioType.BGM)
        {
            _audioSource.outputAudioMixerGroup = _BGM;
        }

        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;
        if (data.randomizePitch)
        {
            _audioSource.pitch
                += Random.Range(-data.randomPicthModifier, data.randomPicthModifier);
        }
        _audioSource.clip = data.clip;
        _audioSource.loop = data.loop;

        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () =>_myPool.Push(this));
        }
        _audioSource.Play();
    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        _myPool.Push(this);
    }

    public void ResetItem()
    {

    }

    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }
}
