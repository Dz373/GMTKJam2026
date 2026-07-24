using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorController : MonoBehaviour
{
    public Vector3Int pos;
    private GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = gm.map.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        transform.position = pos;
    }
}
