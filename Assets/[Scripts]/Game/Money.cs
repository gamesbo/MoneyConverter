using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(PlayerController.instance.moneyDestroyTime);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * PlayerController.instance.moneySpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();
        LuxCar luxCar = other.GetComponent<LuxCar>();
        Door door = other.GetComponent<Door>();

        if (car)
        {
            if (car.carLevel > 0)
            {
                car.carLevel--;
            }
            Destroy(gameObject);
        }
        if (luxCar)
        {
            if (luxCar.carLevel > 0)
            {
                luxCar.carLevel--;
            }
            Destroy(gameObject);
        }
        if (door)
        {
            door.value++;
        }
        
        if (other.CompareTag("MoneyBag"))
        {
            EKTemplate.GameManager.instance.AddMoney(50);
            PlayerController.instance.gunLevel += 3;
            PlayerController.instance.gunLevelText.transform.DOScale(0.75f, 0.2f).OnComplete(() =>
            {
                PlayerController.instance.gunLevelText.transform.DOScale(0.5f, 0.25f);
                PlayerController.instance.gunLevelText.color = new Color(0.4f, 1, 0.4f, 1);
            });
            GameObject cloud = Instantiate(Resources.Load("particles/CloudBag"), new Vector3(other.transform.position.x, other.transform.position.y + 2f,
           other.transform.position.z), Quaternion.identity) as GameObject;
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(false);
            foreach (Rigidbody rb in other.transform.GetChild(0).GetComponentsInChildren<Rigidbody>())
            {
                rb.transform.parent = null;
                rb.isKinematic = false;
                rb.AddForce(new Vector3(Random.Range(-1.75f, 1.75f), Random.Range(1f, 2.5f), Random.Range(-0.5f, .5f)) * 210f);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            
        }
    }
    
}
