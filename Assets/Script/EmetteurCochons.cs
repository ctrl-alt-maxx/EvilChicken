using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmetteurCochons : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabCochon;
    [SerializeField]
    private float _minInterval;
    [SerializeField]
    private float _maxInterval;
    [SerializeField]
    private Vector2 direction;

    private float lastTick = 0;
    private float nextInterval = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastTick + nextInterval)
        {
            lastTick = Time.time;
            nextInterval = Random.Range(_minInterval, _maxInterval);
            Instantiate(prefabCochon, transform.position,Quaternion.identity).GetComponent<Cochon>().direction = direction ;
        }
    }
}
