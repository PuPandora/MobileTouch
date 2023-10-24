using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenBehaviour : MonoBehaviour
{
    public static bool paused;

    [Tooltip("일시정지 메뉴 오브젝트")]
    public GameObject pauseMenu;

    void Start()
    {
        // 시작 초기화
        SetPauseMenu(false);
    }

    /// <summary>
    /// 현재 씬을 다시 로드하여 리셋하는 함수
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        // 이미 게임이 멈춰있으면 0. 아니라면 1
        Time.timeScale = (paused) ? 0 : 1;
        pauseMenu.SetActive(paused);
    }
}
