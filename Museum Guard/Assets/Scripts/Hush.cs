using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hush : MonoBehaviour
{
    public GameObject player, currAttack, hushAttack;
    public float time, timer;
    bool timed;

    private Collider[] colls;
    public float radius, coneSpreadAngles;
    public int interactableLayer;
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
            GameObject attack = GameObject.Instantiate(hushAttack, transform.position + transform.forward, transform.rotation);
            attack.GetComponent<hushAttack>().enabled = true;

            colls = Physics.OverlapSphere(transform.position, radius, interactableLayer + 1 << 8);

            for (int i = 0; i < colls.Length; i++)
            {
                Vector3 temp = colls[i].gameObject.transform.position - transform.position;
                float angle = Vector3.Dot(temp.normalized, transform.forward);
                if (angle > 1 - coneSpreadAngles)
                {
                    colls[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                }
            }

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
