//-----------------------------------------------------------------------
//  CardHeadline.cs
//  AR Golf
//
//  Created by Edward Ng on 04/26/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The CardHeadline custom control communicates the subject of the card.
/// </summary>
public class CardHeadline : Label {
  
  /// <summary>
  /// The UXML factory that instantiates a CardHeadline using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<CardHeadline, UxmlTraits> {
    
    /// <summary>
    /// Returns the type name of CardHeadline.
    /// </summary>
    public override string uxmlName => "CardHeadline";

    /// <summary>
    /// Returns the namespace name of CardHeadline.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";

    /// <summary>
    /// Returns the type fully qualified name of CardHeadline.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines UxmlTraits for the CardHeadline.
  /// </summary>
  public new class UxmlTraits : Label.UxmlTraits {
    
    /// <summary>
    /// The text attribute of CardHeadline.
    /// </summary>
    private readonly UxmlStringAttributeDescription _text = new UxmlStringAttributeDescription {
        name = "text",
        defaultValue = ""
    };
    
    /// <summary>
    /// Describes the types of element that can appear as children of this element in a UXML file.
    /// </summary>
    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
      get {
        yield break;
      }
    }

    /// <summary>
    /// Initialize CardHeadline properties using values from the attribute bag.
    /// </summary>
    /// <param name="ve">The object to initialize.</param>
    /// <param name="bag">The attribute bag.</param>
    /// <param name="cc">The creation context; unused.</param>
    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
      base.Init(ve, bag, cc);
      CardHeadline headline = (CardHeadline)ve;
      
      headline.text = _text.GetValueFromBag(bag, cc);
    }
  }

  /// <summary>
  /// The name of the CardHeadline class.
  /// </summary>
  private readonly string _className = "card__headline";

  /// <summary>
  /// The name of the rich text style.
  /// </summary>
  private readonly string _styleName = "HeadlineSmall";

  /// <summary>
  /// The resource path to the corresponding stylesheet for the CardHeadline.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/CardHeadline";

  /// <summary>
  /// The text of the CardHeadline.
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
  /// The text of the CardHeadline.
  /// </summary>
  private string _text;

  /// <summary>
  /// Initializes and returns an instance of CardHeadline.
  /// </summary>
  public CardHeadline() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }
}