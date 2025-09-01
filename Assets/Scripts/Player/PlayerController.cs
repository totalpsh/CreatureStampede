using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // 한국어 주석
    Rigidbody2D _rigidbody;

    SpriteRenderer spriteRenderer; // 캐릭터 스프라이트 렌더러


    [SerializeField] private float moveSpeed = 5f; // 이동 속도

    Vector2 movementDirection = Vector2.zero; // 이동 방향 벡터
    public Vector2 MovementDirection
    {
        get { return movementDirection; }
        set
        {
            movementDirection = value;
            if (movementDirection.x != 0) // 좌우 이동 방향에 따라 스프라이트 뒤집기
            {
                spriteRenderer.flipX = movementDirection.x < 0;
            }
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movment(MovementDirection);
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * moveSpeed;

        _rigidbody.velocity = direction;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MovementDirection = context.ReadValue<Vector2>();
            MovementDirection = MovementDirection.normalized;

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MovementDirection = Vector2.zero;
        }
    }
}

