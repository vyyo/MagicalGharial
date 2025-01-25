using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class BubbleContainer : MonoBehaviour
{
    private float speed;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
            when space is pressed
            burst event
            Destroy(gameObject, 0.1f);
        */
    }

    public void FillContainer(Bubble bubble)
    {
        speed = bubble.speed;

        spriteRenderer.sprite = bubble.bubbleSprite;
        //animator.runtimeAnimatorController = bubble.animations as RuntimeAnimatorController;
        //animator.SetFloat("Resistance", resistance);


        //collision setup
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;
    }
}
