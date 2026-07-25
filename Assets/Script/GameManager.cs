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

    private Vector3Int[] directions = { Vector3Int.right, Vector3Int.up, Vector3Int.left, Vector3Int.down };
    private List<Vector3Int> movementTiles;

    private void Awake() {
        tileData = new Dictionary<TileBase, TileData>();

        foreach (var data in tileDataList) {
            foreach (var tile in data.tiles) {
                tileData.Add(tile, data);
            }
        }
    }

    private void Start() {
        DisplayOverlay();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            print("Tile Cost: " + GetMoveCost(cursor.pos));
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector3Int target = cursor.pos;

            if (movementTiles.Contains(target)) {
                player.Move(target);
                DisplayOverlay();
            }
        }
    }

    private List<Vector3Int> GetMoveTiles() {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, int> tiles = new Dictionary<Vector3Int, int>();

        queue.Enqueue(player.pos);
        tiles.Add(player.pos, player.mv_range);

        while (queue.Count > 0) {
            Vector3Int cur_tile = queue.Dequeue();
            int cur_mv = tiles[cur_tile];

            foreach (Vector3Int dir in directions) {
                Vector3Int new_tile = cur_tile + dir;

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

        return new List<Vector3Int>(tiles.Keys);
    }

    private bool IsValidTile(Vector3Int v) {
        if (!map.HasTile(v))
            return false;

        if (objectMap.HasTile(v))
            if (tileData[objectMap.GetTile(v)].no_pass)
                return false;
        
        if (tileData[map.GetTile(v)].no_pass)
            return false;

        return true;
    }

    private int GetMoveCost(Vector3Int v) {
        int cost = tileData[map.GetTile(v)].mv_cost;

        if (objectMap.HasTile(v))
            cost += tileData[objectMap.GetTile(v)].mv_cost;

        return cost;
    }

    private void DisplayOverlay() {
        movementTiles = GetMoveTiles();
        overlay.ClearAllTiles();
        foreach (Vector3Int v in movementTiles) {
            overlay.SetTile(v, greenOverlay);
        }
    }
}
