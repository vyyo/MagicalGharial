using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    float remainingTime;
    [SerializeField] float gameDuration;
    [SerializeField] Image timerImage;
    [SerializeField] Player player;

    void Start()
    {
        remainingTime = gameDuration;
    }

    void Update()
    {
        if (remainingTime <= 0)
        {
            //gameover scene
            SceneManager.LoadScene(3);
        }
        else
        {
            remainingTime -= Time.deltaTime;
        }
        timerImage.fillAmount = remainingTime/gameDuration;
    }
}
