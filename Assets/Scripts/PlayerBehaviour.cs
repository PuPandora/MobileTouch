using UnityEngine;

/// <summary>
/// �÷��̾� ��ũ��Ʈ
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rigid;

    [Header("Basic Properties")]
    [Tooltip("���� ����/���������� �����̴� �ӵ�")]
    public float dodgeSpeed = 5.0f;

    [Tooltip("���� �ڵ����� ������ ���� �ӵ�")]
    [Range(0f, 10f)]
    public float rollSpeed = 5.0f;

    private float horizontalSpeed = 0f;

    [Header("Swipe Properties")]
    [Tooltip("�������� �� �÷��̾ �����̴� �Ÿ�")]
    public float swipeMove = 2f;

    [Tooltip("�׼��� �����ϱ� ���� �������� �ּ� �Ÿ� (1/4)")]
    public float minSwipeDistance = 0.25f;

    /// <summary>
    /// pixels�� ��ȯ�� minSwipeDistance ���� ����
    /// </summary>
    private float minSwipeDistancePixels;

    /// <summary>
    /// ����� ��ġ �̺�Ʈ�� ��ġ ������ ��ġ ����
    /// <br></br>
    /// �������� �Ÿ� ����� ���� ����
    /// </summary>
    private Vector2 touchStart;

    [Header("Scaling Properties")]
    [Tooltip("�÷��̾��� �ּ� ũ��")]
    public float minScale = 0.5f;

    [Tooltip("�÷��̾��� �ִ� ũ��")]
    public float maxScale = 3.0f;

    /// <summary>
    /// �÷��̾��� ���� ũ��
    /// </summary>
    private float currentScale = 1.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        // dpi = dots per inch
        // Screen.dpi = ȭ�� 1��ġ�� �ȼ��� ��
        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;
    }

    void FixedUpdate()
    {
        // Ű���� �Է�
        //horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        // ������, ��, ���� ������ ����
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        // ���콺 �Է�
        if (Input.GetMouseButton(0))
        {
            horizontalSpeed = CalculateMovement(Input.mousePosition);
        }

        // ����� ����̽� (iOS, Android)
#elif UNITY_IOS || UNITY_ANDROID
        // ��ġ �Է�
        if (Input.touchCount > 0)
        {
            // ù ��° ��ġ�� ����
            Touch touch = Input.touches[0];
            horizontalSpeed = CalculateMovement(touch.position);
            SwipeTeleport(touch);
            ScalePlayer();
        }
#endif

        rigid.AddForce(horizontalSpeed, 0, rollSpeed);
    }
    
    /// <summary>
    /// ��ġ�� �÷��̾ ���� �̵��ϴ� �Լ�
    /// </summary>
    /// <param name="pixelPos">�÷��̾ ��ġ/Ŭ���� ��ġ</param>
    /// <returns>x�࿡�� �̵��� ����</returns>
    private float CalculateMovement(Vector3 pixelPos)
    {
        // 0�� 1 �����Ϸ� ��ȯ
        var worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float xMove = 0;

        // ȭ�� ������ Ŭ��
        if (worldPos.x < 0.5f)
        {
            xMove = -1;
        }
        // ȭ�� ���� Ŭ��
        else
        {
            xMove = 1;
        }
        // horizontalSpeed ���� ����
        horizontalSpeed = xMove * dodgeSpeed;

        // ���ο� horizontalSpeed ���� ��ȯ
        return xMove * dodgeSpeed;
    }

    /// <summary>
    /// �������� �����̵� �Լ�
    /// </summary>
    /// <param name="touch">��ġ ��ǥ</param>
    private void SwipeTeleport(Touch touch)
    {
        // ��ġ�� ���۵ƴٸ�
        if (touch.phase == TouchPhase.Began)
        {
            // touchStart ����
            touchStart = touch.position;
        }
        // ��ġ�� �����ٸ�
        else if (touch.phase == TouchPhase.Ended)
        {
            // ��ġ�� ���� ���� ����
            Vector2 touchEnd = touch.position;

            // x�࿡�� ��ġ�� ���۰� �� ������ ���� ���
            float x = touchEnd.x - touchStart.x;

            // Swipe �Ÿ��� �����ϴٸ� �ڷ���Ʈ ���� ����
            if (Mathf.Abs(x) < minSwipeDistancePixels)
            {
                return;
            }

            Vector3 moveDirection;
            // x�࿡�� ������ �̵��� ��� �������� �̵�
            if (x < 0)
            {
                moveDirection = Vector3.left;
            }
            // ������ ������ �̵�
            else
            {
                moveDirection = Vector3.right;
            }

            RaycastHit hit;

            // �浹�Ǵ� ���� ���� ���� �̵�
            if (!rigid.SweepTest(rigid.position, out hit, swipeMove))
            {
                // �÷��̾� �̵�
                rigid.MovePosition(rigid.position + (moveDirection * swipeMove));
            }
        }
    }

    private void ScalePlayer()
    {
        // 2���� ��ġ�� ������ ���� ����
        if (Input.touchCount != 2)
        {
            return;
        }
        else
        {
            // ������ ��ġ�� ����
            Touch touch0 = Input.touches[0];
            Touch touch1 = Input.touches[1];

            // ���� �����ӿ��� �� ��ġ�� ���� Ž��
            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

            // �� �������� ��ġ ������ �Ÿ� ���
            float prevTouchDeltaMag = (touch0Prev - touch1Prev).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            // �� ������ ������ �Ÿ� ����
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ������ �ӵ��� ������� ��ȭ�� �����ϰ� ����
            float newScale = currentScale - (deltaMagnitudeDiff * Time.deltaTime);

            // ���ο� ũ�Ⱑ ������ ������ ����
            newScale = Mathf.Clamp(newScale, minScale, maxScale);

            // �÷��̾� ũ�� ������Ʈ
            transform.localScale = Vector3.one * newScale;

            // ���� �����ӿ� ���� ũ��� ����
            currentScale = newScale;
        }
    }
}
