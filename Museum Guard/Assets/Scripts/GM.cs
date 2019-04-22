using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    public List<GameObject> POI;
    // [HideInInspector]
    public List<GameObject> guests;
    public float amountOfGuests, spawnPointRadius;
    public List<GameObject> guestVariant;

    public GameObject spawnPoint;
    public Vector2 spawnArea;
    public Transform entrance;

    // Start is called before the first frame update

    public enum MyStates
    {
        opening,
        running,
        ending
    };
    public MyStates myState = MyStates.opening;
    void Start()
    {
        POI = new List<GameObject>();
        POI[] temp = FindObjectsOfType<POI>();
        for (int i = 0; i < temp.Length; i++)
        {
            POI.Add(temp[i].gameObject);
        }
        //Spawn guests and store them
        for (int i = 0; i < amountOfGuests; i++)
        {
            //find spawn point on the spawn area
            Vector3 currentSpawnPoint = new Vector3(spawnPoint.transform.position.x + Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                spawnPoint.transform.position.y + 1f,
                spawnPoint.transform.position.z + Random.Range(-spawnArea.y / 2, spawnArea.y / 2));

            int variant = Random.Range(0, guestVariant.Count);
            float size = Mathf.Max(guestVariant[variant].GetComponent<BoxCollider>().size.x, guestVariant[variant].GetComponent<BoxCollider>().size.z);
            if (Physics.CheckSphere(currentSpawnPoint, size, 1 << 8 + 10))
            {
                i--;
                continue;
            }

            GameObject currentGuest = Instantiate(guestVariant[variant], currentSpawnPoint, spawnPoint.transform.rotation);

            guests.Add(currentGuest);
            // currentGuest.GetComponent<Person>().walkTo(POI[Random.Range(0, POI.Count)].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case MyStates.opening:
                for (int i = 0; i < guests.Count; i++)
                {
                    guests[i].GetComponent<Person>().walkTo(entrance.transform, POI[0].transform);
                }
                myState = MyStates.running;
                break;
            case MyStates.running:
                break;
            case MyStates.ending:
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnPoint.transform.position.x, 1, spawnPoint.transform.position.z), new Vector3(spawnArea.x, 1, spawnArea.y));
    }
}
