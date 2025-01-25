using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Move
    Vector2 move;
    [SerializeField] float moveSpeed = 3;
    //Combo Sequence Vars
    [SerializeField] float comboResetTimer = 1;
    float comboTimer;
    [SerializeField] int maxComboSize = 5;
    [SerializedDictionary("Combo Name", "Combo")] public SerializedDictionary<Bubble, List<ComboSymbols>> combos;
    private List<ComboSymbols> currentComboSequence = new List<ComboSymbols>();
    [SerializeField] GameObject bubbleSpawn;
    [SerializeField] GameObject bubblePrefab;
    GameObject currentBubble;

    //Pop Vars
    public bool canMove = true;

    public BubbleContainer target;

    void Awake()
    {
        comboTimer = comboResetTimer;

        //bubbleContainer = GetComponent<BubbleContainer>();

        //controls.BubbleMap.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.BubbleMap.Move.canceled += ctx => move = Vector2.zero;

        //controls.BubbleMap.Pop.performed += ctx => pop = true;
    }

    private void Update()
    {
        if(canMove)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCombo(ComboSymbols.Circle);
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                UpdateCombo(ComboSymbols.Rectangle);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                UpdateCombo(ComboSymbols.Triangle);
            }

            if (currentComboSequence.Count > 0)
            {
                comboTimer -= Time.deltaTime;
                if(comboTimer <= 0)
                {
                    currentComboSequence.Clear();
                    Debug.Log("combo reset");
                }
            }
            else
            {
                comboTimer = comboResetTimer;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentBubble.GetComponent<BubbleContainer>().Pop();
                canMove = true;
            }
        }
    }

    void UpdateCombo(ComboSymbols newComboSymbol)
    {
        currentComboSequence.Add(newComboSymbol);
        comboTimer = comboResetTimer;
        //Debug.Log(newComboSymbol);
        /*foreach (ComboSymbols symbol in currentComboSequence)
        {
            Debug.Log(symbol);
        }*/
        if(currentComboSequence.Count > maxComboSize)
        {
            print("combo reset");
            currentComboSequence.Clear();
        }
        foreach(KeyValuePair<Bubble, List<ComboSymbols>> combo in combos)
        {
            if(currentComboSequence.Count == combo.Value.Count)
            {
                if(currentComboSequence.SequenceEqual(combo.Value))
                {
                    FoundCombo(combo.Key);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void FoundCombo(Bubble bubble)
    {
        var newBubble = Instantiate(bubblePrefab, bubbleSpawn.transform.position, bubbleSpawn.transform.rotation);
        newBubble.GetComponent<BubbleContainer>().FillContainer(bubble);
        currentComboSequence.Clear();
        currentBubble = newBubble;
        canMove = false;
    }
    void Move()
    {
        transform.position = new Vector2 (transform.position.x, transform.position.y) + (move * moveSpeed * Time.deltaTime);
    }
    public enum ComboSymbols
    {
        Circle = 1,
        Rectangle = 2,
        Triangle = 3
    }
}
