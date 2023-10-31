using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Button2D : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private UnityEvent _onClick;
    
    private void OnMouseOver()
    {
        ToggleHighlight(true);
    }

    private void OnMouseExit()
    {
        ToggleHighlight(false);
    }

    private void OnMouseUp()
    {
        _onClick?.Invoke();
    }
    
    private void ToggleHighlight(bool toggle)
    {
        if (_highlight == null) return;
        
        _highlight.SetActive(toggle);
    }

}
