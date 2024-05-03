using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarManager : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public static AStarManager instance;
    public Tilemap floorMap;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;
    }

    public void Init()
    {
        PopulateNodes();
    }

    public void PopulateNodes()
    {
        for(int i = floorMap.cellBounds.xMin; i < floorMap.cellBounds.xMax; i++)
        {
            for (int j = floorMap.cellBounds.yMin; j < floorMap.cellBounds.yMax; j++)
            {
                Vector3Int localPos = new Vector3Int(i, j, (int)floorMap.transform.position.y);
                Vector3 pos = floorMap.CellToWorld(localPos);

                if (floorMap.HasTile(localPos))
                {
                    pos.x += 0.5f;
                    pos.y += 0.5f;

                    Node node = new Node(pos);
                    nodes.Add(node);
                }
            }
        }

        CreateConnections();
    }

    public void CreateConnections()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                if (Vector2.Distance(nodes[i].transform, nodes[j].transform) <= 1f)
                {
                    AddConnection(nodes[i], nodes[j]);
                    AddConnection(nodes[j], nodes[i]);
                }
            }
        }
    }

    public void AddConnection(Node from, Node to)
    {
        from.connectedNodes.Add(to);
    }

    public List<Node> GeneratePath(Node start, Node end)
    {
        List<Node> openSet = new List<Node>();

        foreach(Node n in nodes)
        {
            n.gScore = float.MaxValue;
        }
        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform, end.transform);
        openSet.Add(start);
        
        while(openSet.Count > 0)
        {
            int lowestF = default;

            for(int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore())
                {
                    lowestF = i;
                }
            }

            Node currentNode = openSet[lowestF];

            openSet.Remove(currentNode);

            if(currentNode == end)
            {
                List<Node> path = new List<Node>();
                path.Insert(0, end);

                currentNode = end;
                while(currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }
                return path;
            }

            foreach(Node connectedNode in currentNode.connectedNodes)
            {
                float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform, connectedNode.transform);
                if(heldGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform, end.transform);
                    if(!openSet.Contains(connectedNode))
                        openSet.Add(connectedNode);
                }
            }
        }

        return null;
    }

    public Node NearestNode(Vector2 pos)
    {
        Node nearest = null;
        float nearestDist = float.MaxValue;

        foreach(Node n in nodes)
        {
            float curDist = Vector2.Distance(pos, n.transform);
            if(curDist < nearestDist)
            {
                nearestDist = curDist;
                nearest = n;
            }
        }

        return nearest;
    }
}

public class Node
{
    public Vector3 transform;
    public float gScore;
    public float hScore;
    public Node cameFrom;
    public List<Node> connectedNodes = new List<Node>();

    public Node(Vector3 trans)
    {
        this.transform = trans;
    }

    public float FScore()
    {
        return gScore + hScore;
    }
}
