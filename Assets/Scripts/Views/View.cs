//-----------------------------------------------------------------------
//  View.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// The View presents content through the UIToolkit user interface.
/// </summary>
public class View : MonoBehaviour {
  
  /// <summary>
  /// The model representing the state of the application.
  /// </summary>
  public Model model;
    
  /// <summary>
  /// The root UI document in the application.
  /// </summary>
  private UIDocument _document;

  /// <summary>
  /// The view that represents and manages instructional cards on the user interface.
  /// </summary>
  private InstructionalCardView _instructionalCards;

  private TabsView _tabs;

  /// <summary>
  /// Determines if the pointer at a screen position is over a UI element.
  /// </summary>
  /// <param name="screenPosition">
  /// The position of the pointer relative to the screen at the top-left.
  /// </param>
  /// <returns>Whether the pointer is over a UI element.</returns>
  public bool IsPointerOverUI(Vector2 screenPosition) {
    IPanel panel = _document.rootVisualElement.panel;
    
    Vector2 bottomLeftScreenPosition = screenPosition;
    bottomLeftScreenPosition.y = Screen.height - bottomLeftScreenPosition.y;
    Vector2 worldPosition = RuntimePanelUtils.ScreenToPanel(panel, bottomLeftScreenPosition);

    VisualElement picked = panel.Pick(worldPosition);
    return picked != null;
  }

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _document = GetComponent<UIDocument>();
    _instructionalCards = GetComponent<InstructionalCardView>();
    _tabs = GetComponent<TabsView>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active.
  /// </summary>
  private void OnEnable() {
    if (model != null) {
      model.StateChange += OnStateChange;
      model.PlaneChange += OnPlaneChange;
      model.TrackedImagesChanged += OnTrackedImagesChanged;
    }
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  private void OnDisable() {
    if (model != null) {
      model.StateChange -= OnStateChange;
      model.PlaneChange -= OnPlaneChange;
      model.TrackedImagesChanged -= OnTrackedImagesChanged;
    }
  }

  /// <summary>
  /// OnPlaneChange runs everytime the user selects/deselects an AR plane. It modifies the
  /// disabled attribute to the instructional card button when the application state is in
  /// PlaneSelection mode.
  /// </summary>
  /// <param name="plane">The plane that is currently selected.</param>
  private void OnPlaneChange(ARPlane plane) {
    if (model.State == AppState.SurfaceSelection) {
      bool disabled = plane == null;
      _instructionalCards.SetButtonDisabled(disabled);
    }
  }

  /// <summary>
  /// OnTrackedImagesChanged runs once the application registers the required amount of tracked
  /// images and spawns their corresponding objects. Enables the continue button in the
  /// instructional card.
  /// </summary>
  private void OnTrackedImagesChanged(List<ARTrackedImage> list) {
    if (model.State == AppState.MarkerRegistration) {
      bool disabled = list.Count < model.maximumTrackedImages;
      _instructionalCards.SetButtonDisabled(disabled);
    }
  }

  /// <summary>
  /// OnStateChange runs everytime the state of the application in the model changes. It modifies
  /// the instructional cards on screen using the state.
  /// </summary>
  /// <param name="state">The state of the application.</param>
  private void OnStateChange(AppState state) {
    switch (state) {
      case AppState.SurfaceSelection: {
        _instructionalCards.AddCard(
            headline: "Surface Selection",
            supportingText: "Move the camera around to scan for surfaces. Tap on the surface to " +
                            "build a golf course on.",
            onButtonPointerDown: () => model.State = AppState.MarkerRegistration);
        break;
      }
      case AppState.MarkerRegistration: {
        _instructionalCards.AddCard(
            headline: "Marker Registration",
            supportingText: "Place the four markers on the surface to create the starting point, " +
                            "teleportation points, and goal.",
            onButtonPointerDown: () => {
              _instructionalCards.RemoveCard();
              _instructionalCards.enabled = false;
              model.State = AppState.Build;
            });
        break;
      }
      case AppState.Build: {
        _tabs.enabled = true;
        break;
      }
    }
  }
}