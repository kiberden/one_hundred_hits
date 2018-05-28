using UnityEngine;
using System.Collections;


public class InteractiveObject : MonoBehaviour
{
    [SerializeField]
    Canvas messageCanvas;
    private Animator anim; //Анимация
    [SerializeField]
    private GameObject babls;


    void Start()
    {
        this.anim = GetComponent<Animator>();// для анимации, но это как обычно
        messageCanvas.enabled = false;
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "hero")

        {
            babls.SetActive(true);
        }

        {

            TurnOnMessage();
        }
       
        
    }

    private void TurnOnMessage()
    {
        messageCanvas.enabled = true;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "hero")
        {
            babls.SetActive(false);
            TurnOffMessage();
            
        }
    }

    private void TurnOffMessage()
    {
        messageCanvas.enabled = false;
    }

  }
