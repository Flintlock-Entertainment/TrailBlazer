using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS 
{
    static private (Dictionary<Tile, int>, Tile[]) BFSFunction(Tile start)
    {
        var tiles = GridManager.Instance._tiles;

        var distMatrix = new Dictionary<Tile, int>();
        int n = tiles.Count;
        bool[] visited = new bool[n];
        Tile[] prev = new Tile[n];

        for (int i = 0; i<n; i++)
        {
            visited[i] = false;
        }

        foreach(var _tile in tiles.Values)
        {
            distMatrix[_tile] = int.MaxValue;
        }
            
        Tile s = start;
        
        LinkedList<Tile> queue = new LinkedList<Tile>();
        distMatrix[s] = 0;
        prev[PosToInt(s.getPosition())] = null;
        

        queue.AddLast(s);
        while (queue.Count > 0) {
            s = queue.First.Value;
            visited[PosToInt(s.getPosition())] = true;
            queue.RemoveFirst();
            LinkedList<Tile> neighbors = new LinkedList<Tile>();
            Vector2 pos = s.getPosition();
            for (int i = -1; i< 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    tiles.TryGetValue(pos + new Vector2(i, j), out var t);
                    if (t != null)
                        neighbors.AddLast(t);
                }
            }
 
            foreach(var neighbor in neighbors)
            {
                int neighborPos = PosToInt(neighbor.getPosition());
                if (!visited[neighborPos] && neighbor._isWalkable) {
                    queue.AddLast(neighbor);
                    if (distMatrix[neighbor] > distMatrix[s] + 1)
                    {
                        distMatrix[neighbor] = distMatrix[s] + 1;
                        prev[neighborPos] = s;
                    }   
                }
            }
        }
        return (distMatrix, prev);
    }

    static public int GetDistanceBetween2Tiles(Tile start, Tile end)
    {
        var dists = BFSFunction(start).Item1;
        return dists[end];
    }

    static public Dictionary<Tile, int> GetDistanceMatrix(Tile start)
    {
        return BFSFunction(start).Item1;
    }

    static public LinkedList<Tile> GetShortestPath(Tile start, Tile end)
    {
        var prev = BFSFunction(start).Item2;
        LinkedList<Tile> path = new LinkedList<Tile>();
        Debug.Log("path");
        for (var p = prev[PosToInt(end.getPosition())]; p != null; p = prev[PosToInt(p.getPosition())])
        {
            Debug.Log(p);
            path.AddFirst(p);
        }
            
        return path;
    }
    static private int PosToInt(Vector2 pos)
    {
        return (int)(pos.x * GridManager.Instance._height + pos.y);
    }
}
