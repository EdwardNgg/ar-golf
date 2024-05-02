//-----------------------------------------------------------------------
//  InstructionalCardContainer.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// InstructionalCardContainer is a container that holds and manages the entrance and exit of
/// instructional cards. The container only holds one card.
/// </summary>
public class InstructionalCardContainer : Box {

  /// <summary>
  /// The UXML factory that instantiates an InstructionalCardContainer using the data read from a
  /// UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<InstructionalCardContainer, UxmlTraits> {

    /// <summary>
    /// Returns the type name of InstructionalCardContainer.
    /// </summary>
    public override string uxmlName => "InstructionalCardContainer";

    /// <summary>
    /// Returns the namespace name of InstructionalCardContainer.
    /// </summary>
    public override string uxmlNamespace => "ARGolf";

    /// <summary>
    /// Returns the type fully qualified name of InstructionalCardContainer.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }
  
  /// <summary>
  /// Defines the UxmlTraits for the InstructionalCardContainer
  /// </summary>
  public new class UxmlTraits : Box.UxmlTraits {
    
    /// <summary>
    /// Describes the types of element that can appear as children of this element in a UXML file.
    /// </summary>
    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
      get {
        yield return new UxmlChildElementDescription(typeof(VisualElement));
      }
    }
  }

  /// <summary>
  /// A queue of cards to add to the instructional card container.
  /// </summary>
  private readonly Queue<InstructionalCard> _cardQueue = new Queue<InstructionalCard>();

  /// <summary>
  /// The name of the InstructionalCardContainer class.
  /// </summary>
  private readonly string _className = "instructional-card-container";

  /// <summary>
  /// The resource path to the corresponding stylesheet for the InstructionalCardContainer.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/InstructionalCardContainer";

  /// <summary>
  /// The instructional card currently in the container.
  /// </summary>
  private InstructionalCard _currentCard => this.Q<InstructionalCard>();

  /// <summary>
  /// Initializes and returns an empty instance of InstructionalCardContainer.
  /// </summary>
  public InstructionalCardContainer() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
  
  /// <summary>
  /// Adds a new instructional card to the container using the headline, supporting text, and
  /// button pointer down action.
  /// </summary>
  /// <param name="headline">The headline of the new instructional card.</param>
  /// <param name="supportingText">The supporting text of the new instructional card.</param>
  /// <param name="onButtonPointerDown">
  /// The action taken when a pointer down event occurs on the instructional card button.
  /// </param>
  public void AddCard(string headline, string supportingText, Action onButtonPointerDown) {
    InstructionalCard card = new InstructionalCard(headline, supportingText, onButtonPointerDown);
    _cardQueue.Enqueue(card);

    if (childCount == 0) {
      // There is no card in the container and there is a card queued to be added.
      // Run a transition to show a card entering from the bottom of the screen.
      InstructionalCard nextCard = _cardQueue.Dequeue();
      nextCard.AddToClassList(Card.BottomClassName);
      nextCard.RegisterCallback<GeometryChangedEvent>(EnterFromBottom);
      nextCard.RegisterCallback<TransitionEndEvent>(ExitToLeft);
      nextCard.RegisterCallback<TransitionEndEvent>(ExitToBottom);
      nextCard.RegisterCallback<DetachFromPanelEvent>(AddNextCardOnDetach);
      
      hierarchy.Add(nextCard);
    } else {
      // There is a card currently in the container and there is a card queued to be added.
      // Run a transition to move the current card to the left side of the screen.
      _currentCard.AddToClassList(Card.LeftClassName);
    }
  }

  /// <summary>
  /// Removes the instructional card from the container.
  /// </summary>
  public void RemoveCard() {
    if (childCount != 0) {
      _currentCard.AddToClassList(Card.BottomClassName);
    }
  }

  /// <summary>
  /// EnterFromBottom is run when the new instructional card is rendered or when their geometry
  /// first changes. It animates the card entering the view from the bottom.
  /// </summary>
  /// <param name="evt">The geometry changed event.</param>
  private void EnterFromBottom(GeometryChangedEvent evt) {
    if (evt.currentTarget is InstructionalCard card) {
      card.RemoveFromClassList(Card.BottomClassName);
    }
    
    evt.PreventDefault();
    evt.StopPropagation();
  }

  /// <summary>
  /// EnterFromRight is run when the new instructional card is rendered or when their geometry
  /// first changes. It animates the card entering the view from the right. If there is another card
  /// queued, the card will proceed to exit the screen to the left.
  /// </summary>
  /// <param name="evt">The geometry changed event.</param>
  private void EnterFromRight(GeometryChangedEvent evt) {
    if (evt.currentTarget is InstructionalCard card) {
      card.RemoveFromClassList(Card.RightClassName);
      if (_cardQueue.Count != 0) {
        card.AddToClassList(Card.LeftClassName);
      }
    }
    
    evt.PreventDefault();
    evt.StopPropagation();
  }

  /// <summary>
  /// ExitToLeft is run when the current instructional card finishes transitioning to the left. It
  /// destorys the current instructional card.
  /// </summary>
  /// <param name="evt">The transition ended event.</param>
  private void ExitToLeft(TransitionEndEvent evt) {
    if (evt.currentTarget is InstructionalCard card &&
        evt.stylePropertyNames.Contains("left") &&
        card.ClassListContains(Card.LeftClassName)) {
      Clear();
    }
    evt.PreventDefault();
    evt.StopPropagation();
  }

  /// <summary>
  /// ExitToBottom is run when the current instructional card finishes transitioning to the bottom.
  /// It destroys the current instructional card.
  /// </summary>
  /// <param name="evt">The transition ended event.</param>
  private void ExitToBottom(TransitionEndEvent evt) {
    if (evt.currentTarget is InstructionalCard card &&
        evt.stylePropertyNames.Contains("bottom") &&
        card.ClassListContains(Card.BottomClassName)) {
      Clear();
    }
    
    evt.PreventDefault();
    evt.StopPropagation();
  }

  /// <summary>
  /// Inserts the next card into the instructional card container from the queue when the current
  /// card is detached from the panel.
  /// </summary>
  private void AddNextCardOnDetach(DetachFromPanelEvent evt) {
    if (evt.currentTarget is InstructionalCard) {
      if (_cardQueue.Count != 0) {
        InstructionalCard nextCard = _cardQueue.Dequeue();
        nextCard.AddToClassList(Card.RightClassName);
        nextCard.RegisterCallback<GeometryChangedEvent>(EnterFromRight);
        nextCard.RegisterCallback<TransitionEndEvent>(ExitToLeft);
        nextCard.RegisterCallback<TransitionEndEvent>(ExitToBottom);
        nextCard.RegisterCallback<DetachFromPanelEvent>(AddNextCardOnDetach);
        hierarchy.Add(nextCard);
      }
    }
    
    evt.PreventDefault();
    evt.StopPropagation();
  }
}