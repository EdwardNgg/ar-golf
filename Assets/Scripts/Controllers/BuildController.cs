//-----------------------------------------------------------------------
//  BuildController.cs
//  AR Golf
//
//  Created by Edward Ng on 05/02/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// The BuildController manages input for spawning, 
/// </summary>
public class BuildController : MonoBehaviour {

  private enum Mode {
    Select,
    Create,
    Manipulate,
    Move,
    Scale
  }

  public GameObject createPrefab;
  
  private Actions _actions;

  private ARAnchorManager _anchorManager;

  private ARPlaneManager _planeManager;

  private ARRaycastManager _raycastManager;

  private Model _model;

  public View view;

  private Mode _currentMode = Mode.Create;

  private ARAnchor _selected;

  private Vector2 _lastPosition;

  private Vector3 _lastFromDirection;

  private float _lastMagnitude;

  private int _touchCount = 0;

  private bool _ignoreTouch = true;
  private void Awake() {
    _actions = new Actions();
    _anchorManager = GetComponent<ARAnchorManager>();
    _planeManager = GetComponent<ARPlaneManager>();
    _raycastManager = GetComponent<ARRaycastManager>();
    _model = GetComponent<Model>();
  }

  private void OnEnable() {
    _actions.touch.Enable();
    
    _actions.touch.pressPrimary.started += OnPressPerformed;
    _actions.touch.pressPrimary.canceled += OnPressCanceled;

    _actions.touch.pressSecondary.started += OnPressPerformed;
    _actions.touch.pressSecondary.canceled += OnPressCanceled;

    _actions.touch.positionPrimary.performed += OnPositionPerformed;
    _actions.touch.positionSecondary.performed += OnPositionSecondaryPerformed;
  }

  /// <summary>
  /// OnPressPerformed is run when the input system detects a press being performed on the
  /// ouchscreen.
  /// </summary>
  /// <param name="context">context</param>
  private void OnPressPerformed(InputAction.CallbackContext context) {
    if (view.IsPointerOverUI(_actions.touch.positionPrimary.ReadValue<Vector2>())) {
      return;
    }
    
    _touchCount++;
  }

  /// <summary>
  /// OnPressCanceled is run when the input system detects when a press is canceled or stopped on
  /// the touchscreen.
  /// </summary>
  /// <param name="context">context</param>
  private void OnPressCanceled(InputAction.CallbackContext context) {
    if (_touchCount > 0) {
      _touchCount--;
    }

    if (_ignoreTouch) {
      _ignoreTouch = false;
    }
    
    _lastPosition = Vector2.zero;
    _lastMagnitude = 0.0f;
    _lastFromDirection = Vector3.zero;
  }

  private void OnPositionPerformed(InputAction.CallbackContext context) {
    Vector2 screenPosition = context.ReadValue<Vector2>();

    if (view.IsPointerOverUI(screenPosition)) {
      return;
    }

    if (_ignoreTouch) {
      return;
    }
    
    switch (_currentMode) {
      case Mode.Select: {
        Select(screenPosition);
        break;
      }
      case Mode.Create: {
        Create(screenPosition);
        break;
      }
      case Mode.Manipulate: {
        if (_touchCount == 1) {
          // Invariant: There is only one finger touching the screen.
          if (_lastPosition.Equals(Vector2.zero) && _lastFromDirection.Equals(Vector3.zero)) {
            if (IsSelected(screenPosition)) {
              _lastPosition = screenPosition;
            } else if (IsRotateGizmoSelected(screenPosition)) {
              _lastFromDirection = GetDirectionFromSelected(screenPosition);
            } else {
              _selected.transform.GetChild(0).GetComponent<SelectableObject>().Select(false);
              _selected = null;
              _currentMode = Mode.Select;
            }
          }

          if (!_lastPosition.Equals(Vector2.zero)) {
            Move(screenPosition);
          }

          if (!_lastFromDirection.Equals(Vector3.zero)) {
            Rotate(screenPosition);
          }
        }
        break;
      }
    }
  }

