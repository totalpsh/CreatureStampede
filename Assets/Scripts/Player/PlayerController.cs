using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    SpriteRenderer spriteRenderer; // 캐릭터 스프라이트 렌더러
    PlayerAnimator playerAnimator;

    [SerializeField] private float moveSpeed = 5f; // 이동 속도

    // 대시 관련 변수
    [Header("Dash Settings")]
    public float dashSpeed = 20f;     // 대시 속도
    [Range(0.1f, 2.0f)] public float dashDuration = 0.2f; // 대시 지속 시간
    public float dashCooldown = 1f;   // 대시 쿨타임


    private bool isDashing = false;      // 현재 대시 중인지 확인
    private bool canDash = true;         // 대시 사용 가능 여부 확인

    private Vector2 dashDirection; // 대시 방향 벡터
    Vector2 movementDirection = Vector2.zero; // 이동 방향 벡터
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
                if (movementDirection.x != 0) // 좌우 이동 방향에 따라 스프라이트 뒤집기
                {
                    spriteRenderer.flipX = movementDirection.x < 0;
                }
            }
        }
    }

    // 대시 쿨타입 연결할 이벤트 액션
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

        // 대시 방향 설정 (현재 이동 방향 또는 바라보는 방향)
        Vector2 inputDir = MovementDirection;
        if (inputDir != Vector2.zero)
        {
            dashDirection = inputDir.normalized;
        }
        else
        {
            // 멈춰있을 경우, 마지막 이동 방향으로 대시
            dashDirection = LastMovementDirection;
        }
        // 대시 동안 무적
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        // 대시 속도 적용
        _rigidbody.velocity = dashDirection * dashSpeed;
        Debug.Log("Dash Input Received");
        // 대시 지속 시간만큼 대기
        yield return new WaitForSeconds(dashDuration);
        // 대시 실행 후 쿨타입UI 반영을 위한 이벤트 실행
        dashAction?.Invoke();
        // 대시 종료
        isDashing = false;
        _rigidbody.velocity = Vector2.zero;

        // 무적 해제
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        // 쿨타임 적용
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

