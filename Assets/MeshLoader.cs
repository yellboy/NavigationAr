using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class MeshLoader : MonoBehaviour
{
    public GameObject[] meshPrefabs = new GameObject[0];
    public string objPath;

    public NavMeshBuildSettings navMeshBuildSettings = new NavMeshBuildSettings()
    {
        agentClimb = 0.5f,
        agentHeight = 2f,
        agentRadius = 0.5f,
        agentSlope = 45f,
        tileSize = 256,
        voxelSize = 0.1666f,
        minRegionArea = 0.1f,
        overrideTileSize = true,
        overrideVoxelSize = true,
        agentTypeID = 0,
    };


    IEnumerator Start()
    {

        var loadedObject = new OBJLoader().Load(Path.Combine(Application.streamingAssetsPath, objPath));
        foreach(Transform child in loadedObject.transform)
        {
            child.gameObject.AddComponent<NavMeshSourceTag>();
        }
        loadedObject.transform.localScale *= 22;
        yield break;
    }
}
