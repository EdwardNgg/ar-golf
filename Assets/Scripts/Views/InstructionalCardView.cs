//-----------------------------------------------------------------------
//  InstructionalCardView.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The InstructionalCardView presents instructional cards through to user interface to help guide
/// users through the application.
/// </summary>
public class InstructionalCardView : MonoBehaviour {
  
  /// <summary>
  /// The root UI document in the application.
  /// </summary>
  private UIDocument _document;

  /// <summary>
  /// The instructional card container that manages the instructional cards in the user interface.
  /// </summary>
  private InstructionalCardContainer _container =>
      _document.rootVisualElement.Q<InstructionalCardContainer>();

  /// <summary>
  /// The single instructional card on the user interface.
  /// </summary>
  private InstructionalCard _card => _document.rootVisualElement.Q<InstructionalCard>();

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _document = GetComponent<UIDocument>();
  }

  /// <summary>
  /// OnDisable is called when the behavior becomes disabled and inactive.
  /// </summary>
  private void OnDisable() {
    VisualElement body = _document.rootVisualElement.Q<VisualElement>(className: "body");
    body.RemoveFromClassList("body--end");
    body.Clear();
  }

  /// <summary>
  /// Modifies the disabled attribute of the button in the instructional card.
  /// </summary>
  /// <param name="disabled">Whether the button should be disabled.</param>
  public void SetButtonDisabled(bool disabled) {
    _card.Disabled = disabled;
  }

  /// <summary>
  /// Adds an instructional card to the instructional card container.
  /// </summary>
  /// <param name="headline">The headline of the instructional card.</param>
  /// <param name="supportingText">The supporting text of the instructional card.</param>
  /// <param name="onButtonPointerDown">
  /// The action that is taken when the instructional card button is pressed.
  /// </param>
  public void AddCard(string headline, string supportingText, Action onButtonPointerDown) {
    _container.AddCard(headline, supportingText, onButtonPointerDown);
  }

  /// <summary>
  /// Removes the card from the instructional card container.
  /// </summary>
  public void RemoveCard() {
    _container.RemoveCard();
  }
}