using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualKeyboard : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_InputField _pseudoInputTMPField;
    private GameObject _activatedBtn;

    [Header("Varibales")]
    private string _outputText;

    private void Awake()
    {
        VirtualKeyboardInitialization();
    }

    public void OnLetterBtn()
    {
        _activatedBtn = EventSystem.current.currentSelectedGameObject.gameObject;
        if (_outputText.Length == 0)
            _outputText = _activatedBtn.GetComponentInChildren<TextMeshProUGUI>().text;
        else
            _outputText += _activatedBtn.GetComponentInChildren<TextMeshProUGUI>().text;
        _outputText.Trim();
        _pseudoInputTMPField.text = _outputText;
    }

    public void OnBackSpaceBtn()
    {
        if (_outputText.Length > 0)
            _outputText = _outputText.Remove(_outputText.Length - 1, 1);
        _pseudoInputTMPField.text = _outputText;
    }

    public string GetOutPutText()
    {
        return _outputText;
    }

    private void VirtualKeyboardInitialization()
    {
        _outputText = "";
    }
}
