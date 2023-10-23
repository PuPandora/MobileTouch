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

    void FixedUpdate()
    {
        // Keyboard Left, Right Move
        //horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // Move Left, Right Move
        if (Input.GetMouseButton(0))
        {
            // 0�� 1 �����Ϸ� ��ȯ
            var worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float xMove = 0;

            // ȭ�� ������ Ŭ��
            if (worldPos.x < 0.5f)
            {
                xMove = -1;
            }
            // ȭ�� ���� Ŭ��
            else
            {
                xMove = 1;
            }
            // horizontalSpeed ���� ����
            horizontalSpeed = xMove * dodgeSpeed;
        }

        rigid.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}
