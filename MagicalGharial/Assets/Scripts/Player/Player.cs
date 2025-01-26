using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip audioClip;
    //Animation Vars
    Animator animator;
    //Move
    Vector2 move;
    public float moveSpeed = 1;
    //Combo Sequence Vars
    [SerializeField] float comboResetTimer = 1;
    float comboTimer;
    [SerializeField] int maxComboSize = 5;
    [SerializedDictionary("Combo Name", "Combo")] public SerializedDictionary<Bubble, List<ComboSymbols>> combos;
    [HideInInspector] public List<ComboSymbols> currentComboSequence = new List<ComboSymbols>();
    [SerializeField] GameObject bubbleSpawn;
    [SerializeField] GameObject bubblePrefab;
    GameObject currentBubble;

    //Pop Vars
    public delegate void PopAction();
    public static event PopAction OnPopped;
    public bool canMove = true;
    //fase change vars
    public bool walking = false;
    public bool hasSnacked = false;

    public BubbleContainer target;

    void Awake()
    {
        animator = GetComponent<Animator>();

        comboTimer = comboResetTimer;
    }

    private void Update()
    {
        if(walking == false)
        {
            if(canMove)
            {
                animator.Play("player_dance");
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    UpdateCombo(ComboSymbols.Circle);
                }
                if(Input.GetKeyDown(KeyCode.W))
                {
                    UpdateCombo(ComboSymbols.Triangle);
                }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    UpdateCombo(ComboSymbols.Rectangle);
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
                animator.Play("player_idle");
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    currentBubble.GetComponent<BubbleContainer>().Pop();
                    OnPopped?.Invoke();
                    canMove = true;
                    audioManager.PlaySfx(audioClip);
                }
            }
        }
        else
        {
            animator.Play("player_snackwalk");
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (-moveSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (moveSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (0, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (0, -moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 180);
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
        Circle = 0,
        Triangle = 1,
        Rectangle = 2
    }
}
