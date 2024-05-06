//-----------------------------------------------------------------------
//  Tabs.cs
//  AR Golf
//
//  Created by Edward Ng on 05/02/2024.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The Tabs custom control is a container that holds a group of tabs.
/// </summary>
public class Tabs : Box {

  /// <summary>
  /// The UXML factory that instantiates Tabs using the data read from a UXML file.
  /// </summary>
  public new class UxmlFactory : UxmlFactory<Tabs, UxmlTraits> {

    /// <summary>
    /// Returns the type name of Tabs.
    /// </summary>
    public override string uxmlName => "Tabs";

    /// <summary>
    /// Returns the namespace name of Tabs.
    /// </summary>
    public override string uxmlNamespace => "Material.Tab";

    /// <summary>
    /// Returns the type fully qualified name of Tabs.
    /// </summary>
    public override string uxmlQualifiedName => $"{uxmlNamespace}.{uxmlName}";
  }

  /// <summary>
  /// Defines UxmlTraits for Tabs.
  /// </summary>
  public new class UxmlTraits : Box.UxmlTraits {

    /// <summary>
    /// Describes the types of element that can appear as children of this element in a UXML file.
    /// </summary>
    public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
      get {
        yield return new UxmlChildElementDescription(typeof(Tab));
      }
    }
  }

  /// <summary>
  /// The name of the Tabs class.
  /// </summary>
  private readonly string _className = "tabs";

  /// <summary>
  /// The resource path to the corresponding stylesheet for Tabs.
  /// </summary>
  private readonly string _styleSheetPath = "Style Sheets/Material/Tab/Tabs";

  /// <summary>
  /// The current tab that is active.
  /// </summary>
  private Tab _activeTab => this.Q<Tab>(className: "tab--active");

  /// <summary>
  /// Initializes and returns an instance of Tabs.
  /// </summary>
  public Tabs() {
    StyleSheet styleSheet = Resources.Load<StyleSheet>(_styleSheetPath);
    styleSheets.Add(styleSheet);
    
    AddToClassList(_className);
  }

  /// <summary>
  /// Sets a tab active using its tab name.
  /// </summary>
  /// <param name="tabName">The name of the tab to set active.</param>
  public void SetActive(string tabName) {
    Tab activeTab = _activeTab;
    if (activeTab != null) {
      _activeTab.active = false;
    }

    this.Q<Tab>(tabName).active = true;
  }
}