  private void Rotate(Vector2 screenPosition) {
    Vector3 toDirection = GetDirectionFromSelected(screenPosition);
    Quaternion newQuaternion = new Quaternion();
    newQuaternion.SetFromToRotation(_lastFromDirection, toDirection);

    Transform selectedObject = _selected.transform.GetChild(0);

    selectedObject.transform.rotation = newQuaternion * selectedObject.transform.rotation;
    _lastFromDirection = toDirection;
  }

  private Vector3 GetDirectionFromSelected(Vector2 screenPosition) {
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon)) {
      foreach (ARRaycastHit hit in hitResults) {
        if (hit.trackableId == _model.Plane.trackableId) {
          return hit.pose.position - _selected.transform.position;
        }
      }
    }

    return Vector3.zero;
  }

  private void OnPositionSecondaryPerformed(InputAction.CallbackContext context) {
    if (_touchCount == 2) {
      _lastPosition = Vector2.zero;
      
      Vector2 screenPosition = _actions.touch.positionPrimary.ReadValue<Vector2>();
      Vector2 screenPosition2 = context.ReadValue<Vector2>();

      if (_currentMode == Mode.Manipulate) {
        if (_lastMagnitude == 0) {
          if (IsSelected(screenPosition) || IsSelected(screenPosition2)) {
            _lastMagnitude = Vector2.Distance(screenPosition, screenPosition2);
          }
        } else {
          float magnitude = Vector2.Distance(screenPosition, screenPosition2);
          Scale(magnitude);
        }
      }
    }
  }

  private bool IsSelected(Vector2 screenPosition) {
    Ray ray = Camera.main!.ScreenPointToRay(screenPosition);

    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) {
      return _selected.transform.GetChild(0) == hit.transform;
    }

    return false;
  }

  private bool IsRotateGizmoSelected(Vector2 screenPosition) {
    Ray ray = Camera.main!.ScreenPointToRay(screenPosition);

    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) {
      Transform cube = _selected.transform.GetChild(0);
      Transform gizmo = cube.GetChild(1);
      return _selected.transform.GetChild(0).GetChild(1) == hit.transform;
    }

    return false;
  }

  private void Select(Vector2 screenPosition) {
    Ray ray = Camera.main!.ScreenPointToRay(screenPosition);

    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) {
      Transform parent = hit.transform.parent; 
      ARAnchor anchor = parent.GetComponent<ARAnchor>();
      if (anchor != null) {
        _selected = anchor;
        hit.transform.GetComponent<SelectableObject>().Select(true);
        _currentMode = Mode.Manipulate;
      }
    }
  }

  private void Create(Vector2 screenPosition) {
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon)) {
      foreach (ARRaycastHit hit in hitResults) {
        if (hit.trackableId == _model.Plane.trackableId) {
          ARAnchor anchor = _anchorManager.AttachAnchor(_model.Plane, hit.pose);
          Instantiate(createPrefab, anchor.transform);
          _selected = anchor;
          _currentMode = Mode.Manipulate;
          break;
        }
      }
    }
  }

  private void Move(Vector2 screenPosition) {
    if (_touchCount == 1) {
      List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
      if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon)) {
        foreach (ARRaycastHit hit in hitResults) {
          if (hit.trackableId == _model.Plane.trackableId) {
            ARAnchor anchor = _anchorManager.AttachAnchor(_model.Plane, hit.pose);
            _selected.transform.GetChild(0).transform.SetParent(anchor.transform, false);
            anchor.transform.localScale = _selected.transform.localScale;
            Destroy(_selected);
            _selected = anchor;
            break;
          }
        }
      }
    }
  }

  private void Scale(float magnitude) {
    float magnitudeDelta = magnitude - _lastMagnitude;
    float scaleDelta = magnitudeDelta * 0.001f;
    
    float newScale = Mathf.Clamp(_selected.transform.localScale.x + scaleDelta, 0.1f, 2.0f);
    _selected.transform.localScale = new Vector3(newScale, newScale, newScale);

    _lastMagnitude = magnitude;
  }
}