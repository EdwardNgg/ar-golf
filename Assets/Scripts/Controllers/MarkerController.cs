//-----------------------------------------------------------------------
//  MarkerController.cs
//  AR Golf
//
//  Created by Edward Ng on 04/23/2024.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Manages the spawning and anchoring of marker objects based on an AR plane.
/// </summary>
public class MarkerController : MonoBehaviour {

  /// <summary>
  /// A pair holding an XR Reference Image name along with its corresponding Game Object prefab to
  /// spawn with the image.
  /// </summary>
  [Serializable]
  public class ImagePrefabPair {
    /// <summary>
    /// The name of the reference image specified in the XR Reference Image Library attached to the
    /// TrackedImageManager.
    /// </summary>
    public string referenceImageName;
    
    /// <summary>
    /// The corresponding tracking image prefab to spawn on top of the tracked image.
    /// </summary>
    public GameObject trackedImagePrefab;
  }

  /// <summary>
  /// A list of XR Reference Image names along with their corresponding tracked image prefabs.
  /// </summary>
  public List<ImagePrefabPair> imagePrefabPairs;

  /// <summary>
  /// The ARTrackedImageManager that performs 2D image tracking.
  /// </summary>
  private ARTrackedImageManager _trackedImageManager;

  /// <summary>
  /// A dictionary mapping the name of tracked images to their spawned game objects.
  /// </summary>
  private readonly Dictionary<string, GameObject> _trackedImageObjects = 
      new Dictionary<string, GameObject>();

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _trackedImageManager = GetComponent<ARTrackedImageManager>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    if (_trackedImageManager != null) {
      _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
      _trackedImageManager.enabled = true;
    }
  }
  
  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  private void OnDisable() {
    if (_trackedImageManager != null) {
      _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
  }

  /// <summary>
  /// OnTrackedImagesChanged is called once per frame with information about the tracked images in
  /// physical space. When a tracked image is created, spawns the appropriate object on top of the
  /// image. Updates are ignored. When a tracked image is removed, the associated object is
  /// destoryed. 
  /// </summary>
  /// <param name="eventArgs">
  /// The lists of tracked images that have been added, updated, and removed.
  /// </param>
  private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
    foreach (ARTrackedImage newImage in eventArgs.added) {
      CreateMarkerObject(newImage);
    }
  }

  /// <summary>
  /// Creates different marker objects using the name of the tracked image.
  /// </summary>
  /// <param name="image">The image tracked by the tracked image manager</param>
  private void CreateMarkerObject(ARTrackedImage image) {
    string imageName = image.referenceImage.name;
    Debug.Log($"Instantiating {imageName}.");
    if (!_trackedImageObjects.ContainsKey(imageName)) {
      ImagePrefabPair imagePrefabPair =
          imagePrefabPairs.Find(pair => pair.referenceImageName == imageName);
      if (imagePrefabPair != null) {
        GameObject spawnedObject = Instantiate(imagePrefabPair.trackedImagePrefab, image.transform);
        _trackedImageObjects.Add(imageName, spawnedObject);
      }
    }
  }
}