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

    [Tooltip("공이 왼쪽/오른쪽으로 움직이는 속도")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("공이 자동으로 앞으로 가는 속도")]
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
