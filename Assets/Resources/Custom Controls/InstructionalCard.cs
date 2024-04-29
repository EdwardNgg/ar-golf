//-----------------------------------------------------------------------
//  InstructionalCard.cs
//  AR Golf
//
//  Created by Edward Ng on 04/28/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// An instructional card that directs the user on what to do and allows them to continue to the
/// next step.
/// </summary>
public class InstructionalCard : Card {

  /// <summary>
  /// The UXML factory that instantiates an InstructionalCard using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<InstructionalCard> {

    /// <summary>
    /// Returns the type name of InstructionalCard.
    /// </summary>
    public override string uxmlName => "InstructionalCard";
    
    /// <summary>
    /// Returns the namespace name of InstructionalCard.
    /// </summary>
    public override string uxmlNamespace => "ARGolf";

    /// <summary>
    /// Returns the type fully qualified name of InstructionalCard.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// The path to the UXML structure visual tree asset for InstructionalCard.
  /// </summary>
  private readonly string _assetPath = "Visual Tree Assets/InstructionalCard";

  /// <summary>
  /// Whether the InstructionalCard button is disabled.
  /// </summary>
  public bool Disabled {
    get => _button.disabled;
    set => _button.disabled = value;
  }

  /// <summary>
  /// The headline of the InstructionalCard.
  /// </summary>
  private CardHeadline _headline => this.Q<CardHeadline>("instructional-card-headline");

  /// <summary>
  /// The supporting text of the InstructionalCard.
  /// </summary>
  private CardSupportingText _support => this.Q<CardSupportingText>("instructional-card-text");

  /// <summary>
  /// The button of the InstructionalCard.
  /// </summary>
  private CardButton _button => this.Q<CardButton>("instructional-card-button");
  
  /// <summary>
  /// Initializes and returns an empty InstructionalCard.
  /// </summary>
  public InstructionalCard() { }

  /// <summary>
  /// Initializes and returns an InstructionalCard using the headline and supporting text. Also
  /// includes a button to continue. 
  /// </summary>
  /// <param name="headline"></param>
  /// <param name="supportingText"></param>
  public InstructionalCard(string headline, string supportingText) {
    VisualTreeAsset asset = Resources.Load<VisualTreeAsset>(_assetPath);
    asset.CloneTree(this);

    _headline.text = headline;
    _support.text = supportingText;
  }
}