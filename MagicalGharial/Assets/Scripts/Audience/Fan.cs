using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    
    bool underBubble = false;

    int maxWetness = 3;
    int currentWetness = 1;
    int bubbleWetness = 3;

    [SerializeField] float dryingTime = 10;
    float dryingTimer;

    [SerializeField] Animator animator;

    void Awake()
    {
        dryingTimer = dryingTime;
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
            Debug.Log("Dry" + currentWetness);
            UpdateAnimation();
        }
    }

    void TryWet()
    {
        if(underBubble)
        {
            currentWetness = Mathf.Clamp(currentWetness + bubbleWetness, 0, maxWetness);
            dryingTimer = dryingTime;
            Debug.Log("Wet" + currentWetness);
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
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Bubble"))
        {
            underBubble = false;
        }
    }

    void UpdateAnimation()
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
