using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsAnim : MonoBehaviour
{
	private Animator _animator;
    private Player _animals;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animals = FindObjectOfType<Player>();
    }

    public void SetParam(AnimTypeSO parm, bool value)
        => _animator.SetBool(parm.hashValue, value);
}
