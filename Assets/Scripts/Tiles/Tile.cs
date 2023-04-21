using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

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

    void OnMouseDown() {
        if(GameManager.Instance.GameState != GameState.PlayersTurn) return;
        for (int i = 0; i < 4; ++i)
        {


            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Character) UnitManager.Instance.SetSelectedCharacter((BaseCharacter)OccupiedUnit);
                else
                {
                    if (UnitManager.Instance.SelectedCharacter != null)
                    {
                        var enemy = (BaseEnemy)OccupiedUnit;
                        Destroy(enemy.gameObject);
                        UnitManager.Instance.SetSelectedCharacter(null);
                    }
                }
            }
            else
            {
                if (UnitManager.Instance.SelectedCharacter != null)
                {
                    SetUnit(UnitManager.Instance.SelectedCharacter);
                    UnitManager.Instance.SetSelectedCharacter(null);
                }
            }
        }
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);

    }

    public void SetUnit(BaseUnit unit) {
        if (!this._isWalkable) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}