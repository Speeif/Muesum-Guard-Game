using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hush : MonoBehaviour
{   public GameObject player, currAttack, hushAttack;
    public float time, timer;
    bool timed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !timed)
        {
            timed = true;
            time = 0;
            GameObject attack = GameObject.Instantiate(hushAttack,transform.position + transform.forward,transform.rotation);
            attack.GetComponent<hushAttack>().enabled = true;

        }
        if (time < timer)
        {
            time += Time.deltaTime;
        }
        else if (timed)
        {
            timed = false;
        }

    }
}
