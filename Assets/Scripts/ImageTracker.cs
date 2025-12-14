using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject placeablePrefab; // single prefab
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float radius = 0.2f; // distance from image center
    [SerializeField] private Vector3 prefabScale = new Vector3(2f, 2f, 2f);

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            SpawnModels(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateModels(trackedImage);
            }
            else
            {
                SetModelsActive(false);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            SetModelsActive(false);
        }
    }

    private void SpawnModels(ARTrackedImage trackedImage)
    {
        // Clear old instances if any
        foreach (var go in spawnedPrefabs)
            Destroy(go);
        spawnedPrefabs.Clear();

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = i * Mathf.PI * 2f / spawnCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            Vector3 spawnPos = trackedImage.transform.position + offset;

            GameObject newObj = Instantiate(placeablePrefab, spawnPos, Quaternion.identity);
            newObj.transform.localScale = prefabScale;
            spawnedPrefabs.Add(newObj);

            Debug.Log($"Spawned '{placeablePrefab.name}' at {spawnPos} with scale {prefabScale}");
        }
    }

    private void UpdateModels(ARTrackedImage trackedImage)
    {
        for (int i = 0; i < spawnedPrefabs.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / spawnCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            spawnedPrefabs[i].transform.position = trackedImage.transform.position + offset;
            spawnedPrefabs[i].SetActive(true);
        }
    }

    private void SetModelsActive(bool active)
    {
        foreach (var go in spawnedPrefabs)
            go.SetActive(active);
    }
}
