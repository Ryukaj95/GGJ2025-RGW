using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public Vector2 startPoint;
    public Vector2 endPoint;

    public abstract Vector2 GetNextEndPoint(float step);
}
