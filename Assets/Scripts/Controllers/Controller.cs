//-----------------------------------------------------------------------
//  Controller.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// The Controller handles user interaction and works with the model. It ultimately selects the
/// view to render. The Controller acts as the main component overseeing all other controller
/// components.
/// </summary>
class Controller : MonoBehaviour {
  
  /// <summary>
  /// The model containing the state of the application.
  /// </summary>
  private Model _model;

  /// <summary>
  /// The controller responsible for handling user input actions.
  /// </summary>
  private ActionController _actionControl;

  /// <summary>
  /// The controller responsible for spawning and managing marker objects.
  /// </summary>
  private MarkerController _markerControl;
  
  /// <summary>
  /// The controller responsible for selecting planes.
  /// </summary>
  private PlaneSelectionController _planeSelectControl;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _model = GetComponent<Model>();
    
    _planeSelectControl = GetComponent<PlaneSelectionController>();
    _markerControl = GetComponent<MarkerController>();
    _actionControl = GetComponent<ActionController>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    if (_model != null) {
      _model.StateChange += OnStateChange;
    }
    
    if (_planeSelectControl != null) {
      _planeSelectControl.PlaneSelected += OnPlaneSelected;
    }

    if (_markerControl != null) {
      _markerControl.TrackedImageObjectAdded += OnTrackedImageObjectAdded;
    }
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  public void OnDisable() {
    if (_model != null) {
      _model.StateChange -= OnStateChange;
    }
    
    if (_planeSelectControl != null) {
      _planeSelectControl.PlaneSelected -= OnPlaneSelected;
    }

    if (_markerControl != null) {
      _markerControl.TrackedImageObjectAdded -= OnTrackedImageObjectAdded;
    }
  }

  /// <summary>
  /// OnPlaneSelected is run when the user selects a plane. 
  /// </summary>
  /// <param name="plane">The plane that was selected.</param>
  private void OnPlaneSelected(ARPlane plane) {
    _model.Plane = plane;
  }

  /// <summary>
  /// OnStateChange is run when the model changes the state of the application.
  /// </summary>
  /// <param name="state">The new state of the application.</param>
  private void OnStateChange(AppState state) {
    switch (state) {
      case AppState.SurfaceSelection: {
        _actionControl.selectMode = ActionController.SelectionMode.Plane;
        _planeSelectControl.enabled = true;
        _markerControl.enabled = false;
        break;
      }
      case AppState.MarkerRegistration: {
        _actionControl.selectMode = ActionController.SelectionMode.None;
        _planeSelectControl.enabled = false;
        _markerControl.enabled = true;
        break;
      }
    }
  }

  /// <summary>
  /// OnTrackedImageObjectAdded runs when a new object is spawned on a tracked image. 
  /// </summary>
  private void OnTrackedImageObjectAdded(ARTrackedImage image) {
    _model.AddTrackedImageObject(image);
  }
}