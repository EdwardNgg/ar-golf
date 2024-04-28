//-----------------------------------------------------------------------
//  CardButton.cs
//  AR Golf
//
//  Created by Edward Ng on 04/27/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A CardButton custom control that lets people take action and make choices with one tap in a
/// card.
/// </summary>
public class CardButton : Button {

  /// <summary>
  /// The UXML factory that instantiate some CardButton using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<CardButton, UxmlTraits> {

    /// <summary>
    /// Returns the type name of CardButton.
    /// </summary>
    public override string uxmlName => "CardButton";

    /// <summary>
    /// Returns the namespace name of CardButton.
    /// </summary>
    public override string uxmlNamespace => "Material.Card";

    /// <summary>
    /// Returns the type fully qualified name of CardButton.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines UxmlTraits for the CardButton.
  /// </summary>
  public new class UxmlTraits : Button.UxmlTraits {

    /// <summary>
    /// The disabled attribute of CardButton.
    /// </summary>
    private readonly UxmlBoolAttributeDescription _disabled = new UxmlBoolAttributeDescription {
        name = "disabled",
        defaultValue = false
    };
    
    /// <summary>
    /// The label-text attribute of CardButton
    /// </summary>
    private readonly UxmlStringAttributeDescription _labelText = new UxmlStringAttributeDescription {
        name = "label-text",
        defaultValue = "Button"
    };
    
    /// <summary>
    /// Describes the types of element that can appear as children of this element in a UXML file.
    /// </summary>
    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
      get {
        yield return new UxmlChildElementDescription(typeof(VisualElement));
      }
    }

    /// <summary>
    /// Initialize CardButton properties using values from the attribute bag.
    /// </summary>
    /// <param name="ve">The object to initialize.</param>
    /// <param name="bag">The attribute bag.</param>
    /// <param name="cc">The creation context; unused.</param>
    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
      base.Init(ve, bag, cc);
      CardButton cardButton = (CardButton)ve;

      cardButton.labelText = _labelText.GetValueFromBag(bag, cc);
      cardButton.disabled = _disabled.GetValueFromBag(bag, cc);
    }
  }

  /// <summary>
  /// The name of the CardButton class.
  /// </summary>
  private readonly string _className = "card__button";

  /// <summary>
  /// The name of the CardButton label class.
  /// </summary>
  private readonly string _classNameLabel = "card__button-label";
  
  /// <summary>
  /// The label of the button.
  /// </summary>
  private readonly Label _label = new Label();

  /// <summary>
  /// The name of the label rich text style.
  /// </summary>
  private readonly string _styleNameLabel = "LabelLarge";
  
  /// <summary>
  /// The resource path to the corresponding stylesheet for the CardButton.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Card/CardButton";
  
  /// <summary>
  /// Whether or not the button is disabled.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public bool disabled {
    get => _disabled;
    set {
      _disabled = value;
      SetEnabled(!value);
    }
  }

  /// <summary>
  /// The label text of the CardButton.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public string labelText {
    get => _labelText;
    set {
      _labelText = value;
      _label.text = $"<style=\"{_styleNameLabel}\">{value}</style>";

      text = "";
    } 
  }
  
  /// <summary>
  /// Whether or not the button is disabled.
  /// </summary>
  private bool _disabled;

  /// <summary>
  /// The label text of the CardButton.
  /// </summary>
  private string _labelText;
  
  /// <summary>
  /// Initializes and returns an instance of a CardButton.
  /// </summary>
  public CardButton() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
    
    _label.AddToClassList(_classNameLabel);
    hierarchy.Add(_label);
  }
}