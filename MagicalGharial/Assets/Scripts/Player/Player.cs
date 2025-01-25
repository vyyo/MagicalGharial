using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;

    //Move
    Vector2 move;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float upSpeed = 2;
    //Combo Sequence Vars
    [SerializeField] float comboResetTimer = 1;
    float comboTimer;
    [SerializeField] int maxComboSize = 6;
    [SerializedDictionary("Combo Name", "Combo")] public SerializedDictionary<Bubble, List<ComboSymbols>> combos;
    private List<ComboSymbols> currentComboSequence = new List<ComboSymbols>();
    BubbleContainer bubbleContainer;

    //Pop Vars
    bool pop = false;

    public BubbleContainer target;

    void Awake()
    {
        comboTimer = comboResetTimer;

        bubbleContainer = GetComponent<BubbleContainer>();
        controls = new PlayerControls();

        controls.BubbleMap.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.BubbleMap.Move.canceled += ctx => move = Vector2.zero;

        controls.BubbleMap.Circle.performed += ctx => UpdateCombo(ComboSymbols.Circle);
        controls.BubbleMap.Rectangle.performed += ctx => UpdateCombo(ComboSymbols.Rectangle);
        controls.BubbleMap.Triangle.performed += ctx => UpdateCombo(ComboSymbols.Triangle);

        controls.BubbleMap.Pop.performed += ctx => pop = true;
    }

    private void FixedUpdate()
    {
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

    void UpdateCombo(ComboSymbols newComboSymbol)
    {
        currentComboSequence.Add(newComboSymbol);
        comboTimer = comboResetTimer;
        Debug.Log(currentComboSequence);
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
                else
                {
                    Debug.Log("gwegerg");
                }
            }
        }
    }

    void Update()
    {
        //Move();
    }

    void FoundCombo(Bubble bubble)
    {
        bubbleContainer.FillContainer(bubble);
        Debug.Log(bubble);
    }

    void Move()
    {
        transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2(move.x * Time.deltaTime * moveSpeed, upSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        controls.BubbleMap.Enable();
    }

    void OnDisable()
    {
        controls.BubbleMap.Disable();
    }

    public enum ComboSymbols
    {
        Circle = 1,
        Rectangle = 2,
        Triangle = 3
    }
}
