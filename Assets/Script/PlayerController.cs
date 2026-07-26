using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public int power = 1;
    public int mv_range = 3;
    public int actions = 3;

    public Vector3Int pos;

    [SerializeField] private PathFollower pathFollow;

    public void Move(List<Vector3Int> path, Vector3Int target) {
        pathFollow.SetNewPath(path);
        pos = target;
    }
}
