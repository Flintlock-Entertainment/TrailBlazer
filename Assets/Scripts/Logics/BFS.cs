using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS
{
    // This private method implements the Breadth-First Search algorithm and returns a tuple containing a dictionary of the distances
    // between the start tile and all other tiles in the grid, and an array of the previous tiles visited during the search.
    static private (Dictionary<Tile, int>, Tile[]) BFSFunction(Tile start)
    {
        // Get the dictionary of all tiles in the grid.
        var tiles = GridManager.Instance._tiles;
        // Create an empty dictionary to hold the distances between the start tile and all other tiles.
        var distMatrix = new Dictionary<Tile, int>();

        // Get the number of tiles in the grid.
        int n = tiles.Count;

        // Create an array to keep track of which tiles have been visited during the search.
        bool[] visited = new bool[n];

        // Create an array to keep track of the previous tile visited during the search.
        Tile[] prev = new Tile[n];

        // Set all tiles as not visited.
        for (int i = 0; i < n; i++)
        {
            visited[i] = false;
        }

        // Set the distance between the start tile and all other tiles to infinity.
        foreach (var _tile in tiles.Values)
        {
            distMatrix[_tile] = int.MaxValue;
        }

        // Set the start tile as the current tile.
        Tile s = start;

        // Create a queue to hold the tiles to be visited during the search.
        LinkedList<Tile> queue = new LinkedList<Tile>();

        // Set the distance between the start tile and itself to 0.
        distMatrix[s] = 0;

        // Set the previous tile of the start tile to null.
        prev[PosToInt(s.getPosition())] = null;

        // Add the start tile to the queue.
        queue.AddLast(s);

        // Perform the search until the queue is empty.
        while (queue.Count > 0)
        {
            // Get the first tile in the queue and mark it as visited.
            s = queue.First.Value;
            visited[PosToInt(s.getPosition())] = true;
            queue.RemoveFirst();

            // Get the neighbors of the current tile and add them to a new queue.
            LinkedList<Tile> neighbors = new LinkedList<Tile>();
            Vector2 pos = s.getPosition();
            for (int col = -1; col < 2; col++)
            {
                for (int row = -1; row < 2; row++)
                {
                    tiles.TryGetValue(pos + new Vector2(col, row), out var neighbor);
                    if (neighbor != null)
                        neighbors.AddLast(neighbor);
                }
            }

            // For each neighbor, update its distance and previous tile if necessary, and add it to the queue.
            foreach (var neighbor in neighbors)
            {
                int neighborPos = PosToInt(neighbor.getPosition());
                if (!visited[neighborPos] && neighbor._isWalkable)
                {
                    queue.AddLast(neighbor);
                    if (distMatrix[neighbor] > distMatrix[s] + 1)
                    {
                        distMatrix[neighbor] = distMatrix[s] + 1;
                        prev[neighborPos] = s;
                    }
                }
            }
        }

        // Return the dictionary of distances and the array of previous tiles.
        return (distMatrix, prev);
    }

    // This public method returns the distance between two tiles using the Breadth-First Search algorithm.
    static public int GetDistanceBetween2Tiles(Tile start, Tile end)
    {
        var dists = BFSFunction(start).Item1; // Get the distance matrix from BFSFunction for the start tile
        return dists[end]; // Return the distance between start and end tile from the matrix
    }

    static public Dictionary<Tile, int> GetDistanceMatrix(Tile start)
    {
        return BFSFunction(start).Item1; // Return the distance matrix from BFSFunction for the start tile
    }

    static public LinkedList<Tile> GetShortestPath(Tile start, Tile end)
    {
        var prev = BFSFunction(start).Item2; // Get the previous node mapping from BFSFunction for the start tile
        LinkedList<Tile> path = new LinkedList<Tile>(); // Create a new linked list to hold the shortest path
        Debug.Log("path");
        for (var p = prev[PosToInt(end.getPosition())]; p != null; p = prev[PosToInt(p.getPosition())]) // Traverse the previous node mapping from end to start tile
        {
            Debug.Log(p); // Log the current node to debug the path
            path.AddFirst(p); // Add the current node to the front of the path list
        }
        return path; // Return the shortest path list
    }

    static private int PosToInt(Vector2 pos)
    {
        return (int)(pos.x * GridManager.Instance._height + pos.y); // Convert the 2D position to an integer index based on the grid height
    }
}


/*
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
*/