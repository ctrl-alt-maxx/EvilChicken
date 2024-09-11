using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cochon : MonoBehaviour
{
    [SerializeField]
    private float _vitesse;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * _vitesse);
        gameObject.GetComponent<Animator>().SetFloat("x",direction.x);
        gameObject.GetComponent<Animator>().SetFloat("y", direction.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destructeur"))
        {
            Destroy(gameObject);
        }
    }
}
