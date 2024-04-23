//-----------------------------------------------------------------------
//  PlaneSelectionController.cs
//  AR Golf
//
//  Created by Edward Ng on 04/23/2024.
//-----------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Selects a detected AR plane and modifies its material.
/// </summary>
public class PlaneSelectionController : MonoBehaviour {
  
  
  /// <summary>
  /// Selects an AR plane through AR ray casts based on a screen position.
  /// </summary>
  /// <param name="screenPosition">
  /// The screen position to cast a ray and select a plane from.
  /// </param>
  public void HandleSelectPlane(Vector2 screenPosition) {
    Debug.Log($"Touch detected at ({screenPosition.x}, {screenPosition.y}).");
  }
}