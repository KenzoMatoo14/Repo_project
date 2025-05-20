using System.Collections;
using UnityEngine;

public class RoomSoundDetector : MonoBehaviour
{
    public GameObject burger;

    private BurgerLogic bl;
    private float timer = 2f;
    private bool canBeep = false; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bl = burger.GetComponent<BurgerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0 && canBeep) {
            bl.beep();
            timer = 2f;
        }

        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            canBeep = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            canBeep = false;
        }
    }
}
