using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ÿ���� ����/�����ϴ� ��ũ��Ʈ
/// </summary>
public class GameController : MonoBehaviour
{
    [Tooltip("�����ϰ����ϴ� Ÿ�� ����")]
    public Transform tile;

    [Tooltip("�����ϰ����ϴ� ��ֹ� ����")]
    public Transform obstacle;

    [Tooltip("ù�� ° Ÿ�� ���� ��ġ")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("ó���� �����Ǵ� Ÿ���� ����")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("���� Ÿ���� ������ ��ġ")]
    private Vector3 nextTileLocation;

    [Tooltip("���� Ÿ���� ȸ�� ��")]
    private Quaternion nextTileRotation;

    [Tooltip("��ֹ� ���� �����Ǵ� Ÿ���� ����")]
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
        // ��ֹ� ���� ����Ʈ�� �����ϴ� List
        var obstacleSpawnPoints = new List<GameObject>();

        // Ÿ�Ͽ� �ִ� �� ���� ���� ������Ʈ Ž��
        foreach (Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                // �±װ� �ִ� ��ġ�� Tile �߰�
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        // ��ֹ� ���� ����Ʈ�� 1 �̻�
        if (obstacleSpawnPoints.Count > 0)
        {
            // �������� ��ֹ� ���� ��ġ ����
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
            // ������ ��ġ ����
            var spawnPos = spawnPoint.transform.position;
            // ��ֹ� ����
            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);
            // ���� ������ Ÿ���� �θ�� ����
            newObstacle.SetParent(spawnPoint.transform);
        }
    }
}
