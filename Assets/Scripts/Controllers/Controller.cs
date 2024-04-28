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
  /// The controller responsible for selecting planes.
  /// </summary>
  private PlaneSelectionController _planeSelection;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  public void Awake() {
    _planeSelection = GetComponent<PlaneSelectionController>();
    if (_planeSelection != null) {
      _planeSelection.PlaneSelected += OnPlaneSelected;
    }
  }

  /// <summary>
  /// OnDestroy is called after all frame updates for the last frame of the object’s existence.
  /// </summary>
  public void OnDestroy() {
    if (_planeSelection != null) {
      _planeSelection.PlaneSelected -= OnPlaneSelected;
    }
  }

  /// <summary>
  /// OnPlaneSelected is run when the user selects a plane. 
  /// </summary>
  /// <param name="plane"></param>
  public void OnPlaneSelected(ARPlane plane) {
    
  } 
}