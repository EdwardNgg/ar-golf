//-----------------------------------------------------------------------
//  Model.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// The Model represents the state of the application and performs business logic and operations.
/// </summary>
public class Model : MonoBehaviour {

  /// <summary>
  /// The current state of the application.
  /// </summary>
  private State _state = State.SurfaceSelection;

  /// <summary>
  /// The currently selected plane.
  /// </summary>
  public ARPlane Plane { get; set; }
}