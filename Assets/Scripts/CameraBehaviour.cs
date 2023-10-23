using UnityEngine;

/// <summary>
/// ī�޶� ��ǥ���� ����ٴϴ� ��ũ��Ʈ
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("ī�޶� ���� ��ǥ")]
    public Transform target;

    [Tooltip("ī�޶�� ��ǥ ������ �Ÿ�")]
    public Vector3 offset = new Vector3(0, 3, -6);

    void Update()
    {
        // Ÿ���� ���� ��
        if (target != null)
        {
            // ��ǥ�� ������ �Ÿ��� �ΰ� ī�޶� ��ġ�� ����
            transform.position = target.position + offset;
            // ��ǥ���� �ٶ󺸵��� ȸ�� �� ����
            transform.LookAt(target);
        }
    }
}
