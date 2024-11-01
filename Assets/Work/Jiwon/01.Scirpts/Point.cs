using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Point : MonoBehaviour
{
    private SavePoint _savePoint;

    public void Initialized(SavePoint savePoint)
    {
        _savePoint = savePoint;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _savePoint.whatIsTarget) != 0)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                _savePoint.SetTarget(entity);
                _savePoint.SetSavePointe(this);
                gameObject.SetActive(false);
            }
        }
    }
}
