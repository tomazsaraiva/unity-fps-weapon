using UnityEngine;

public class Weapon : MonoBehaviour 
{
    // consts
    const string INPUT = "Fire1";
    const float COOLDOWN = 0.1f;



    // serializables

    [SerializeField]
    GameObject _bulletHole;



    // fields

    WeaponEffects _effects;

    RaycastHit _hit;
    Ray _ray;

    float _lastShotTime;
    Vector3 _initialPosition;



    // MonoBehaviour

	void Awake () 
    {
        _effects = GetComponent<WeaponEffects>();
	}

    void Start()
    {
        _initialPosition = transform.localPosition;
    }
	
	void Update () 
    {
        if(Input.GetButton(INPUT) && Time.time > _lastShotTime + COOLDOWN) 
        {
            if(_effects != null)
            {
                _effects.Play();   
            }

            Vector2 screenCenterPoint = new Vector2(Screen.width/2, Screen.height/2);

            _ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if(Physics.Raycast(_ray, out _hit, Camera.main.farClipPlane) && _bulletHole != null) 
            {
                Vector3 bulletHolePosition = _hit.point + _hit.normal * 0.01f;

                Quaternion bulletHoleRotation = Quaternion.FromToRotation(-Vector3.forward, _hit.normal);

                GameObject hole = GameObject.Instantiate(_bulletHole, bulletHolePosition, bulletHoleRotation);
                hole.transform.SetParent(_hit.transform);
            }

            _lastShotTime = Time.time;

            transform.localPosition = _initialPosition + new Vector3(RandomCoordinate(), RandomCoordinate(), RandomCoordinate());
        }
        else if(transform.localPosition != _initialPosition)
        {
            transform.localPosition = _initialPosition;
        }
	}

    float RandomCoordinate()
    {
        return Random.Range(-0.01f, 0.01f);
    }
}