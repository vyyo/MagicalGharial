using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] GameObject target1;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject cartBody;
    [SerializeField] float cartSpeed = 1.5f;
    GameObject currentTarget;
    void Start()
    {
        currentTarget = target1;
    }

    // Update is called once per frame
    void Update()
    {
        cartBody.transform.position = Vector3.MoveTowards(cartBody.transform.position, currentTarget.transform.position, cartSpeed * Time.deltaTime);
        if (cartBody.transform.position == target1.transform.position)
        {
            currentTarget = target2;
            cartBody.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(cartBody.transform.position == target2.transform.position)
        {
            currentTarget = target1;
            cartBody.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
