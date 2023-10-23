using UnityEngine;

/// <summary>
/// 플레이어 스크립트
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rigid;
    
    [Tooltip("공이 왼쪽/오른쪽으로 움직이는 속도")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("공이 자동으로 앞으로 가는 속도")]
    [Range(0f, 10f)]
    public float rollSpeed = 5.0f;

    private float horizontalSpeed = 0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Keyboard Left, Right Move
        //horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // Move Left, Right Move
        if (Input.GetMouseButton(0))
        {
            // 0과 1 스케일로 변환
            var worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float xMove = 0;

            // 화면 오른쪽 클릭
            if (worldPos.x < 0.5f)
            {
                xMove = -1;
            }
            // 화면 왼쪽 클릭
            else
            {
                xMove = 1;
            }
            // horizontalSpeed 값을 수정
            horizontalSpeed = xMove * dodgeSpeed;
        }

        rigid.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}
