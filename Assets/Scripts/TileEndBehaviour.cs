using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 타일 끝 콜라이더에 충돌했을 때의
/// <br></br>
/// 로직을 담당하는 스크립트
/// </summary>
public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("EndTile에 도달한 후 타일을 제거하기 전까지 대기하는 시간")]
    public float destroyTimer = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 PlayerBehaviour 컴포넌트를 가지고 있다면
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            // 씬에서 GameController 컴포넌트를 가진
            // 오브젝트의 GameController 스크립트에서 SpawnNextTile 메소드 호출
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            // 정해진 시간 뒤에 부모 오브젝트를 제거 (타일 제거)
            Destroy(transform.parent.gameObject, destroyTimer);
        }
    }
}
