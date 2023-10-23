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

    void Update()
    {
        horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
    }

    void FixedUpdate()
    {
        rigid.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}
