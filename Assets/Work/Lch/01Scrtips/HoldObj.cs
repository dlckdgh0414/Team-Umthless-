using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObj : MonoBehaviour
{
	private APE _ape;
    private Rigidbody2D _rbCompo;

    private void Awake()
    {
        _ape = FindObjectOfType<APE>();
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    public void Throwing()
    {
        _rbCompo.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        _rbCompo.AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
    }

    public void HoidingObj()
    {
        _rbCompo.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = _ape.transform;
        transform.position = _ape._holdTrm.position;
        _ape.isHold = true;
    }
}
