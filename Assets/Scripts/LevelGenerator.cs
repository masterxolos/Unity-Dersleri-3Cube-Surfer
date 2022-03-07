using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    private const float PlayerDistance = 200f;
    [SerializeField] private GameObject player;
    [SerializeField] private List<Transform> sectionList;

    [SerializeField] private Transform sectionStart;

    private Vector3 lastEndPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        lastEndPosition = sectionStart.Find("EndPosition").position;
        int spawnAmount = 2;
        for (int i = 0; i <= spawnAmount; i++)
        {
            SpawnSection();
        }
    }

    private void SpawnSection()
    {
        Transform randomLevelSection = sectionList[Random.Range(0, sectionList.Count)];
        Transform lastEndTransform = SpawnSection(randomLevelSection, lastEndPosition);
        lastEndPosition = lastEndTransform.Find("EndPosition").position;
    }

    private Transform SpawnSection(Transform section, Vector3 spawnPos)
    {
        Transform lastEndTransform = Instantiate(section, spawnPos, Quaternion.identity);
        return lastEndTransform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PlayerDistance)
        {
            SpawnSection();
        }
    }
}
