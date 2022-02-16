using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrollingTexture : MonoBehaviour
{
    public float speed=0.01f;
    private float _value;
    private Renderer _spriteRenderer;
    private Material _material;

    private void Start()
    {
        _spriteRenderer = GetComponent<Renderer>();
        _material = _spriteRenderer.material;
    }

    public void SetRenderTexture(Texture2D texture)
    {
        _material.mainTexture = texture;
    }

    private void Update()
    {
        _value += speed*Time.deltaTime;
        if (_value >= 10)
        {
            _value%= 10;
        }
        _material.mainTextureOffset=new Vector2(_value,_value);
    }
}
