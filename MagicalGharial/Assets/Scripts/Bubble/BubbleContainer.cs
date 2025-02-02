using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleContainer : MonoBehaviour
{
    float moveSpeed = 1;
    float upSpeed = 1;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem bubbleParticle;

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (-moveSpeed * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (moveSpeed * Time.deltaTime, 0);
        }
        MoveUp();
    }

    void MoveUp()
    {
        transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (0, upSpeed * Time.deltaTime);
    }

    public void FillContainer(Bubble bubble)
    {
        moveSpeed = bubble.moveSpeed;
        upSpeed = bubble.upSpeed;
        spriteRenderer.sprite = bubble.collisionSprite;


        //collision setup
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;

        spriteRenderer.sprite = bubble.bubbleSprite;
    }

    public void Pop()
    {
        spriteRenderer.sprite = null;
        bubbleParticle.Play();
        Destroy(gameObject, 0.5f);
    }
}
