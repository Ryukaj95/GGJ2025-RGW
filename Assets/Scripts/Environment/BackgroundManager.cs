using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;

    private SpriteRenderer spriteRenderer;
    private float backgroundSize;

    private GameObject currentBackground;

    private bool isBackground1OnTop = true;

    protected override void Awake()
    {
        base.Awake();

        SpriteRenderer spriteRenderer = background1.GetComponent<SpriteRenderer>();
        backgroundSize = spriteRenderer.sprite.bounds.size.y;
        currentBackground = background1;
    }

    private void Update()
    {
        background1.transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime, 0);
        background2.transform.position += new Vector3(0, -scrollSpeed * Time.deltaTime, 0);
    }

    private void PutScolledBackgroundOnTopOfOther(GameObject background)
    {

        if (isBackground1OnTop)
        {
            isBackground1OnTop = false;
            background1.transform.position = new Vector3(
                background2.transform.position.x,
                background2.transform.position.y + backgroundSize,
                background2.transform.position.z
            );
        }
        else
        {
            background2.transform.position = new Vector3(
                background1.transform.position.x,
                background1.transform.position.y + backgroundSize,
                background1.transform.position.z
            );
            isBackground1OnTop = true;
        }
    }

    public void HandleScrolledBackground(GameObject background)
    {
        PutScolledBackgroundOnTopOfOther(background);
    }

    public void UpdateBackground(Sprite newSprite)
    {
        background1.GetComponent<SpriteRenderer>().sprite = newSprite;
        background2.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
