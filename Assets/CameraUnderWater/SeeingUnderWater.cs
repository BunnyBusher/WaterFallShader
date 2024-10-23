using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingUnderWater : MonoBehaviour
{
    public static SeeingUnderWater Instance;
    private Transform _myTransform;
    [SerializeField] private float _waterheight;
    [SerializeField] private GameObject _prefab;
    private Vector3 _waterSurfacePosition;
    private bool _isUnderWater = false;
    private GameObject _waterPlane;
    [SerializeField] private float _waterPlaneOffSet;

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _myTransform = Camera.main.transform;
        _waterSurfacePosition = new Vector3(0, _waterheight, 0);
    }

    void FixedUpdate()
    {
        if (_myTransform.position.y < _waterSurfacePosition.y && _isUnderWater == false) 
        {
            _waterPlane = Instantiate(_prefab, _myTransform.position, Quaternion.identity);
            _isUnderWater=true;
        }
        else if (_myTransform.position.y > _waterSurfacePosition.y && _isUnderWater == true) 
        {
            Destroy(_waterPlane);
            _isUnderWater=false;
        }
         
    }

    private void Update()
    {
        if (_isUnderWater == true)
        {
            Vector3 newPos = _myTransform.position;
            newPos.z += _waterPlaneOffSet;
            _waterPlane.transform.position = newPos;
            _waterPlane.transform.rotation = Quaternion.Euler(_myTransform.rotation.eulerAngles.x, _myTransform.rotation.eulerAngles.y -90, _myTransform.rotation.eulerAngles.z + 90);
        }
            
    }
}
