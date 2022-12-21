using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private Image _canvas;
    [SerializeField] private ScrollingTextureItem[] _scrollingTextures;
    [SerializeField] private float _speed = 0.2f;
    private float _value;
    private Image _image;
    private Material _material;

    [Button]
    private void Sort()
    {
        _scrollingTextures = _scrollingTextures.SortByEnum().ToArray();
    }

    private void Awake()
    {
        Sort();
        _image = GetComponent<Image>();
        _material = _image.material;
    }

    private void Start() => Toolbox.Get<Signals>().Get<OnCharacterSelected>().AddListener(SetDuckTexture);

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

    private void SetDuckTexture(Ducks duck)
    {
        _material.mainTexture = _scrollingTextures[(int)duck].Texture;
        _canvas.RecalculateMasking();
    }
}

[Serializable]
public class ScrollingTextureItem : IEnum
{
    public Enum EnumValue => _duck;
    public Texture2D Texture => _texture;

    [SerializeField] private Ducks _duck;
    [SerializeField] private Texture2D _texture;
}