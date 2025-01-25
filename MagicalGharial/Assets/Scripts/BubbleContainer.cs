using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleContainer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float upSpeed = 1;
    [SerializeField] SpriteRenderer spriteRenderer;

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
        moveSpeed = bubble.speed;
        spriteRenderer.sprite = bubble.collisionSprite;


        //collision setup
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;

        spriteRenderer.sprite = bubble.bubbleSprite;
    }

    public void Pop()
    {
        Destroy(gameObject, 0.1f);
    }
}
