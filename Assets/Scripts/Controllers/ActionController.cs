//-----------------------------------------------------------------------
//  ActionController.cs
//  AR Golf
//
//  Created by Edward Ng on 04/23/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages user input actions by interfacing with corresponding components.
/// </summary>
public class ActionController : MonoBehaviour {

  /// <summary>
  /// The type of objects that the select action chooses.
  /// </summary>
  public enum SelectionMode {
    Plane,
    None
  }
  
  /// <summary>
  /// The current type of objects that the select action will choose.
  /// </summary>
  public SelectionMode selectMode = SelectionMode.Plane;

  /// <summary>
  /// The view that presents application content through the user interface.
  /// </summary>
  public View view;
  
  /// <summary>
  /// The actions asset that contain a defined set of input actions.
  /// </summary>
  private Actions _actions;

  /// <summary>
  /// The controller that responds to touch selection actions and selects/deselects AR planes.
  /// </summary>
  private PlaneSelectionController _planeSelection;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _actions = new Actions();
    _planeSelection = GetComponent<PlaneSelectionController>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    _actions.touch.Enable();
    _actions.touch.select.performed += OnSelectTouchPerformed;
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  private void OnDisable() {
    _actions.touch.Disable();
    _actions.touch.select.performed -= OnSelectTouchPerformed;
  }

  /// <summary>
  /// Selects a detected plane when the user performs a touch on the screen.
  /// </summary>
  /// <param name="context"></param>
  private void OnSelectTouchPerformed(InputAction.CallbackContext context) {
    Vector2 screenPosition = context.ReadValue<Vector2>();
    if (!view.IsPointerOverUI(screenPosition)) {
      switch (selectMode) {
        case SelectionMode.Plane:
          _planeSelection.HandleSelectPlane(screenPosition);
          break;
        case SelectionMode.None:
          break;
      }

    }
  }
}