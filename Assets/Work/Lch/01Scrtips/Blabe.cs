using DG.Tweening;
using UnityEngine;

public class Blabe : MonoBehaviour
{
    private void Start()
    {
        RotObj();
    }

    private void RotObj()
    {
        transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental)
               .SetEase(Ease.Linear);
    }
}
