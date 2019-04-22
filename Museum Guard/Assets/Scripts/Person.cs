using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Pathfinding pathfinder;
    public List<Node> currentPath;
    public float movementSpeed, rotationSpeed;
    public bool NEW = false;
    Vector3 viewDir = new Vector3(0, 0, 1);
    Rigidbody rb;
    private Transform spawnPoint, entrance;
    public GameObject testTarget;

    public enum myStates
    {
        idle,
        walking
    };
    public myStates state = myStates.idle;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinding>();
        rb = GetComponent<Rigidbody>();
        currentPath = new List<Node>();
        viewDir = transform.forward;
        Debug.Log("1: " + Vector3.Cross(new Vector3(1, 0, 0), new Vector3(0, 0, 1)));
        Debug.Log("2: " + Vector3.Cross(new Vector3(1, 0, 0), new Vector3(-1, 0, 0)));
        Debug.Log("3: " + Vector3.Cross(new Vector3(1, 0, 0), new Vector3(0, 0, -1)));
        Debug.Log("4: " + Vector3.Cross(new Vector3(1, 0, 0), new Vector3(1, 0, 0)));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case myStates.idle:
                break;
            case myStates.walking:
                if (currentPath.Count == 0) { state = myStates.idle; Debug.Log("Leaving walking"); break; }

                Vector3 pointVector = new Vector3(currentPath[0].worldPosition.x - transform.position.x, 0, currentPath[0].worldPosition.z - transform.position.z).normalized;
                Vector3 crossVector = Vector3.Cross(viewDir, pointVector);
                float angles = Mathf.Lerp(0, Vector3.Dot(viewDir, pointVector), 0.1f * rotationSpeed * Time.deltaTime);
                if (crossVector.y > 0.05f)
                {
                    //left = -
                    viewDir = (Quaternion.AngleAxis(Mathf.Lerp(0, 20, rotationSpeed * Time.deltaTime), Vector3.up) * viewDir).normalized;
                    transform.LookAt(transform.position + viewDir);
                }
                else if (crossVector.y < -0.05f)
                {
                    // right = +
                    viewDir = (Quaternion.AngleAxis(Mathf.Lerp(0, -20, rotationSpeed * Time.deltaTime), Vector3.up) * viewDir).normalized;
                    transform.LookAt(transform.position + viewDir);
                }

                transform.position += transform.forward * movementSpeed * Time.deltaTime;



                //Find the distance between the next point and the agent
                float dist = Vector3.Distance(transform.position, new Vector3(currentPath[0].worldPosition.x, transform.position.y, currentPath[0].worldPosition.z));
                //if the distance is small enough, delete the current focus point from the array.
                if (dist < 0.5f)
                {
                    Debug.Log("Removing current path");
                    currentPath.Remove(currentPath[0]);
                }
                break;
        }
        if (NEW)
        {
            walkTo(testTarget.transform);
            NEW = false;
        }
    }

    public void walkTo(Transform target)
    {
        currentPath = pathfinder.FindPath(transform.position, target.position);
        state = myStates.walking;
    }
    public void walkTo(Transform from, Transform target)
    {
        currentPath = pathfinder.FindPath(from.position, target.position);
        state = myStates.walking;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewDir);
    }
}
