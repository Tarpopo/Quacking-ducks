using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class CustomButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private UnityEvent _buttonDown;
    [SerializeField] private UnityEvent _buttonUp;

    public event UnityAction ButtonDown
    {
        add => _buttonDown.AddListener(value);
        remove => _buttonDown.RemoveListener(value);
    }
    
    public event UnityAction ButtonUp
    {
        add => _buttonUp.AddListener(value);
        remove => _buttonUp.RemoveListener(value);
    }
    
    private SpriteRenderer _image;
    
    [SerializeField] private Sprite _whenPressed;
    [SerializeField] private Sprite _fullButton;

    private void Start()
    {
        _image = GetComponent<SpriteRenderer>();
        _image.sprite = _fullButton;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _whenPressed;
        _buttonDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _fullButton;
        _buttonUp?.Invoke();
    }
}
