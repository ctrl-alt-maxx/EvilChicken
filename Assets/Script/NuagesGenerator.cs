using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuagesGenerator : MonoBehaviour
{
    public List<GameObject> _nuages;

    [SerializeField]
    private int _intervalApparition = 1;
    private float _lastTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= _lastTick + _intervalApparition)
        {
            _lastTick = Time.time;
            int rndNuage = Random.Range(0, _nuages.Count);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            float y = Random.Range(spriteRenderer.bounds.center.y - spriteRenderer.bounds.extents.y, spriteRenderer.bounds.center.y + spriteRenderer.bounds.extents.y);
            Instantiate(_nuages[rndNuage], new Vector2(transform.position.x,y), Quaternion.identity);
            
        }
    }
}
