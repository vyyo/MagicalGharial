using UnityEngine.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "Bubble", order = 0)]
public class Bubble : ScriptableObject
{
    //public BubbleType bubbleType;
    //public BubbleType[] bubbleSequence;
    public Sprite bubbleSprite;
    public Sprite collisionSprite;
    public float speed = 1;
    //public RuntimeAnimatorController animations;

    public enum BubbleType
    {
        Heart = 1,
        Club = 2
    }

}