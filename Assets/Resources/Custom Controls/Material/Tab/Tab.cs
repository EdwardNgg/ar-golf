//-----------------------------------------------------------------------
//  Tab.cs
//  AR Golf
//
//  Created by Edward Ng on 05/01/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// A Tab custom control organizes content across different screens and views.
/// </summary>
public class Tab : Box {

  /// <summary>
  /// The UXML factory that instantiates a Tab using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<Tab, UxmlTraits> {

    /// <summary>
    /// Returns the type name of Tab.
    /// </summary>
    public override string uxmlName => "Tab";

    /// <summary>
    /// Returns the namespace name of Tab.
    /// </summary>
    public override string uxmlNamespace => "Material.Tab";

    /// <summary>
    /// Returns the type fully qualified name of Card.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines UxmlTraits for Tab.
  /// </summary>
  public new class UxmlTraits : Box.UxmlTraits {

    /// <summary>
    /// The active attribute of Tab.
    /// </summary>
    private readonly UxmlBoolAttributeDescription _active = new UxmlBoolAttributeDescription {
        name = "active",
        defaultValue = false
    };

    /// <summary>
    /// The icon attribute of Tab.
    /// </summary>
    private readonly UxmlStringAttributeDescription _icon = new UxmlStringAttributeDescription {
        name = "icon",
        defaultValue = ""
    };

    /// <summary>
    /// The label attribute of Tab.
    /// </summary>
    private readonly UxmlStringAttributeDescription _label = new UxmlStringAttributeDescription {
        name = "label",
        defaultValue = ""
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
    /// Initialize Tab properties using values from the attribute bag.
    /// </summary>
    /// <param name="ve">The object to initialize.</param>
    /// <param name="bag">The attribute bag.</param>
    /// <param name="cc">The creation context; unused.</param>
    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
      base.Init(ve, bag, cc);
      Tab tab = (Tab)ve;

      tab.active = _active.GetValueFromBag(bag, cc);
      tab.icon = _icon.GetValueFromBag(bag, cc);
      tab.label = _label.GetValueFromBag(bag, cc);
    }
  }

  /// <summary>
  /// Whether the tab is actively selected.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public bool active {
    get => _active;
    set {
      _active = value;
      if (value) {
        AddToClassList(_classNameActive);
      } else {
        RemoveFromClassList(_classNameActive);
      }
    }
  }

  /// <summary>
  /// The name of the icon shown in the Tab.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public string icon {
    get => _icon;
    set {
      _icon = value;
      if (_iconNames.TryGetValue(value, out var iconName)) {
        _iconLabel.text = iconName;
      }
    }
  }

  /// <summary>
  /// The label text of the Tab.
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public string label {
    get => _label;
    set => _textLabel.text = $"<style=\"{_styleNameLabel}\">{value}</style>";
  }

  /// <summary>
  /// A dictionary mapping human-readable icon names to their corresponding character in the icon
  /// fontface.
  /// </summary>
  private static readonly Dictionary<string, string> _iconNames = new Dictionary<string, string> {
    {"play", "\uEBBD"},
    {"cube-alt", "\uED71"}
  } ;
  
  /// <summary>
  /// The path to the UXML structure visual tree asset for Tab.
  /// </summary>
  private readonly string _assetPath = "Visual Tree Assets/Material/Tab/Tab";

  /// <summary>
  /// The name of the Tab class.
  /// </summary>
  private readonly string _className = "tab";

  /// <summary>
  /// The name of the active Tab class.
  /// </summary>
  private readonly string _classNameActive = "tab--active";

  /// <summary>
  /// The name of the rich text style of the Tab label.
  /// </summary>
  private readonly string _styleNameLabel = "TitleSmall";

  /// <summary>
  /// Whether the tab is actively selected.
  /// </summary>
  private bool _active;
  
  /// <summary>
  /// The name of the icon shown in the Tab.
  /// </summary>
  private string _icon;

  /// <summary>
  /// The label corresponding to the icon in Tab.
  /// </summary>
  private Label _iconLabel => this.Q<Label>("tab-icon");

  /// <summary>
  /// The label text of the Tab.
  /// </summary>
  private string _label;

  /// <summary>
  /// The label corresponding to the text in Tab.
  /// </summary>
  private Label _textLabel => this.Q<Label>("tab-label");

  /// <summary>
  /// Initializes and returns an instance of Tab.
  /// </summary>
  public Tab() {
    VisualTreeAsset asset = Resources.Load<VisualTreeAsset>(_assetPath);
    asset.CloneTree(this);
    
    AddToClassList(_className);
  }

  
  public Tab(string label, string icon) {
    VisualTreeAsset asset = Resources.Load<VisualTreeAsset>(_assetPath);
    asset.CloneTree(this);
    
    AddToClassList(_className);
    
    this.label = label;
    this.icon = icon;
    name = label;
  }
}