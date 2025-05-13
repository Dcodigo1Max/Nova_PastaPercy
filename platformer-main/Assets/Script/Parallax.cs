using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //[SerializeField] private Transform _background;

    //[SerializeField] private Transform _middleground;

    //[SerializeField] private Transform _playerCamera;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private ParallaxSprite[] _parallaxSprites;

    // Update is called once per frame
    void Update()
    {
        //_background.position = _playerCamera.transform.position /2f;
        //_middleground.position = _playerCamera.transform.position / 4f;


        for (int i = 0; i < _parallaxSprites.Length; i++)
        {
            // update position based on camera position
            _parallaxSprites[i].TargetSprite.transform.position =
                _playerCamera.transform.position / _parallaxSprites[i].SpeedDiv;
            //Set order in layer every frame 
            _parallaxSprites[i].TargetSprite.sortingOrder = i;
        }
    }

    [System.Serializable]
    public class ParallaxSprite
    {
        [SerializeField] private float _speedDiv = 2f;

        [SerializeField] private SpriteRenderer _targetSprite;

        public float SpeedDiv => _speedDiv;

        public SpriteRenderer TargetSprite => _targetSprite;
    }
}

