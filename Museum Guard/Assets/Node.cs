using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> adjecantNodes;
    public bool isPOI;
    // Start is called before the first frame update
    public Node getNeighbor(int i)
    {
        return adjecantNodes[i];
    }
}
