using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void PlayFeedback();

    public virtual void StopFeedback()
    {

    }

    private void OnDisable()
    {
        StopFeedback();
    }
}
