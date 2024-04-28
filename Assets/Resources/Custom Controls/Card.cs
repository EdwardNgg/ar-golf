//-----------------------------------------------------------------------
//  Card.cs
//  AR Golf
//
//  Created by Edward Ng on 04/26/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A Card custom control is a container that holds all card elements. The card container is the
/// only required element of a card.
/// </summary>
public class Card : Box {
  
  /// <summary>
  /// The UXML factory that instantiates a Card using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<Card, UxmlTraits> {
    
    /// <summary>
    /// Returns the type name of Card.
    /// </summary>
    public override string uxmlName => "Card";

    /// <summary>
    /// Returns the namespace name of Card.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";

    /// <summary>
    /// Returns the type fully qualified name of Card.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }
  
  /// <summary>
  /// Defines UxmlTraits for the Card.
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
  /// The name of the Card class.
  /// </summary>
  private readonly string _className = "card";

  /// <summary>
  /// The resource path to the corresponding stylesheet for the Card.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/Card";

  /// <summary>
  /// Initializes and returns an instance of a Card.
  /// </summary>
  public Card() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
}
