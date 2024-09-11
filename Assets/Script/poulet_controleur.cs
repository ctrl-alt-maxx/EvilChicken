using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class poulet_controleur : MonoBehaviour
{
    private Animator animatorPoulet;
    private Rigidbody2D rigidbody;

    private float controleX;
    private float controleY;

    [SerializeField] private float quantiteForce;
    [SerializeField] private float vitesseMaximum;
    [SerializeField] private GameObject fadeout;

    private bool _peutMarcher = true;
    private bool _isDead = false;
    public bool _isCelebration = false;
    private Vector2 _mouvementCelebration;
    private Vector2 randomCelebrationDirection;

    IEnumerator _Picosser;
    IEnumerator _Celebrer;

    // Start is called before the first frame update
    void Start()
    {
        animatorPoulet = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        _Picosser = Picosser();
        StartCoroutine(_Picosser);

        _Celebrer = Celebrer();

    }

    // Update is called once per frame
    void Update()
    {
        if(!_isCelebration) 
        {
            controleX = Input.GetAxis("Horizontal");
            controleY = Input.GetAxis("Vertical");
        } 
     
        Vector2 direction = new Vector2 (controleX, controleY);
        float longueurDirection = direction.magnitude;
        if (longueurDirection >= 0.1f && _peutMarcher && !_isCelebration)
        {
            Vector2 directionAssainie = ForceAnimationVirtualJoystick.ForceDirectionAxe(direction);
            animatorPoulet.SetFloat("MouvementX", directionAssainie.x);
            animatorPoulet.SetFloat("MouvementY", directionAssainie.y);
        }
        float vitesse = rigidbody.velocity.magnitude;
        animatorPoulet.SetFloat("Vitesse", vitesse);
    }

    private void FixedUpdate()
    {
        if(_isCelebration)
        {
            controleX = _mouvementCelebration.normalized.x;
            controleY = _mouvementCelebration.normalized.y;
            if (_mouvementCelebration != Vector2.zero)
            {
                animatorPoulet.SetFloat("MouvementX", controleX);
                animatorPoulet.SetFloat("MouvementY", controleY);
            }
        }
        if (_peutMarcher || _isCelebration)
        {
            float vitesseActuelle = rigidbody.velocity.magnitude;
            if(vitesseActuelle < vitesseMaximum)
            {
                rigidbody.AddForce(new Vector2(controleX, controleY) * quantiteForce);

            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void Mort()
    {
        if (!_isCelebration)
        {
            _peutMarcher = false;
            animatorPoulet.SetBool("mort", true);
            _isDead = true;
        }

    }

    private void Celebration()
    {
        _isCelebration = true;
        _peutMarcher = false;
        randomCelebrationDirection = Random.insideUnitCircle.normalized;
        StopCoroutine(_Picosser);
        StartCoroutine(_Celebrer);

    }

    private IEnumerator Celebrer()
    {
        animatorPoulet.SetBool("picosser", true);
        while (true)
        {
            _mouvementCelebration = quantiteForce * randomCelebrationDirection;
            yield return new WaitForSeconds(0.5f);
            _mouvementCelebration = Vector2.zero;
            yield return new WaitForSeconds(3);
            _mouvementCelebration = quantiteForce * -randomCelebrationDirection;
            yield return new WaitForSeconds(0.5f);
            _mouvementCelebration = Vector2.zero;
            yield return new WaitForSeconds(3);
        }

    }

    private IEnumerator Picosser()
    {
        while (true)
        {
            animatorPoulet.SetBool("picosser", false);
            float rnd = Random.Range(2, 8);
            yield return new WaitForSeconds(rnd);
            rnd = Random.Range(2, 8);
            animatorPoulet.SetBool("picosser", true);
            yield return new WaitForSeconds(rnd);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision);

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ennemi") && !_isCelebration)
        {
            Mort();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Oeuf"))
        {
            _peutMarcher = false;

            animatorPoulet.SetTrigger("Fondue");
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Douzaine") && !_isCelebration)
        {
            Celebration();
        }
    }

    public void onAnimationEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changerNiveau()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
