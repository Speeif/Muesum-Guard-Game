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

    void FixedUpdate()
	{
        
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		transform.rotation = Quaternion.LookRotation(movement);
		rb.AddForce(movement * speed);
	

	}

	void OnCollisionEnter(Collision collision)
	{
       
		if(collision.gameObject.tag == "Enemy"){
			
			Debug.Log("ENEMY TOUCHED");

            Rigidbody rbCol = collision.gameObject.GetComponent<Rigidbody>();
            rbCol.AddForce((collision.gameObject.transform.position - transform.position) *600);
            				
		}   
	}	
}