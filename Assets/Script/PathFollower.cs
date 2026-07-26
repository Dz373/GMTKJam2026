using UnityEngine;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private List<Vector3Int> path;
    private int index = -1;

    void Update() {
        if (path == null || path.Count == 0 || index < 0)
            return;

        Vector2 target = new Vector2(path[index].x, path[index].y);

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if (Vector2.Distance(transform.position, target) < 0.01f)
            index--;
        
    }

    public void SetNewPath(List<Vector3Int> newPath) {
        path = newPath;
        index = newPath.Count-1;
    }
}
