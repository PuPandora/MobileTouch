using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ֹ� ��ũ��Ʈ
/// </summary>
public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("������ �ٽ� �����ϱ���� ����ϴ� �ð�")]
    public float waitTime = 2.0f;

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� PlayerBehaviour ������Ʈ�� ������ �ִٸ�
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // Player ����
            Destroy(collision.gameObject);

            // waitTime�� ����� �� ResetGame �Լ� ȣ��
            Invoke(nameof(ResetGame), waitTime);
        }
    }

    /// <summary>
    /// ���� �ε�� ������ �ٽ� ����
    /// </summary>
    private void ResetGame()
    {
        // ���� ������ �ٽ� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
