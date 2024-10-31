using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryFallingObj : MonoBehaviour
{
	public LayerMask PlayerLayermask;

	[SerializeField] private Vector2 size;

    private void FixedUpdate()
    {
       var collider = Physics2D.OverlapBox(transform.position,size,0,PlayerLayermask);

        if (collider)
        {
            var agent = collider.GetComponent<Player>();

            if(agent == null)
            {
                Destroy(collider.gameObject);
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
