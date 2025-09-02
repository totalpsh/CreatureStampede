using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool isPierce;
    [SerializeField] float speed;

    Rigidbody2D _rigidbody;

    private void Awake()
    {
       _rigidbody = GetComponent<Rigidbody2D>();
    }


    public void Init(float damage, bool isPierce, Vector3 dir, float speed = 0)
    {
        this.damage = damage;
        this.isPierce = isPierce;
        this.speed = speed;

        if (_rigidbody != null)
        {
            _rigidbody.velocity = dir * this.speed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Enemy"))
        //{
        //    if(!isPierce)
        //    {
        //        BulletSetActive();
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("BulletBoundary"))
        {
            BulletSetActive();
        }
    }

    void BulletSetActive()
    {
        _rigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
