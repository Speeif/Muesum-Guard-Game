using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   	
	public float speed = 10.0f;

	private Rigidbody rb;

    void Start()
    {
		rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
		float moveHorizontal = Input.GetAxis("Horizontal");

		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		transform.rotation = Quaternion.LookRotation(movement);
		transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }
}
