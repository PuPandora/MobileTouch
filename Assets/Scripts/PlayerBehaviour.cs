using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// A reference to the Rigidbody component.
    /// </summary>
    private Rigidbody rigid;

    [Tooltip("���� ����/���������� �����̴� �ӵ�")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("���� �ڵ����� ������ ���� �ӵ�")]
    [Range(0f, 10f)]
    public float rollSpeed = 5.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
        rigid.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}
