//-----------------------------------------------------------------------
//  CardSupportingText.cs
//  AR Golf
//
//  Created by Edward Ng on 04/26/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The CardSupportingText custom control includes the body content of the card.
/// </summary>
public class CardSupportingText : Label {
  
  /// <summary>
  /// The UXML factory that instantiates a CardSupportingText using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<CardSupportingText, UxmlTraits> {
    
    /// <summary>
    /// Returns the type name of CardSupportingText.
    /// </summary>
    public override string uxmlName => "CardSupportingText";

    /// <summary>
    /// Returns the namespace name of CardSupportingText.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";

    /// <summary>
    /// Returns the type fully qualified name of CardSupportingText.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines the UxmlTraits for the CardSupportingText.
  /// </summary>
  public new class UxmlTraits : Label.UxmlTraits {
    
    /// <summary>
    /// The text attribute of CardSupportingText.
    /// </summary>
    private readonly UxmlStringAttributeDescription _text = new UxmlStringAttributeDescription {
        name = "text",
        defaultValue = "",
    };
    
    /// <summary>
    /// Describes the types of elements that can appear as children of this element.
    /// </summary>
    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
      get {
        yield break;
      }
    }

    /// <summary>
    /// Initialize CardSupportingText properties using values from the attribute bag.
    /// </summary>
    /// <param name="ve">The object to initialize.</param>
    /// <param name="bag">The attribute bag.</param>
    /// <param name="cc">The creation context; unused.</param>
    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
      base.Init(ve, bag, cc);
      CardSupportingText supportingText = (CardSupportingText)ve;

      supportingText.text = _text.GetValueFromBag(bag, cc);
    }
  }

  /// <summary>
  /// The name of the CardSupportingText class.
  /// </summary>
  private readonly string _className = "card__supporting-text";
  
  /// <summary>
  /// The name of the rich text style.
  /// </summary>
  private readonly string _styleName = "BodySmall";

  /// <summary>
  /// The resource path to the corresponding stylesheet for the CardSupportingText.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/CardSupportingText";

  /// <summary>
  /// The text of the CardSupportingText.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public new string text {
    get => _text;
    set {
      _text = value;
      base.text = $"<style=\"{_styleName}\">{value}</style>";
    }
  }

  /// <summary>
  /// The text of the CardSupportingText.
  /// </summary>
  private string _text;

  /// <summary>
  /// Initialize and returns an instance of CardSupportingText.
  /// </summary>
  public CardSupportingText() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
}