using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    SpriteRenderer spriteRenderer; // ĳ���� ��������Ʈ ������
    PlayerAnimator playerAnimator;

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

    // ��� ��Ÿ�� ������ �̺�Ʈ �׼�
    public event Action dashAction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void DataInitialization(float moveSpeed,float dashSpeed, float dashDuration, float dashCooldown)
    {
        this.moveSpeed = moveSpeed;
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
        this.dashCooldown = dashCooldown;
    }

    // 활용 좋다 -> 왜 써야하는가에 대해는 파악하고 있는지??
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
            playerAnimator.SetMoveAnimation(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MovementDirection = Vector2.zero;
            playerAnimator.SetMoveAnimation(false);
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
        // ��� ���� ����
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        // ��� �ӵ� ����
        _rigidbody.velocity = dashDirection * dashSpeed;

        // ��� ���� �ð���ŭ ���
        yield return new WaitForSeconds(dashDuration);
        // ��� ���� �� ��Ÿ��UI �ݿ��� ���� �̺�Ʈ ����
        // 이름이 대시 시작시인지, 종료시인지 모호함
        // 보통 시작으로 인식라는 경우가 많음
        dashAction?.Invoke();
        // ��� ����
        isDashing = false;
        _rigidbody.velocity = Vector2.zero;

        // ���� ����
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        // ��Ÿ�� ����
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // �����ٵ� �ӵ� 0����
    public void StopMovement()
    {
        _rigidbody.velocity = Vector2.zero;
        MovementDirection = Vector2.zero;
    }
}

