//-----------------------------------------------------------------------
//  CardContent.cs
//  AR Golf
//
//  Created by Edward Ng on 04/27/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A CardContent custom control that acts as a content container for headlines, subheads,
/// supporting text, and containers for card actions.
/// </summary>
public class CardContent : Box {
  
  /// <summary>
  /// The UXML factory that instantiate some CardContent using the data read from a UXML file. 
  /// </summary>
  public new class UxmlFactory : UxmlFactory<CardContent, UxmlTraits> {
    
    /// <summary>
    /// Returns the type name of CardContent.
    /// </summary>
    public override string uxmlName => "CardContent";
    
    /// <summary>
    /// Returns the namespace name of CardContent.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";
    
    /// <summary>
    /// Returns the type fully qualified name of CardContent.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }
  
  /// <summary>
  /// Defines UxmlTraits for the CardContent.
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
  /// The name of the CardContent class.
  /// </summary>
  private readonly string _className = "card__content";
  
  /// <summary>
  /// The resource path to the corresponding stylesheet for the CardContent.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/CardContent";
  
  /// <summary>
  /// Initializes and returns an instance of some CardContent.
  /// </summary>
  public CardContent() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
}