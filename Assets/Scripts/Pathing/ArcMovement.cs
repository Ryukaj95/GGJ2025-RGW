
using Unity.VisualScripting;
using UnityEngine;

public class ArcMovement : Movement
{
    public float height = 2f; // Altezza massima dell'arco

    public override Vector2 GetNextEndPoint(float step)
    {
        Vector2 startPos = startPoint;
        Vector2 endPos = endPoint;

        // Interpolazione lineare tra i punti di partenza e arrivo
        Vector2 currentPos = Vector3.Lerp(startPos, endPos, step);

        // Calcolare l'altezza dell'arco
        float curveHeight = Mathf.Sin(step * Mathf.PI) * height; // Sinusoidale per creare un arco
        currentPos.y += curveHeight;

        return currentPos;
    }
}
