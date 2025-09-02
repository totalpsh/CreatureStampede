using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    SpriteRenderer spriteRenderer; // ĳ���� ��������Ʈ ������


    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�

    // ��� ���� ����
    [Header("Dash Settings")]
    public float dashSpeed = 20f;     // ��� �ӵ�
    [Range(0.1f, 2.0f)] public float dashDuration = 0.2f; // ��� ���� �ð�
    public float dashCooldown = 1f;   // ��� ��Ÿ��


    private bool isDashing = false;      // ���� ��� ������ Ȯ��
    private bool canDash = true;         // ��� ��� ���� ���� Ȯ��

    private Vector2 dashDirection; // ��� ���� ����
    Vector2 movementDirection = Vector2.zero; // �̵� ���� ����
    public Vector2 LastMovementDirection { get; private set; } = Vector2.right;
    public Vector2 MovementDirection
    {
        get { return movementDirection; }
        set
        {
            movementDirection = value;
            if (movementDirection != Vector2.zero)
            {
                LastMovementDirection = movementDirection;
                if (movementDirection.x != 0) // �¿� �̵� ���⿡ ���� ��������Ʈ ������
                {
                    spriteRenderer.flipX = movementDirection.x < 0;
                }
            }
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void DataInitialization(float moveSpeed,float dashSpeed, float dashDuration, float dashCooldown)
    {
        this.moveSpeed = moveSpeed;
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
        this.dashCooldown = dashCooldown;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movment(MovementDirection);
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * moveSpeed;

        _rigidbody.velocity = direction;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isDashing)
        {
            MovementDirection = context.ReadValue<Vector2>().normalized;

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MovementDirection = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // ��� ���� ���� (���� �̵� ���� �Ǵ� �ٶ󺸴� ����)
        Vector2 inputDir = MovementDirection;
        if (inputDir != Vector2.zero)
        {
            dashDirection = inputDir.normalized;
        }
        else
        {
            // �������� ���, ������ �̵� �������� ���
            dashDirection = LastMovementDirection;
        }

        // ��� �ӵ� ����
        _rigidbody.velocity = dashDirection * dashSpeed;
        Debug.Log("Dash Input Received");
        // ��� ���� �ð���ŭ ���
        yield return new WaitForSeconds(dashDuration);
        // ��� ����
        isDashing = false;
        _rigidbody.velocity = Vector2.zero;

        // ��Ÿ�� ����
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

