//-----------------------------------------------------------------------
//  Model.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// The Model represents the state of the application and performs business logic and operations.
/// </summary>
public class Model : MonoBehaviour {

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
  /// The StateChange event occurs when the application state is updated.
  /// </summary>
  public event Action<AppState> StateChange;

  /// <summary>
  /// The current state of the application.
  /// </summary>
  private AppState _state = AppState.SurfaceSelection;

  /// <summary>
  /// The currently selected plane.
  /// </summary>
  public ARPlane Plane { get; set; }
}