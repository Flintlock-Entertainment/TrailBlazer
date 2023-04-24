using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight, _darklight;
    [SerializeField] public bool _isWalkable { get; protected set; }

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;


    public virtual void Init(int x, int y)
    {
      
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        //MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        //MenuManager.Instance.ShowTileInfo(null);
    }

    public void DarkLight(bool active)
    {
        _darklight.SetActive(active);
    }
   
    void OnMouseDown() {
        if(GameManager.Instance.GameState != GameState.PlayersTurn) return;
        if (_darklight.activeSelf)
        {
            UnitManager.Instance.Character.selectedTile = this;
        }
    }

    public void SetUnit(BaseUnit unit) {
        if (!this._isWalkable) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
        
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }
}