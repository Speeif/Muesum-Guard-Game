using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class person : MonoBehaviour
{
    public string state, lastState;

    public List<GameObject> POI;
    GameObject currPOI;
    // Start is called before the first frame update
    void Start()
    {
        state = "idle";
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "idle":
                //Animate fidgeting.
                lastState = state;
                break;
            case "mad":
                //Make pupUp box that says vulgarities
                //stand still.
                //Animate aggresiveness
                lastState = state;
                break;
            case "moving":
                //Move to a point of interest [GameObject]
                if (lastState != "moving")
                {
                    lastState = state;
                }


                break;
        }
    }

    public void setState(string newState)
    {
        state = newState;
    }
}
