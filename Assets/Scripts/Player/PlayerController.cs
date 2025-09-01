using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // �ѱ��� �ּ�
    Rigidbody2D _rigidbody;

    SpriteRenderer spriteRenderer; // ĳ���� ��������Ʈ ������


    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�

    Vector2 movementDirection = Vector2.zero; // �̵� ���� ����
    public Vector2 MovementDirection
    {
        get { return movementDirection; }
        set
        {
            movementDirection = value;
            if (movementDirection.x != 0) // �¿� �̵� ���⿡ ���� ��������Ʈ ������
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

