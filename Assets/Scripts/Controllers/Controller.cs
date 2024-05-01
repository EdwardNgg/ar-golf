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
  private ActionController _actions;

  /// <summary>
  /// The controller responsible for spawning and managing marker objects.
  /// </summary>
  private MarkerController _markers;
  
  /// <summary>
  /// The controller responsible for selecting planes.
  /// </summary>
  private PlaneSelectionController _planeSelection;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _model = GetComponent<Model>();
    
    _planeSelection = GetComponent<PlaneSelectionController>();
    _markers = GetComponent<MarkerController>();
    _actions = GetComponent<ActionController>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    if (_model != null) {
      _model.StateChange += OnStateChange;
    }
    
    if (_planeSelection != null) {
      _planeSelection.PlaneSelected += OnPlaneSelected;
    }
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  public void OnDisable() {
    if (_model != null) {
      _model.StateChange -= OnStateChange;
    }
    
    if (_planeSelection != null) {
      _planeSelection.PlaneSelected -= OnPlaneSelected;
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
        _actions.selectMode = ActionController.SelectionMode.Plane;
        _planeSelection.enabled = true;
        _markers.enabled = false;
        break;
      }
      case AppState.MarkerRegistration: {
        _actions.selectMode = ActionController.SelectionMode.None;
        _planeSelection.enabled = false;
        _markers.enabled = true;
        break;
      }
    }
  }
}