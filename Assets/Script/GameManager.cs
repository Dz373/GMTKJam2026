using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PlayerController player;
    [SerializeField] private CursorController cursor;
    [SerializeField] private UIManager ui;
    [SerializeField] private Tilemap overlay;
    [SerializeField] private Tilemap map;

    [Header("Misc Objects")]
    [SerializeField] private Tile greenOverlay;

    [SerializeField] private List<TileData> tileDataList;
    private Dictionary<TileBase, TileData> tileData;

    private void Awake() {
        tileData = new Dictionary<TileBase, TileData>();

        foreach (var data in tileDataList) {
            foreach (var tile in data.tiles) {
                tileData.Add(tile, data);
            }
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileBase tile = map.GetTile(map.WorldToCell(mousePosition));

            print(tileData[tile].mv_cost);
        }
    }
}
