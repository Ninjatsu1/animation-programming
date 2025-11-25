using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDebugger : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PointerClick received on: " + gameObject.name);
    }
}
