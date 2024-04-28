using UnityEngine;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _button;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("Play") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayClick);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayClick);
    }

    private void OnPlayClick(ClickEvent evt)
    {
        Debug.Log("You Pressed Play");
    }
}
