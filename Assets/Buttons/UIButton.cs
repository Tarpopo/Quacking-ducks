using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeReference] private BaseButton _baseButton;

    private void Start() => _baseButton.OnStart(gameObject);

    private void OnDisable() => _baseButton.OnDisable();

    public void OnPointerDown(PointerEventData eventData) => _baseButton.OnPointerDown(eventData);

    public void OnPointerUp(PointerEventData eventData) => _baseButton.OnPointerUp(eventData);
}