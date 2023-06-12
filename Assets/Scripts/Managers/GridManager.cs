using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// This script manages the generation and management of the game grid
public class GridManager : MonoBehaviour
{
    // Instance of this script, used for accessing methods and variables from other scripts
    public static GridManager Instance;

    // Width and height of the game grid, set in the Unity editor
    [SerializeField] public int _width;
    [SerializeField] public int _height;

    // GameObject used as parent to all spawned tiles
    [SerializeField] private GameObject Tiles;

    // Floor and wall tiles to be used in the game grid, set in the Unity editor
    [SerializeField] private Tile _floorTile, _wallTile;

    
    // Camera object used to display the game grid
    [SerializeField] private Transform _cam;

    // Dictionary of tiles on the game grid, accessed by their Vector2 position
    public Dictionary<Vector2, Tile> _tiles { get; private set; }

    public Tile selectedTile;
    private IEnumerable<KeyValuePair<Tile, int>> darkTiles = new List<KeyValuePair<Tile, int>>();



    // Runs when the script is first initialized
    void Awake()
    {
        Instance = this;
    }

    // Generates the game grid
    public void GenerateGrid()
    {
        // Clears the existing tiles from the dictionary
        _tiles = new Dictionary<Vector2, Tile>();

        // Loops through each tile in the grid and randomly generates a floor or wall tile
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? _wallTile : _floorTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // Initializes the spawned tile with its position in the game grid
                spawnedTile.Init(x, y);

                // Adds the spawned tile to the dictionary of tiles
                _tiles[new Vector2(x, y)] = spawnedTile;

                // Add the spawned tile under the Tiles GameObject
                spawnedTile.transform.parent = Tiles.transform;
            }
        }

        // Sets the camera position to the center of the game grid
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        // Changes the game state to spawning the player character
        CombatManager.Instance.ChangeState(CombatState.SpawnCharacter);
    }

    // Returns a random tile from the left half of the game grid that is walkable
    public Tile GetCharacterSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    // Returns a random tile from the right half of the game grid that is walkable and reachable by the enemy
    public Tile GetEnemySpawnTile(Dictionary<Tile, int> distMatrix)
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable && distMatrix[t.Value] < int.MaxValue).OrderBy(t => Random.value).First().Value;
    }

    // Returns the tile at the specified position in the game grid, or null if no tile is found
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public  void ToggleDarkLightTiles(IEnumerable<KeyValuePair<Tile, int>> tiles)
    {
        ToggleOffDarkLightTiles();
        foreach (var tile in tiles)
        {
            tile.Key.DarkLight(true);
        }
        darkTiles = tiles;
    }

    public void ToggleOffDarkLightTiles()
    {
        foreach (var tile in darkTiles)
        {
            tile.Key.DarkLight(false);
        }
        selectedTile = null;
    }

    public IEnumerable<KeyValuePair<Tile, int>> ToggleDarkLightTiles(Func<KeyValuePair<Tile, int>, bool> discriminator, Tile startTile)
    {
        // Get the distance matrix for the current tile and show tiles that are within the character's speed range.
        var distMatrix = BFS.GetDistanceMatrix(startTile);
        var tiles = distMatrix.Where(discriminator);
        ToggleDarkLightTiles(tiles);
        return tiles;
    }
}