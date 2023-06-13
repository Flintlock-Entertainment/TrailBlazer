using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is a base class for all tiles in the game.
It contains various properties and methods that are common to all tiles.
*/
public abstract class Tile : MonoBehaviour
{
    // Name of the tile.
    public string TileName;

    // Reference to the tile's sprite renderer.
    [SerializeField] protected SpriteRenderer _renderer;

    // Reference to the tile's highlight and darklight objects.
    [SerializeField] private GameObject _highlight, _darklight;

    // Indicates whether the tile is walkable.
    [SerializeField] public bool _isWalkable { get; protected set; }

    // Reference to the unit currently occupying the tile.
    public BaseUnit OccupiedUnit;

    // Indicates whether the tile is both walkable and unoccupied by any unit.
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    // Initializes the tile's properties.
    public virtual void Init(int x, int y)
    {
        // Code to initialize the tile goes here.
    }

    // Highlights the tile when the mouse enters its collider.
    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        if (OccupiedUnit != null)
            MenuManager.Instance.ShowInformation(OccupiedUnit.unitData.GetDescription());
    }

    // Disables the tile's highlight when the mouse exits its collider.
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    // Activates/deactivates the tile's darklight object.
    public void DarkLight(bool active)
    {
        _darklight.SetActive(active);
    }

    // Handles left mouse button down events.
    void OnMouseDown()
    {
        // Only allow player input during their turn.
        if (!CombatManager.Instance.isPlayersTurn()) return;

        // If the tile is currently darkened, select it.
        if (_darklight.activeSelf)
        {
            GridManager.Instance.selectedTile = this;
        }
    }

    // Sets the tile's OccupiedUnit property and updates the unit's position.
    public void SetUnit(BaseUnit unit)
    {
        if (!Walkable) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    // Returns the tile's position as a Vector2.
    public Vector2 getPosition()
    {
        return transform.position;
    }
}