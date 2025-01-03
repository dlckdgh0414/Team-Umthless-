using UnityEngine;
using UnityEngine.UI;

public class AudioBar : MonoBehaviour
{
    [SerializeField] private AudioDataSO _data;
    [SerializeField] private float _startVolumeValue = 0.5f;

    private Slider _bar;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }

    private void Start()
    {
        _data.audioMixer.SetFloat("BGMVolume", _data.bgmValue);
        _data.audioMixer.SetFloat("SFXVolume", _data.sfxValue);
    }

    public void OnSFXValueChange()
    {
        _data.sfxValue = _bar.value;
        _data.audioMixer.SetFloat("SFXVolume", Mathf.Log10(_data.sfxValue) * 20);
    }

    public void OnBGMValueChange()
    {
        _data.bgmValue = _bar.value;
        _data.audioMixer.SetFloat("BGMVolume", Mathf.Log10(_data.bgmValue) * 20);
    }
}
