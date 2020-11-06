using System;
using UnityEngine;
using UnityEngine.Assertions;


public class Parallax : MonoBehaviour
{
    public Vector2 parallaxEffect = Vector2.one;
    public bool infiniteHorizontal = true;
    public bool infiniteVertical = false;
    
    [Space]
    public bool addVerticalMovement = false;
    public float vercticalMovement = 1f;
    
    [Space]
    public bool addHorizontalMovement = false;
    public float horizontalMovement = 1f;

    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeX;
    private float _textureUnitSizeY;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Camera.main, "Scene need a camera");
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        Vector3 localScale = transform.localScale;
        _textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * localScale.x;
        _textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * localScale.y;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        
        if(addVerticalMovement)
        {
            deltaMovement += vercticalMovement * Time.deltaTime * Vector3.up;
        }
        
        if(addHorizontalMovement)
        {
            deltaMovement += horizontalMovement * Time.deltaTime * Vector3.left;
        }

        transform.position += new Vector3(deltaMovement.x * parallaxEffect.x, deltaMovement.y * parallaxEffect.y);
        _lastCameraPosition = _cameraTransform.position;

        if (infiniteHorizontal && Math.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        
        if (infiniteVertical && Math.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
        {
            float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(_cameraTransform.position.x,_cameraTransform.position.y + offsetPositionY);
        }
    }
}
