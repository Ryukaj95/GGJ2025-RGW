using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;

    [SerializeField] private GameObject background1;

    [SerializeField] private GameObject background2;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float backgroundSize;

    [SerializeField] private float mileage;

    [SerializeField] private int lastModule = 0;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = background1.GetComponent<SpriteRenderer>();
        backgroundSize = spriteRenderer.sprite.bounds.size.y;
    }

    private void Update()
    {
        background1.transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime, 0);
        background2.transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime, 0);
    }
}
