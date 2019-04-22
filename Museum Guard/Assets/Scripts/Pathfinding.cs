using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < openSet[i].hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }
            Debug.Log(grid.GetNeighbors(currentNode));
            foreach (Node Neighbor in grid.GetNeighbors(currentNode))
            {
                if (!Neighbor.walkable || closedSet.Contains(Neighbor))
                {
                    continue;
                }

                int costToNeighbor = currentNode.gCost + GetNodeDistance(currentNode, Neighbor);
                if (costToNeighbor < Neighbor.gCost || !openSet.Contains(Neighbor))
                {
                    Neighbor.gCost = costToNeighbor;
                    Neighbor.hCost = GetNodeDistance(Neighbor, targetNode);
                    Neighbor.parent = currentNode;

                    if (!openSet.Contains(Neighbor))
                        openSet.Add(Neighbor);
                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> list = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            list.Add(currentNode);
            currentNode = currentNode.parent;
        }
        list.Reverse();
        return list;
    }

    int GetNodeDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
