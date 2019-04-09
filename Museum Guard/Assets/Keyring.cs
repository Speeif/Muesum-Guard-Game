using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyring : MonoBehaviour
{
    public GameObject player;
    float time = 1, timer = 0;
    public float radius;
    bool start = true;
    // Start is called before the first frame update
    void Start()
    { 
    }
    void OnEnabled() 
    {
        timer = 0;
        time = 2;
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;
        transform.LookAt(player.transform.position);
        start = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Debug.Log("Done with the shiiiit");
            start = false;
        }
        //Time since initiation
        timer += Time.deltaTime;

        //Destruction timer
        if (timer > time)
        {
            Destroy(gameObject);
        }
    }
}
