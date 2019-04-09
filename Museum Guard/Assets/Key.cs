using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject player, currAttack, Keyattack;
    public float time, timer;
    bool timed;

    private Collider[] colls;
    public float radius, force;
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
        if (Input.GetKeyDown("r") && !timed){
            timed = true;
            time = 0;
            GameObject keys = GameObject.Instantiate(Keyattack, transform.position, transform.rotation);
            keys.GetComponent<Keyring>().enabled = true;

            colls = Physics.OverlapSphere(transform.position, radius, interactableLayer + 1 << 8);

            for(int i = 0; i < colls.Length; i++){
                Vector3 temp = colls[i].gameObject.transform.position - transform.position;
                temp = temp.normalized;
                temp.y = 0.25F;
                colls[i].gameObject.GetComponent<Rigidbody>().velocity = ((temp)*force);

                Debug.Log("I hit" + colls[i].gameObject.name);
                
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
