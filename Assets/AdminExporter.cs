using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
using UnityEngine.XR.WSA.Sharing;

public class AdminExporter : MonoBehaviour
{
    public GameObject _anchorObject;

    private WorldAnchorStore _anchorStore;

    private void Start()
    {
        WorldAnchorStore.GetAsync(store => _anchorStore = store);
    }

    public void ExportAnchor()
    {
        _anchorObject.transform.position = Camera.main.transform.position;
        _anchorObject.transform.rotation = Camera.main.transform.rotation;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "anchor.topicteam")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "anchor.topicteam"));
        }

        ExportWorldAnchor();
        SaveAnchorPosition();
    }

    private void SaveAnchorPosition()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "anchor.vector"), $"{_anchorObject.transform.position.x} {_anchorObject.transform.position.y} {_anchorObject.transform.position.z}");
    }

    private void ExportWorldAnchor()
    {
        var worldAnchor = _anchorObject.GetComponent<WorldAnchor>();
        if (worldAnchor != null)
        {
            _anchorStore.Delete(_anchorObject.name);
            DestroyImmediate(worldAnchor);
        }

        worldAnchor = _anchorObject.AddComponent<WorldAnchor>();
        _anchorStore.Save(_anchorObject.name, worldAnchor);

        WorldAnchorTransferBatch transferBatch = new WorldAnchorTransferBatch();
        transferBatch.AddWorldAnchor(_anchorObject.name, worldAnchor);
        WorldAnchorTransferBatch.ExportAsync(transferBatch, OnExportDataAvailable, OnExportComplete);
    }

    private void OnExportComplete(SerializationCompletionReason completionReason)
    {
        if (completionReason != SerializationCompletionReason.Succeeded)
        {
            // If we have been transferring data and it failed, 
            // tell the client to discard the data
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "log.txt"), "Failed, Completeion reason:" + completionReason);
        }
        else
        {
            // Tell the client that serialization has succeeded. 
            // The client can start importing once all the data is received.
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "log.txt"), "Succeeded, Completeion reason:" + completionReason);
        }
    }

    private void OnExportDataAvailable(byte[] data)
    {
        using (var stream = File.Open(Path.Combine(Application.persistentDataPath, "anchor.topicteam"), FileMode.OpenOrCreate))
        {
            stream.Seek(0, SeekOrigin.End);
            stream.Write(data, 0, data.Length);
        }
    }
}
