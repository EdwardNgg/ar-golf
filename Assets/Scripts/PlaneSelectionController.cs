//-----------------------------------------------------------------------
//  PlaneSelectionController.cs
//  AR Golf
//
//  Created by Edward Ng on 04/23/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Selects a detected AR plane and modifies its material.
/// </summary>
public class PlaneSelectionController : MonoBehaviour {

  /// <summary>
  /// The ray cast manager attached to the AR session.
  /// </summary>
  private ARRaycastManager _raycastManager;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _raycastManager = GetComponent<ARRaycastManager>();
  }
  
  /// <summary>
  /// Selects an AR plane through AR ray casts based on a screen position.
  /// </summary>
  /// <param name="screenPosition">
  /// The screen position to cast a ray and select a plane from.
  /// </param>
  public void HandleSelectPlane(Vector2 screenPosition) {
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon)) {
      Debug.Log("Touch at Plane.");
    } else {
      Debug.Log("Touch not at Plane.");
    }
  }
}