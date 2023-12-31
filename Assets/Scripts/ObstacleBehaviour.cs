using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 장애물 스크립트
/// </summary>
public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("게임을 다시 시작하기까지 대기하는 시간")]
    public float waitTime = 2.0f;

    public GameObject explosion;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 PlayerBehaviour 컴포넌트를 가지고 있다면
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // Player 제거
            Destroy(collision.gameObject);

            // waitTime이 경과한 뒤 ResetGame 함수 호출
            Invoke(nameof(ResetGame), waitTime);
        }
    }

    /// <summary>
    /// 현재 로드된 레벨을 다시 시작
    /// </summary>
    private void ResetGame()
    {
        // 현재 레벨을 다시 시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 오브젝트를 터치하면 폭발이 발생하고 이 물체를 파괴합니다
    /// </summary>
    private void PlayerTouch()
    {
        if (explosion != null)
        {
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }
        Destroy(this.gameObject);
    }
}
