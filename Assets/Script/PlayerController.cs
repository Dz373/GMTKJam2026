using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int power = 1;
    public int mv_range = 3;
    public int actions = 3;

    public Vector3Int pos;

    public void Move(Vector3Int v) {
        pos = v;
        transform.position = v;
    }
}
