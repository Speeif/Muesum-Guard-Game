using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 lookPos, center, playerposition;
    public GameObject Player;
    public GameObject[] interests;
    public float centering;

    void Start()
    {
        playerposition = Player.transform.position;
        transform.position = new Vector3(playerposition.x, 20, playerposition.z-20);

        transform.LookAt(Player.transform.position);
        centering = 0.1f;
    }

    void FixedUpdate(){

        transform.position = new Vector3(Player.transform.position.x,20,Player.transform.position.z -20);

        center = Vector3.zero;

        center += Player.transform.position;
        int j = 1;
        foreach(var obj in interests){
            float dist = Vector3.Distance(obj.transform.position, Player.transform.position);

            if(dist < 6){
                j++;
                center += obj.transform.position;
            }
        }

        center /= j;

        lookPos = Vector3.Lerp(lookPos, center, 0.5f * Time.deltaTime*8);
        
        transform.LookAt(lookPos);
    }
}
