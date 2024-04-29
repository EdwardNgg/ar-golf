//-----------------------------------------------------------------------
//  View.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class View : MonoBehaviour {
  
  /// <summary>
  /// The root UI document in the application.
  /// </summary>
  public UIDocument document;
  
  /// <summary>
  /// The model representing the state of the application.
  /// </summary>
  private Model _model;

  /// <summary>
  /// Returns the body of the root UI document in the application.
  /// </summary>
  private VisualElement _body => document.rootVisualElement.Q<VisualElement>(className: "body");

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _model = GetComponent<Model>();
    if (_model != null) {
      OnStateChange(_model.State);
      _model.StateChange += OnStateChange;
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
        string headline = "Surface Selection";
        string support = "Tap on the surface you would like to build a golf course on.";
        RenderInstructionalCard(headline, support);
        break;
      }
    }
  }
  
  /// <summary>
  /// Adds an instructional card to the application view.
  /// </summary>
  /// <param name="headline">The headline of the instructional card.</param>
  /// <param name="supportingText">The supporting text of the instructional card.</param>
  private void RenderInstructionalCard(string headline, string supportingText) {
    InstructionalCard card = new InstructionalCard(headline, supportingText);
    _body.Add(card);
  }
}