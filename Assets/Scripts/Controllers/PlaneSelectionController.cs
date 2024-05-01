//-----------------------------------------------------------------------
//  PlaneSelectionController.cs
//  AR Golf
//
//  Created by Edward Ng on 04/23/2024.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Selects a detected AR plane and modifies its material.
/// </summary>
public class PlaneSelectionController : MonoBehaviour {

  /// <summary>
  /// The PlaneSelected event occurs whenever the user selects or deselects a plane.
  /// </summary>
  public event Action<ARPlane> PlaneSelected;

  /// <summary>
  /// The currently selected plane.
  /// </summary>
  // ReSharper disable once MemberCanBePrivate.Global
  public ARPlane Plane {
    get => _plane;
    set {
      _plane = value;
      PlaneSelected?.Invoke(value);
    }
  }

  /// <summary>
  /// The default material for visualizing AR planes. Typically, it is the material attached to the
  /// AR Default Plane prefab under the AR Plane Manager component.
  /// </summary>
  public Material defaultPlaneMaterial;

  /// <summary>
  /// The material to apply to a selected AR plane.
  /// </summary>
  public Material selectedPlaneMaterial;

  /// <summary>
  /// The currently selected plane.
  /// </summary>
  private ARPlane _plane;

  /// <summary>
  /// The plane manager attached to the AR session that detects and tracks flat surfaces in the
  /// physical environment.
  /// </summary>
  private ARPlaneManager _planeManager;

  /// <summary>
  /// The raycast manager attached to the AR session that determines where a ray intersects with a
  /// trackable object.
  /// </summary>
  private ARRaycastManager _raycastManager;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _planeManager = GetComponent<ARPlaneManager>();
    _raycastManager = GetComponent<ARRaycastManager>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    _planeManager.enabled = true;
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive. It stops visualizing AR
  /// planes that are detected in the application.
  /// </summary>
  private void OnDisable() {
    _planeManager.planePrefab = null;
    
    foreach (ARPlane plane in _planeManager.trackables) {
      if (plane.trackableId != _plane.trackableId) {
        plane.gameObject.SetActive(false);
      }
    }
  }

  /// <summary>
  /// Selects an AR plane through AR raycasts based on a screen position.
  /// </summary>
  /// <param name="screenPosition">
  /// The screen position to cast a ray and select a plane from.
  /// </param>
  /// <remarks>
  /// The method will always deselect the currently selected plane when called.
  /// </remarks>
  public void HandleSelectPlane(Vector2 screenPosition) {
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    
    DeselectPlane();
    
    if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon)) {
      ARRaycastHit hit = hitResults[0];
      TrackableId planeId = hit.trackableId;

      ARPlane plane = _planeManager.GetPlane(planeId);
      MeshRenderer planeMesh = plane.gameObject.GetComponent<MeshRenderer>();
      planeMesh.material = selectedPlaneMaterial;

      Plane = plane;
    }
  }

  /// <summary>
  /// Deselects the currently selected plane by returning the original material to the plane.
  /// </summary>
  private void DeselectPlane() {
    if (Plane != null) {
      MeshRenderer planeMesh = Plane.gameObject.GetComponent<MeshRenderer>();
      planeMesh.material = defaultPlaneMaterial;
      Plane = null;
    }
  }
}