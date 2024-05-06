//-----------------------------------------------------------------------
//  TabsView.cs
//  AR Golf
//
//  Created by Edward Ng on 05/02/2024.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The TabsView controls the organization of content and allows the user to switch between Build
/// and Play mode.
/// </summary>
public class TabsView : MonoBehaviour {
  
  /// <summary>
  /// The root UI document in the application.
  /// </summary>
  private UIDocument _document;

  /// <summary>
  /// Awake is called when a new instance of the behavior is created.
  /// </summary>
  private void Awake() {
    _document = GetComponent<UIDocument>();
  }

  /// <summary>
  /// OnEnable is called when the behavior becomes enabled and active. It configures the UI document
  /// to render tabs.
  /// </summary>
  private void OnEnable() {
    VisualElement body = _document.rootVisualElement.Q<VisualElement>(className: "body");

    Tabs tabs = new Tabs();
    tabs.Add(new Tab("Build", "cube-alt"));
    tabs.Add(new Tab("Play", "play"));
    tabs.SetActive("Build");
    
    body.Add(tabs);
  }
}