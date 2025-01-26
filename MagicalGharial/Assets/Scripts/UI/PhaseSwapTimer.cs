using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseSwapTimer : MonoBehaviour
{
    float remainingTime;
    [SerializeField] float bubblePhaseDuration;
    [SerializeField] float snackPhaseDuration;
    float currentPhaseDuration;

    [SerializeField] Image timerImage;
    [SerializeField] Player player;
    [SerializeField] GameObject bubblePos;
    [SerializeField] GameObject walkPos;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = bubblePhaseDuration;
        currentPhaseDuration = bubblePhaseDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hasSnacked)
        {
            //player.hasSnacked = false;
            currentPhaseDuration = bubblePhaseDuration;
            remainingTime = currentPhaseDuration;
            player.walking = false;
            player.transform.position = bubblePos.transform.position;
            player.transform.rotation = bubblePos.transform.rotation;
        }
        if (remainingTime <= 0)
        {
            player.walking = !player.walking;
            player.currentComboSequence.Clear();
            if(player.walking == false)
            {
                currentPhaseDuration = bubblePhaseDuration;
                player.transform.position = bubblePos.transform.position;
                player.transform.rotation = bubblePos.transform.rotation;
            }
            else
            {
                currentPhaseDuration = snackPhaseDuration;
                player.transform.position = walkPos.transform.position;
                if(player.canMove == false)
                {
                    player.PopCall();
                }
            }
            remainingTime = currentPhaseDuration;
        }
        else
        {
            remainingTime -= Time.deltaTime;
        }
        timerImage.fillAmount = remainingTime/currentPhaseDuration;
    }
}
