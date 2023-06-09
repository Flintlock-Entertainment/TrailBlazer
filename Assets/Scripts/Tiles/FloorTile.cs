using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(int x, int y)
    {
        _isWalkable = true;
        //A calculation to create a checkboard with altermanting colors.
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
