using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHolder : MonoBehaviour
{
    [SerializeField] Player player;
    int comboSize;
    List<int> comboValues = new List<int>();
    [SerializeField] SpriteRenderer[] comboSprites;
    [SerializeField] Sprite[] comboBubbles;
    
    void Start()
    {
        comboSize = player.currentComboSequence.Count;
        foreach (var symbol in player.currentComboSequence)
        {
            int value = (int)symbol;
            Debug.Log(value);
        }
    }

    void Update()
    {
        if(comboSize != player.currentComboSequence.Count)
        {
            comboSize = player.currentComboSequence.Count;
            foreach (var symbol in player.currentComboSequence)
            {
                int value = (int)symbol;
                comboValues.Add((int)symbol);
                //Debug.Log(value);
            }
            for (int i = 0; i < comboSprites.Length; i++)
            {
                if(i < comboValues.Count)
                {
                    int value = comboValues[i];
                    comboSprites[i].sprite = comboBubbles[value];
                }
                else
                {
                    comboSprites[i].sprite = null;
                }
            }
            comboValues.Clear();
        }
    }
}
