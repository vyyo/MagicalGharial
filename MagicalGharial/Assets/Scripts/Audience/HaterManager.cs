using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HaterManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int hatersTotal;
    int hatersEaten;
    List<Fan> fans = new List<Fan>();
    void Awake()
    {
        PopulateFans();
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateHater();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hasSnacked)
        {
            hatersEaten = hatersEaten + 1;
            if(hatersEaten >= hatersTotal)
            {
                //win scene
                SceneManager.LoadScene(2);
            }
            else
            {
                GenerateHater();
            }
            player.hasSnacked = false;
        }
    }

    void GenerateHater()
    {
        PopulateFans();
        int randomFan = Random.Range(0, fans.Count);
        fans[randomFan].hater = true;
        fans[randomFan].UpdateAnimation();
    }

    void PopulateFans()
    {
        fans.Clear();
        foreach(Transform obj in transform)
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
    }
}
