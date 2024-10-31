using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimals : MonoBehaviour
{
	[SerializeField] private GameObject[] _animals;

    private void Start()
    {
        for(int i = 0; i < _animals.Length; i++)
        {
            _animals[i].SetActive(false);
        }
    }
}
