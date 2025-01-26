using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    
    bool underBubble = false;

    int maxWetness = 3;
    public int currentWetness = 1;
    int bubbleWetness = 3;

    [SerializeField] float dryingTime = 10;
    float dryingTimer;

    [SerializeField] float wetnessSpeedDenominator = 5;

    [SerializeField] Animator animator;

    [SerializeField] bool hater = false;

    void Awake()
    {
        dryingTimer = dryingTime;
        if(hater)
        {
            maxWetness = 0;
            currentWetness = 0;
        }
    }

    void OnEnable()
    {
        Player.OnPopped += TryWet;
    }

    void OnDisable()
    {
        Player.OnPopped -= TryWet;
    }
    
    void Update()
    {
        if(dryingTimer > 0)
        {
            dryingTimer -= Time.deltaTime;
        }
        else
        {
            currentWetness = Mathf.Clamp(currentWetness - 1, 0, maxWetness);
            dryingTimer = dryingTime;
            UpdateAnimation();
        }
    }

    void TryWet()
    {
        if(underBubble)
        {
            currentWetness = Mathf.Clamp(currentWetness + bubbleWetness, 0, maxWetness);
            dryingTimer = dryingTime;
            UpdateAnimation();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bubble"))
        {
            underBubble = true;
            //bubbleWetness = other.BubbleContainer.bubbleWetness;
        }
        if(other.CompareTag("Player") && hater)
        {
            other.gameObject.GetComponentInParent<Player>().hasSnacked = true;
            Destroy(gameObject, 0.1f);
        }
        if(other.CompareTag("Speed"))
        {
            other.gameObject.GetComponentInParent<Player>().moveSpeed = other.gameObject.GetComponentInParent<Player>().moveSpeed + currentWetness/wetnessSpeedDenominator;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Bubble"))
        {
            underBubble = false;
        }
        if(other.CompareTag("Speed"))
        {
            other.gameObject.GetComponentInParent<Player>().moveSpeed = other.gameObject.GetComponentInParent<Player>().moveSpeed - currentWetness/wetnessSpeedDenominator;
        }
    }

    void UpdateAnimation()
    {
        if(hater == false)
        {
            switch(currentWetness)
            {
                case 0:
                    animator.Play("viewer_displeased");
                    break;
                case 1:
                    animator.Play("viewer_neutral");
                    break;
                case 2:
                    animator.Play("viewer_amused");
                    break;
                case 3:
                    animator.Play("viewer_elated");
                    break;
            }
        }

    }
}
