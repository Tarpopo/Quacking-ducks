using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxLayer : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _startPosition;
    [SerializeField]private float _paralaxFactor;
    private void Start()
    {
        _transform = transform;
        _startPosition = _transform.localPosition;
    }
    
    public void MoveLayer(float xCameraPosition)
    {
        var localPosition = _transform.localPosition;
        localPosition = new Vector3(_startPosition.x + xCameraPosition * _paralaxFactor, localPosition.y, localPosition.z);
        _transform.localPosition = localPosition;
    }
}
