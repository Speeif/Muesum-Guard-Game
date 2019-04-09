using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hush : MonoBehaviour
{
    public GameObject player, currAttack, HushBox;
    public float time, timer; 
    public float radius, distMod;
    private Collider[] colls;
    bool timed;
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
        if (Input.GetKeyDown("t") && !timed)
        {
            timed = true;
            time = 0;
            GameObject hush = GameObject.Instantiate(HushBox, transform.position + transform.forward, transform.rotation);
            hush.GetComponent<HushAttack>().enabled = true;

            colls = Physics.OverlapBox(transform.position + transform.forward * radius, Vector3.one *radius, transform.rotation, interactableLayer + 1 << 8);

            for (int i = 0; i < colls.Length; i++)
            {
                Vector3 temp = colls[i].gameObject.transform.position - transform.position;
                Vector3 distance = transform.forward * radius;
            if (temp.magnitude < distance.magnitude + distMod)
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
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * radius, Vector3.one * radius);
    }
}
