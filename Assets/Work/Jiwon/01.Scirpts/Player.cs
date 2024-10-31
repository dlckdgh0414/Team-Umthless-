using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader InputComp { get; private set; }

    private Entity _currentEntity;
    private CinemachineVirtualCamera _virtualCamera; 

    private void OnEnable()
    {
        
    }
        
    private void OnDisable()
    {
        
    }
}
