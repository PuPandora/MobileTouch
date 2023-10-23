using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타일을 생성/관리하는 스크립트
/// </summary>
public class GameController : MonoBehaviour
{
    [Tooltip("생성하고자하는 타일 참조")]
    public Transform tile;

    [Tooltip("생성하고자하는 장애물 참조")]
    public Transform obstacle;

    [Tooltip("첫번 째 타일 생성 위치")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("처음에 생성되는 타일의 개수")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("다음 타일이 생성될 위치")]
    private Vector3 nextTileLocation;

    [Tooltip("다음 타일의 회전 값")]
    private Quaternion nextTileRotation;

    [Tooltip("장애물 없이 생성되는 타일의 갯수")]
    public int initNoObstacle = 4;

    void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; i++)
        {
            SpawnNextTile(i >= initNoObstacle);
        }
    }

    public void SpawnNextTile(bool spawnObstacle = true)
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);
        var nextTile = newTile.Find("Next Spawn Point");

        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if (spawnObstacle)
        {
            SpawnObstacle(newTile);
        }
    }

    private void SpawnObstacle(Transform newTile)
    {
        // 장애물 생성 포인트를 저장하는 List
        var obstacleSpawnPoints = new List<GameObject>();

        // 타일에 있는 각 하위 게임 오브젝트 탐색
        foreach (Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                // 태그가 있는 위치에 Tile 추가
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        // 장애물 생성 포인트가 1 이상
        if (obstacleSpawnPoints.Count > 0)
        {
            // 랜덤으로 장애물 생성 위치 설정
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
            // 생성할 위치 저장
            var spawnPos = spawnPoint.transform.position;
            // 장애물 생성
            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);
            // 새로 생성된 타일을 부모로 설정
            newObstacle.SetParent(spawnPoint.transform);
        }
    }
}
