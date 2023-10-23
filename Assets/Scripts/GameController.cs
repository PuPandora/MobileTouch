using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("생성하고자하는 타일 참조")]
    public Transform tile;

    [Tooltip("첫번 째 타일 생성 위치")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("처음에 생성되는 타일의 개수")]
    [Range(1, 15)]
    public int initSpawnNum = 10;

    [Tooltip("다음 타일이 생성될 위치")]
    private Vector3 nextTileLocation;

    [Tooltip("다음 타일의 회전 값")]
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
