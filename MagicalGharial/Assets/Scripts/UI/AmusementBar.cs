using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmusementBar : MonoBehaviour
{
    [SerializeField] GameObject Audience;
    List<Fan> fans = new List<Fan>();

    [SerializeField] Slider slider;
    [SerializeField] int minEntertainment;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = new GameObject[Audience.transform.childCount];
        foreach(Transform obj in Audience.transform)
        {
            foreach(Transform obj2 in obj)
            {
                if(obj2.GetComponent<Fan>() != null)
                {
                    fans.Add(obj2.GetComponent<Fan>());
                }
            }
            if(obj.GetComponent<Fan>() != null)
            {
                fans.Add(obj.GetComponent<Fan>());
            }
        }
        slider.maxValue = 168;
    }

    void Update()
    {
        UpdateBar();
        if(slider.value < minEntertainment)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }

    void UpdateBar()
    {
        int barValue = 0;
        foreach(Fan fan in fans)
        {
            barValue = barValue + fan.currentWetness;
        }
        slider.value = barValue;
    }
}
