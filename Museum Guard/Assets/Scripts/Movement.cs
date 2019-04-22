using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anima;
    public float speed = 10.0f;

    private Rigidbody rb;
    bool walk = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Vector3.Magnitude(movement) > 0)
        {
            if (!walk) { anima.SetTrigger("walk"); walk = true; }
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (walk) { anima.SetTrigger("idle"); walk = false; }
        }
    }
}
