//-----------------------------------------------------------------------
//  Model.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// The Model represents the state of the application and performs business logic and operations.
/// </summary>
public class Model : MonoBehaviour {
  
  /// <summary>
  /// The PlaneChange event occurs when the user selects/deselects an AR plane.
  /// </summary>
  public event Action<ARPlane> PlaneChange;
  
  /// <summary>
  /// The StateChange event occurs when the application state is updated.
  /// </summary>
  public event Action<AppState> StateChange;
  
  /// <summary>
  /// The TrackedImagesChanged event fires when the list of tracked images is updated.
  /// </summary>
  public event Action<List<ARTrackedImage>> TrackedImagesChanged;
  
  /// <summary>
  /// The currently selected plane.
  /// </summary>
  public ARPlane Plane {
    get => _plane;
    set {
      _plane = value;
      PlaneChange?.Invoke(value);
    }
  }
  
  /// <summary>
  /// The current state of the application.
  /// </summary>
  public AppState State {
    get => _state;
    set {
      _state = value;
      StateChange?.Invoke(value);
    }
  }
  
  /// <summary>
  /// The maximum number of tracked images in the application.
  /// </summary>
  public int maximumTrackedImages;

  /// <summary>
  /// The list of tracked images with corresponding marker objects in the application.
  /// </summary>
  public List<ARTrackedImage> trackedImages;
  
  /// <summary>
  /// The current state of the application.
  /// </summary>
  private AppState _state;

  /// <summary>
  /// The currently selected plane.
  /// </summary>
  private ARPlane _plane;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  private void Start() {
    State = AppState.SurfaceSelection;
    Plane = null;
  }

  /// <summary>
  /// Adds a tracked image and its corresponding spawned object to the model.
  /// </summary>
  /// <param name="image">A new tracked image.</param>
  public void AddTrackedImageObject(ARTrackedImage image) {
    trackedImages.Add(image);
    TrackedImagesChanged?.Invoke(trackedImages);
  }
}