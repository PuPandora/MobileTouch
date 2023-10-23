using UnityEngine;

/// <summary>
/// ī�޶� ��ǥ���� ����ٴϴ� ��ũ��Ʈ
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 3, -6);

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;

            transform.LookAt(target);
        }
    }
}
