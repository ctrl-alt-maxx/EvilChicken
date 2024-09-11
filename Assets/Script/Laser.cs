using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private VolumetricLines.VolumetricLineBehavior _Laser;

    private bool _EstArrete = false;

    [SerializeField]
    private Transform _EmetteurLaser;

    [SerializeField]
    private float _LongueurLaser = 100.0f;


    void Start()
    {
        _Laser = GetComponent<VolumetricLines.VolumetricLineBehavior>();
        _Laser.StartPos = Vector3.zero;
        _Laser.EndPos = new Vector3(0.0f, _LongueurLaser, 0.0f);
    }

    private void Update()
    {
        Vector3 direction = transform.up;

        int layerMask = LayerMask.GetMask(new[] { "Obstacle", "Joueur" });
        RaycastHit2D collision = Physics2D.Raycast(_EmetteurLaser.transform.position, direction, _LongueurLaser, layerMask);
        _EstArrete = collision.collider != null;
        Debug.DrawRay(_EmetteurLaser.transform.position, direction * _LongueurLaser, _EstArrete ? Color.green : Color.gray);
        _Laser.EndPos = new Vector3(0.0f, _EstArrete ? collision.distance : _LongueurLaser, 0.0f);
        if(collision.collider.gameObject.GetComponent<poulet_controleur>() != null)
        {
            collision.collider.gameObject.GetComponent<poulet_controleur>().Mort();
        }

    }
}