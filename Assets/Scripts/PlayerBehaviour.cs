using UnityEngine;

/// <summary>
/// �÷��̾� ��ũ��Ʈ
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rigid;
    
    [Tooltip("���� ����/���������� �����̴� �ӵ�")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("���� �ڵ����� ������ ���� �ӵ�")]
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
