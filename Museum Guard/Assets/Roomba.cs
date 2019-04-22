using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : MonoBehaviour {

	public List<GameObject> sucked = new List<GameObject>();

	//Return function variables for the roomba should return
	public List<Transform> breadCrumbs = new List<Transform>();
	public Vector3 startPos;
	public bool goingBack = false;

	bool turning = false;
	bool turn = false;
	public bool eject = false;
	public bool stop = true;

	bool locked = false;
	Vector3 CollisionNormalVector;

	//Speed of the roomba (Follows the z axis)
	public float Speed = 2.5f;
	public float startSpeed;
	Vector3 dir = new Vector3(0,0,1);
	
	//Time Variables
	public float lasttime = 0;
	public float thistime = 0;

	public int layer;
	
	void OnCollisionEnter(Collision other){
		Debug.Log("COLLIDES!");
		if(other.gameObject.layer == layer && !locked){
			Debug.Log("COLLIDES2!");
			turn = true;
			locked = true;
			CollisionNormalVector = other.contacts[0].normal;
		}else if(other.gameObject.tag == "Suckable" && !eject){
			sucked.Add(other.gameObject);
			other.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.layer == layer && !locked){
			turn = true;
			locked = true;
			CollisionNormalVector = Quaternion.AngleAxis(180,Vector3.up) * transform.forward;
		}
	}

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startSpeed = Speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(turn && !stop){
			//Calculate the turning point
			Vector3 crossResult = Vector3.Cross(dir, CollisionNormalVector);

			if(crossResult.x  < 0.1 && crossResult.y  < 0.1 && crossResult.z  < 0.1 && crossResult.x  > -0.1 && crossResult.y  > -0.1 && crossResult.z > -0.1){
				Debug.Log("Back");
				StartCoroutine(RotateMe(Vector3.up * 180, 1f));
				dir = Quaternion.AngleAxis(180,Vector3.up) * dir;
			}else{
				Debug.Log("Turn");
				if(crossResult.x >= Vector3.zero.x && crossResult.y >= Vector3.zero.y && crossResult.z >= Vector3.zero.z){
					StartCoroutine(RotateMe(Vector3.up * -90,1));
					dir = Quaternion.AngleAxis(-90,Vector3.up) * dir;
				}else if(crossResult.x <= Vector3.zero.x && crossResult.y <= Vector3.zero.y && crossResult.z <= Vector3.zero.z){
					StartCoroutine(RotateMe(Vector3.up * 90, 1f));
					dir = Quaternion.AngleAxis(90,Vector3.up) * dir;
				}
			}

			turning = true;
			turn = false;

		}else if (!turning && !stop){
			locked = false;
			transform.position = Vector3.Lerp(transform.localPosition,transform.localPosition + (dir)/100 * Speed,1);
		}
		// Debug.Log("forward = "+ transform.forward + "newPos = " + newPos);
		if(Input.GetKeyDown("e")){
			launchSucked();
		}

		if(Input.GetKeyDown("q")){
			startStop();
		}

		//Go back from to whence you came.
		if(Input.GetKeyDown("t") && !(startPos.x < 0.05f && startPos.x > -0.05f && startPos.z < 0.05f && startPos.z > -0.05f)){
			returnHome();
		}
		
		if( startPos.x < 0.5f && startPos.x > -0.5f && startPos.z < 0.5f && startPos.z > -0.5f && goingBack){
			Debug.Log("Backnow");
			Debug.Log((transform.position -startPos));
			Speed = startSpeed;
			StartCoroutine(RotateMe(Vector3.up * 180, 1f));
			dir = Quaternion.AngleAxis(180,Vector3.up) * dir;
			StartCoroutine(TransformBack(transform.position,startPos,0.2f, 0.5f));
			turning = true;
			turn = false;
			stop = true;
			goingBack = false;
		}

		if(thistime - 0.0001f > lasttime){
			eject = false;
		}
		thistime = Time.deltaTime;
	}

	IEnumerator TransformBack(Vector3 a, Vector3 b, float c, float time){
		{
		float elapsedTime = 0;
		Vector3 startingPos = transform.position;
		while (elapsedTime < time)
		{
		transform.position = Vector3.Lerp(startingPos, b, (elapsedTime / time));
		elapsedTime += Time.deltaTime;
		yield return new WaitForEndOfFrame();
		}
		transform.position = b;
		}
		yield return null;
	}

	IEnumerator RotateMe(Vector3 byAngles, float inTime){
	    var fromAngle = transform.rotation;
	    var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for(var t = 0f; t <= 1; t += Time.deltaTime/inTime) {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
		transform.rotation = toAngle;
		turning = false;
     }

	public void launchSucked(){
		 foreach(GameObject Obj in sucked){
			 Obj.SetActive(true);
			 Obj.transform.position = Vector3.Slerp(transform.position - dir + new Vector3(0,0.5f,0), transform.position - dir + new Vector3(0,0.5f,0),0.01f);
			sucked = new List<GameObject>();
		 }
		eject = true;
		lasttime = thistime;
	 }

	 void startStop(){
		 	stop = !stop;
			dir = transform.forward;
	 }
	 void returnHome(){
			Debug.Log("Going Back");
			turning = true;
			turn = false;
			StartCoroutine(RotateMe(Vector3.up * 180, 1f));
			dir = Quaternion.AngleAxis(180,Vector3.up) * dir;

			Speed = startSpeed*3;

			goingBack =true;
	 }
}
