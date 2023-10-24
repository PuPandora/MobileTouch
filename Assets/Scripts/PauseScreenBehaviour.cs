using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenBehaviour : MonoBehaviour
{
    public static bool paused;

    [Tooltip("�Ͻ����� �޴� ������Ʈ")]
    public GameObject pauseMenu;

    void Start()
    {
        // ���� �ʱ�ȭ
        SetPauseMenu(false);
    }

    /// <summary>
    /// ���� ���� �ٽ� �ε��Ͽ� �����ϴ� �Լ�
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        // �̹� ������ ���������� 0. �ƴ϶�� 1
        Time.timeScale = (paused) ? 0 : 1;
        pauseMenu.SetActive(paused);
    }
}
