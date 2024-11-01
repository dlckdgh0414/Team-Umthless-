using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "SO/AudioData")]
public class AudioDataSO : ScriptableObject
{
    public AudioMixer audioMixer;
    public float sfxValue;
    public float bgmValue;
}
