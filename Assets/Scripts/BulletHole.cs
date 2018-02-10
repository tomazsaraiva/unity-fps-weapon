using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour 
{
    [SerializeField]
    float _duration;

    IEnumerator Start () 
    {
        yield return new WaitForSeconds(_duration);

        Destroy(gameObject);
	}
}
