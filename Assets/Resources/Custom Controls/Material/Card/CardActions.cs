//-----------------------------------------------------------------------
//  CardActions.cs
//  AR Golf
//
//  Created by Edward Ng on 04/27/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A CardActions custom control that acts as a content container for buttons, icon buttons,
/// selection controls, and linked text.
/// </summary>
public class CardActions : Box {

  /// <summary>
  /// The UXML factory that instantiate some CardActions using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<CardActions, UxmlTraits> {

    /// <summary>
    /// Returns the type name of CardActions.
    /// </summary>
    public override string uxmlName => "CardActions";

    /// <summary>
    /// Returns the namespace name of CardActions.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";

    /// <summary>
    /// Returns the type fully qualified name of CardActions.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines UxmlTraits for the CardActions.
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
  /// The name of the CardActions class.
  /// </summary>
  private readonly string _className = "card__actions";

  /// <summary>
  /// The resource path to the corresponding stylesheet for the CardActions.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/CardActions";
  
  /// <summary>
  /// Initializes and returns an instance of some CardActions.
  /// </summary>
  public CardActions() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
}