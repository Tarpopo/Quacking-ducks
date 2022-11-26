using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private float _speed = 0.01f;
    private float _value;
    private Image _image;
    private Material _material;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _material = _image.material;
    }

    private void OnEnable() => StartCoroutine(TextureOffsetChanger());

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator TextureOffsetChanger()
    {
        while (true)
        {
            _value += _speed * Time.deltaTime;
            if (_value >= 10) _value %= 10;
            _material.mainTextureOffset = new Vector2(_value, _value);
            yield return null;
        }
    }

    public void SetRenderTexture(Texture2D texture) => _material.mainTexture = texture;
}