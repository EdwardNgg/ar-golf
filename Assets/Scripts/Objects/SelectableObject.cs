using UnityEngine;

public class SelectableObject : MonoBehaviour {
  private bool _selected;
  
  public void Select(bool isSelect) {
    for (int i = 0; i < transform.childCount; i++) {
      transform.GetChild(i).gameObject.SetActive(isSelect);
    }
  }
}