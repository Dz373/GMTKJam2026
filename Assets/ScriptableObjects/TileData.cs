using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public int mv_cost;
    public bool no_pass;
    public bool can_interact;
}
