using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Vector2 pos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.Set(Mathf.Floor(pos.x), Mathf.Floor(pos.y));

        transform.position = pos;
    }
}
