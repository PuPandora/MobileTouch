using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("�����ϰ����ϴ� Ÿ�� ����")]
    public Transform tile;

    [Tooltip("ù�� ° Ÿ�� ���� ��ġ")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("ó���� �����Ǵ� Ÿ���� ����")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("���� Ÿ���� ������ ��ġ")]
    private Vector3 nextTileLocation;

    [Tooltip("���� Ÿ���� ȸ�� ��")]
    private Quaternion nextTileRotation;

    void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; i++)
        {
            SpawnNextTile();
        }
    }

    public void SpawnNextTile()
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);
        var nextTile = newTile.Find("Next Spawn Point");

        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;
    }
}
