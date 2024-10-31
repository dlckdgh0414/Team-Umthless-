using UnityEngine;

[CreateAssetMenu(menuName = "SO/Animal/Data")]
public class AnimalDataSO : ScriptableObject
{
    public Sprite IconSprite;
    public float moveSpeed;
    public float jumpPower;
    public float camFov;
}
