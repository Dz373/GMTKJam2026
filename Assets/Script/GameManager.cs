using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public PlayerController player;
    public CursorController cursor;
    public UIManager ui;
    public Tilemap overlay;
    public Tilemap map;
    public Tilemap objectMap;

    [Header("Misc Objects")]
    [SerializeField] private Tile greenOverlay;

    [SerializeField] private List<TileData> tileDataList;
    private Dictionary<TileBase, TileData> tileData;

    private Vector2[] directions = {Vector2.right, Vector2.up, Vector2.left, Vector2.down };

    private void Awake() {
        tileData = new Dictionary<TileBase, TileData>();

        foreach (var data in tileDataList) {
            foreach (var tile in data.tiles) {
                tileData.Add(tile, data);
            }
        }
    }

    private void Start() {
        foreach (Vector2 v in GetMoveTiles()) {
            overlay.SetTile(Vec2ToVec3(v), greenOverlay);
        }
    }

    private List<Vector2> GetMoveTiles() {
        Queue<Vector2> queue = new Queue<Vector2>();
        Dictionary<Vector2, int> tiles = new Dictionary<Vector2, int>();

        queue.Enqueue(player.pos);
        tiles.Add(player.pos, player.mv_range);

        while (queue.Count > 0) {
            Vector2 cur_tile = queue.Dequeue();
            int cur_mv = tiles[cur_tile];

            foreach (Vector2 dir in directions) {
                Vector2 new_tile = cur_tile + dir;

                if (!IsValidTile(new_tile))
                    continue;

                int mv_cost = GetMoveCost(new_tile);
                if (cur_mv < mv_cost)
                    continue;

                if (tiles.ContainsKey(new_tile)) {
                    if (cur_mv - mv_cost > tiles[new_tile]) {
                        tiles[new_tile] = cur_mv - mv_cost;
                        queue.Enqueue(new_tile);
                    }
                }
                else {
                    queue.Enqueue(new_tile);
                    tiles.Add(new_tile, cur_mv - mv_cost);
                }
            }
        }

        return new List<Vector2>(tiles.Keys);
    }

    private bool IsValidTile(Vector2 v) {
        Vector3Int tile = Vec2ToVec3(v);

        if (!map.HasTile(tile))
            return false;

        if (objectMap.HasTile(tile))
            if (tileData[map.GetTile(tile)].no_pass)
                return false;
        
        if (tileData[map.GetTile(tile)].no_pass)
            return false;

        return true;
    }

    private int GetMoveCost(Vector2 v) {
        Vector3Int tile = Vec2ToVec3(v);

        return tileData[map.GetTile(tile)].mv_cost;
    }

    private Vector3Int Vec2ToVec3(Vector2 v) {
        return new Vector3Int((int)v.x, (int)v.y, 0);
    }
}
