using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float startMoveSpeed = 4f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private float moveSpeed;

    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = startMoveSpeed;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
