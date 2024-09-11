using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuage : MonoBehaviour
{
    [SerializeField]
    private float _sizeMin = 0.1f;
    [SerializeField]
    private float _sizeMax = 1.0f;
    [SerializeField]
    private float _minVitesse;
    [SerializeField]
    private float _maxVitesse;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D CurrentRigidBody2d = GetComponent<Rigidbody2D>();
        Vector2 velocity = new(Random.Range(_minVitesse, _maxVitesse), 0);
        CurrentRigidBody2d.velocity = velocity;
        Transform CurrentTransform = GetComponent<Transform>();
        float size = Random.Range(_sizeMin, _sizeMax);
        CurrentTransform.localScale = new Vector3(size, size, 1);
        SpriteRenderer MonSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector2 EtendueSprite = MonSpriteRenderer.bounds.extents;
        CurrentTransform.localPosition = new Vector3(CurrentTransform.position.x, CurrentTransform.position.y + EtendueSprite.y, CurrentTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Killzone"))
        {
            Destroy(gameObject);
        }
    }
}
