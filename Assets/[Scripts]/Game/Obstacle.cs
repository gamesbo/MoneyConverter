using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour
{
    public float speed = 175f;
    private Transform parent;
    public float bounceFactor = 0.25f;
    public float bounceSpeed = 0.1f;
    private Tween tw;
    private void Start()
    {
        parent = transform.parent;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Money money = other.GetComponent<Money>();
        if (money)
        {
            if (tw != null) return;
          tw = parent.DOScale(new Vector3(parent.localScale.x + bounceFactor, parent.localScale.y + bounceFactor,
                parent.localScale.z + bounceFactor), bounceSpeed).OnComplete(() =>
               {
                   //parent.DOScale(new Vector3(parent.localScale.x - bounceFactor, parent.localScale.y - bounceFactor,
                   //  parent.localScale.z - bounceFactor), bounceSpeed);
                   tw = null;
               });
            Destroy(money.gameObject);
        }
    }
}
