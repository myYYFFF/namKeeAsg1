using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageSpawnManager : MonoBehaviour
{
    public GameObject pauPrefab;
    public int spawnCount = 10;
    public float radius = 0.15f;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, List<GameObject>> spawnedObjects = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnPau(trackedImage);
        }
    }

    void SpawnPau(ARTrackedImage trackedImage)
    {
        if (spawnedObjects.ContainsKey(trackedImage.referenceImage.name))
            return;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = i * Mathf.PI * 2f / spawnCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            GameObject obj = Instantiate(
                pauPrefab,
                trackedImage.transform.position + offset,
                Quaternion.identity
            );

            obj.transform.parent = trackedImage.transform;
            list.Add(obj);
        }

        spawnedObjects.Add(trackedImage.referenceImage.name, list);
    }
}
