using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public GameObject player;
    float time = 1, timer = 0;
    private Collider[] colls;
    public float radius, coneSpreadAngles;
    public int interactableLayer;
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
        transform.position = player.transform.position + player.transform.forward;
        transform.LookAt(player.transform.position + player.transform.forward * 2);
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
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
