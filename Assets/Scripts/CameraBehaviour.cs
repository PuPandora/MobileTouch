using UnityEngine;

/// <summary>
/// 카메라가 목표물을 따라다니는 스크립트
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("카메라가 따라갈 목표")]
    public Transform target;

    [Tooltip("카메라와 목표 사이의 거리")]
    public Vector3 offset = new Vector3(0, 3, -6);

    void Update()
    {
        // 타겟이 있을 때
        if (target != null)
        {
            // 목표와 정해진 거리를 두고 카메라 위치를 조정
            transform.position = target.position + offset;
            // 목표물을 바라보도록 회전 값 조정
            transform.LookAt(target);
        }
    }
}
