using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeshLoader : MonoBehaviour
{
    public GameObject[] meshPrefabs = new GameObject[0];

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
        var createdMeshes = new List<GameObject>();
        foreach (GameObject meshPrefab in meshPrefabs)
        {
            createdMeshes.Add(Instantiate(meshPrefab));
        }

        yield break;

        /*yield return null;
        yield return null;

        NavMeshData navMeshData = new NavMeshData();
        List<NavMeshBuildSource> navMeshBuildSources = new List<NavMeshBuildSource>();
        Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 100);
        foreach (GameObject createdMesh in createdMeshes)
        {
            MeshFilter[] meshFilters = createdMesh.GetComponentsInChildren<MeshFilter>();
            foreach (var meshFilter in meshFilters)
            {
                var navMeshBuildSource = new NavMeshBuildSource();
                navMeshBuildSource.shape = NavMeshBuildSourceShape.Mesh;
                navMeshBuildSource.transform = meshFilter.transform.localToWorldMatrix;
                navMeshBuildSource.sourceObject = meshFilter.sharedMesh;

                navMeshBuildSources.Add(navMeshBuildSource);

                //bounds.Encapsulate(meshFilter.sharedMesh.bounds);
            }
        }

        NavMeshBuilder.UpdateNavMeshData(navMeshData, this.navMeshBuildSettings, navMeshBuildSources, bounds);

        NavMesh.AddNavMeshData(navMeshData);*/
    }
}
