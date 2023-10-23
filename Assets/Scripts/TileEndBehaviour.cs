using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ Ÿ�� �� �ݶ��̴��� �浹���� ����
/// <br></br>
/// ������ ����ϴ� ��ũ��Ʈ
/// </summary>
public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("EndTile�� ������ �� Ÿ���� �����ϱ� ������ ����ϴ� �ð�")]
    public float destroyTimer = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� PlayerBehaviour ������Ʈ�� ������ �ִٸ�
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            // ������ GameController ������Ʈ�� ����
            // ������Ʈ�� GameController ��ũ��Ʈ���� SpawnNextTile �޼ҵ� ȣ��
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            // ������ �ð� �ڿ� �θ� ������Ʈ�� ���� (Ÿ�� ����)
            Destroy(transform.parent.gameObject, destroyTimer);
        }
    }
}
