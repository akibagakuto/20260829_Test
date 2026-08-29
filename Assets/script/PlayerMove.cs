using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 720.0f;
    public float jumpForce = 5.0f;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        float moveX = 0.0f;
        float moveZ = 0.0f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1.0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1.0f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveZ = 1.0f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveZ = -1.0f;

        movementInput = new Vector3(moveX, 0.0f, moveZ);

        if (movementInput.magnitude > 1.0f)
        {
            movementInput.Normalize();
        }

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // 物理演算の処理は FixedUpdate で行うことで、衝突時の挙動が圧倒的に安定します
    void FixedUpdate()
    {
        // 1. 移動速度の設定 (X軸、Z軸のみ物理的に動かす)
        Vector3 velocity = movementInput * moveSpeed;
        velocity.y = rb.linearVelocity.y; // 古いUnityの場合は rb.velocity.y
        rb.linearVelocity = velocity;      // 古いUnityの場合は rb.velocity = velocity;

        // 2. 進行方向への振り向き処理
        if (movementInput.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // 3. 【最重要】何かにぶつかっても横（X軸・Z軸）に絶対に倒れないように角度を強制ロック
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0.0f, currentEuler.y, 0.0f);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
