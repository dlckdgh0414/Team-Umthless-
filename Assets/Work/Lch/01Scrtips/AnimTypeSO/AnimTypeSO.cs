using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimParm
{
    None ,Idle, Move, Skill, Jump, fail, dashCrash, push, climb, OnHand, OnHandMove
}
public enum Animals
{
    None, Rabit,Cat,Rat,Iizard,Chicken,Bunny,Rhino,Elephant,Bat,Ape,Human,Spider
}
[CreateAssetMenu(menuName = "Anim/SO/AnimType")]
public class AnimTypeSO : ScriptableObject
{
    public AnimParm animType;
    public Animals animals;
	public int hashValue;

    private void OnValidate()
    {
        if (animType == AnimParm.None) return;
        hashValue = Animator.StringToHash(animals.ToString()+animType.ToString());
    }
}
