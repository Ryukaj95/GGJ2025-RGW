using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;

    private void Update() {
        transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime, 0);
    }
}
