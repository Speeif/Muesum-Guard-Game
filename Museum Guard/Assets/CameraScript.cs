using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 lookPos, center, playerposition;
    public GameObject Player;
    public GameObject[] interests;
    public float centering;
    private float exp;
    private int j, oldj;

    public int LayerForInterests;



    public float FixationDistance;

    void Start()
    {
        playerposition = Player.transform.position;
        transform.position = new Vector3(playerposition.x, 20, playerposition.z - 20);

        transform.LookAt(Player.transform.position);
        centering = 0.1f;
    }

    void FixedUpdate()
    {
        Collider[] ints = Physics.OverlapSphere(Player.transform.position, FixationDistance, 1 << 8 + LayerForInterests);
        foreach (Collider coll in ints)
        {

        }

        transform.position = new Vector3(Player.transform.position.x, 20, Player.transform.position.z - 20);

        center = Vector3.zero;

        j = 0;
        foreach (var obj in ints)
        {
            float dist = Vector3.Distance(obj.gameObject.transform.position, Player.transform.position);

            if (dist < FixationDistance)
            {
                j++;
                center += obj.transform.position;
            }
        }

        if (j != oldj)
        {
            exp = 0;
            oldj = j;
        }

        center /= j;
        if (j == 0)
        {
            lookPos = Vector3.Lerp(lookPos, Player.transform.position, 0.7f * exp);
            exp += Time.deltaTime;
        }
        else
        {
            lookPos = Vector3.Lerp(lookPos, center, 0.5f * exp);
            exp += Time.deltaTime;
        }
        transform.LookAt(lookPos);
    }
}
