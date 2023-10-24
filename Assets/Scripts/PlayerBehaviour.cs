using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어 스크립트
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rigid;

    [Header("Basic Properties")]
    [Tooltip("공이 왼쪽/오른쪽으로 움직이는 속도")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("공이 자동으로 앞으로 가는 속도")]
    [Range(0f, 10f)]
    public float rollSpeed = 5.0f;

    private float horizontalSpeed = 0f;

    [Header("Swipe Properties")]
    [Tooltip("스와이프 시 플레이어가 움직이는 거리")]
    public float swipeMove = 2f;

    [Tooltip("액션을 실행하기 위한 스와이프 최소 거리 (1/4)")]
    public float minSwipeDistance = 0.25f;

    /// <summary>
    /// pixels로 변환한 minSwipeDistance 값을 저장
    /// </summary>
    private float minSwipeDistancePixels;

    /// <summary>
    /// 모바일 터치 이벤트의 터치 시작한 위치 저장
    /// <br></br>
    /// 스와이프 거리 계산을 위한 변수
    /// </summary>
    private Vector2 touchStart;

    [Header("Scaling Properties")]
    [Tooltip("플레이어의 최소 크기")]
    public float minScale = 0.5f;

    [Tooltip("플레이어의 최대 크기")]
    public float maxScale = 3.0f;

    /// <summary>
    /// 플레이어의 현재 크기
    /// </summary>
    private float currentScale = 1.0f;

    // Accelerometer
    public enum MobileHorizMovement
    {
        Accelerometer,
        ScreenTouch
    }
    [Header("Accelerometer Property")]
    public MobileHorizMovement horizMovement = MobileHorizMovement.Accelerometer;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        // dpi = dots per inch
        // Screen.dpi = 화면 1인치당 픽셀의 수
        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;

#if UNITY_IOS || UNITY_ANDROID
        Application.targetFrameRate = 60;
#endif
    }

    void FixedUpdate()
    {
        // 정지 중이라면 FixedUpdate 비활성화
        if (PauseScreenBehaviour.paused)
        {
            return;
        }

        // 키보드 입력
        //horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // 에디터, 웹, 독립 실행형 빌드
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        // 마우스 입력
        if (Input.GetMouseButton(0))
        {
            horizontalSpeed = CalculateMovement(Input.mousePosition);
        }

        // 모바일 디바이스 (iOS, Android)
#elif UNITY_IOS || UNITY_ANDROID
        if (horizMovement == MobileHorizMovement.Accelerometer)
        {
            // 가속도 센서 이동 방식
            horizontalSpeed = Input.acceleration.x * dodgeSpeed;
        }

        // 터치 입력
        // 터치 입력이 하나 이상일 때만
        if (Input.touchCount > 0)
        {
            if (horizMovement == MobileHorizMovement.ScreenTouch)
            {
                // 첫 번째 터치를 감지
                Touch touch = Input.touches[0];
                horizontalSpeed = CalculateMovement(touch.position);
                SwipeTeleport(touch);
                ScalePlayer();
                TouchObjects(touch);
            }
        }
#endif

        rigid.AddForce(horizontalSpeed, 0, rollSpeed);

        // 떨어졌는지 체크
        if (rigid.position.y <= -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    /// <summary>
    /// 터치로 플레이어를 수평 이동하는 함수
    /// </summary>
    /// <param name="pixelPos">플레이어가 터치/클릭한 위치</param>
    /// <returns>x축에서 이동할 방향</returns>
    private float CalculateMovement(Vector3 pixelPos)
    {
        // 0과 1 스케일로 변환
        var worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float xMove = 0;

        // 화면 오른쪽 클릭
        if (worldPos.x < 0.5f)
        {
            xMove = -1;
        }
        // 화면 왼쪽 클릭
        else
        {
            xMove = 1;
        }
        // horizontalSpeed 값을 수정
        horizontalSpeed = xMove * dodgeSpeed;

        // 새로운 horizontalSpeed 값을 반환
        return xMove * dodgeSpeed;
    }

    /// <summary>
    /// 모바일 스와이프 순간이동 함수
    /// </summary>
    /// <param name="touch">터치 좌표</param>
    private void SwipeTeleport(Touch touch)
    {
        // 터치가 시작됐다면
        if (touch.phase == TouchPhase.Began)
        {
            // touchStart 설정
            touchStart = touch.position;
        }
        // 터치가 끝났다면
        else if (touch.phase == TouchPhase.Ended)
        {
            // 터치가 끝난 지점 저장
            Vector2 touchEnd = touch.position;

            // x축에서 터치의 시작과 끝 사이의 차이 계산
            float x = touchEnd.x - touchStart.x;

            // Swipe 거리가 부족하다면 텔레포트 하지 않음
            if (Mathf.Abs(x) < minSwipeDistancePixels)
            {
                return;
            }

            Vector3 moveDirection;
            // x축에서 음수로 이동한 경우 왼쪽으로 이동
            if (x < 0)
            {
                moveDirection = Vector3.left;
            }
            // 양수라면 오른쪽 이동
            else
            {
                moveDirection = Vector3.right;
            }

            RaycastHit hit;

            // 충돌되는 것이 없을 때만 이동
            if (!rigid.SweepTest(rigid.position, out hit, swipeMove))
            {
                // 플레이어 이동
                rigid.MovePosition(rigid.position + (moveDirection * swipeMove));
            }
        }
    }

    /// <summary>
    /// 모바일 핀치 크기 조절 함수
    /// </summary>
    private void ScalePlayer()
    {
        // 2개의 터치를 감지될 때만 실행
        if (Input.touchCount != 2)
        {
            return;
        }
        else
        {
            // 감지된 터치를 저장
            Touch touch0 = Input.touches[0];
            Touch touch1 = Input.touches[1];

            // 이전 프레임에서 각 터치의 위지 탐색
            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

            // 각 프레임의 터치 사이의 거리 계산
            float prevTouchDeltaMag = (touch0Prev - touch1Prev).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            // 각 프레임 사이의 거리 차이
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 프레임 속도에 관계없이 변화를 일정하게 유지
            float newScale = currentScale - (deltaMagnitudeDiff * Time.deltaTime);

            // 새로운 크기가 정해진 범위로 제한
            newScale = Mathf.Clamp(newScale, minScale, maxScale);

            // 플레이어 크기 업데이트
            transform.localScale = Vector3.one * newScale;

            // 다음 프레임에 현재 크기로 설정
            currentScale = newScale;
        }
    }

    /// <summary>
    /// 게임 오브젝트를 터치하고 있는지 확인
    /// <br></br>
    /// 터치하고 있다면 이벤트 호출
    /// </summary>
    /// <param name="touch"></param>
    private static void TouchObjects(Touch touch)
    {
        // 카메라가 바라보는 위치를 ray로 반환
        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

        RaycastHit hit;

        // 가능한 모든 채널과 충돌하는 LayerMask
        int layerMask = ~0;

        // Collider가 오브젝트를 터치하는 체크
        if (Physics.Raycast(touchRay, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            // 이 함수를 호출한 오브젝트가 PlayerTouch 함수가 있을 경우 호출
            hit.transform.SendMessage("PlayerTouch", SendMessageOptions.DontRequireReceiver);
        }
    }
}
