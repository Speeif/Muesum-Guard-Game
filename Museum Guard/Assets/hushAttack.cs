using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hushAttack : MonoBehaviour
{

    public GameObject player;
    float time = 1, timer = 0;
    // Start is called before the first frame update
    void Start() { 
    }

    void OnEnabled()
    {
        timer = 0;
        time = 2;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + player.transform.forward;
        transform.LookAt(player.transform.position + player.transform.forward * 2);

        timer += Time.deltaTime;
        if(timer > time)
        {
            Destroy(gameObject);
        }
    }
}
