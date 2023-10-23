using UnityEngine;

/// <summary>
/// 카메라가 목표물을 따라다니는 스크립트
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
