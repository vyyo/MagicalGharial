using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseSwapTimer : MonoBehaviour
{
    float remainingTime;
    [SerializeField] float phaseDuration;

    [SerializeField] Image timerImage;
    [SerializeField] Player player;
    [SerializeField] GameObject bubblePos;
    [SerializeField] GameObject walkPos;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = phaseDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hasSnacked)
        {
            player.hasSnacked = false;
            remainingTime = phaseDuration;
            player.walking = false;
            player.transform.position = bubblePos.transform.position;
            player.transform.rotation = bubblePos.transform.rotation;
        }
        if (remainingTime <= 0)
        {
            remainingTime = phaseDuration;
            player.walking = !player.walking;
            player.currentComboSequence.Clear();
            if(player.walking == false)
            {
                player.transform.position = bubblePos.transform.position;
                player.transform.rotation = bubblePos.transform.rotation;
            }
            else
            {
                player.transform.position = walkPos.transform.position;
            }
        }
        else
        {
            remainingTime -= Time.deltaTime;
        }
        timerImage.fillAmount = remainingTime/phaseDuration;
    }
}
