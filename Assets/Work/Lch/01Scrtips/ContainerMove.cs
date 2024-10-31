using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMove : MonoBehaviour
{

    private Rigidbody2D _rbCompo;

   [SerializeField] private AnimalDataSO _move;

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }


    public void ContaineMover()
    {
        _rbCompo.velocity = new Vector2(_move.moveSpeed, _rbCompo.velocity.y);
    }
}
