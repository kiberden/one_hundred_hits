using UnityEngine;
using System.Collections;

public class HealsBar : MonoBehaviour
{
    private Transform[] harts = new Transform[5];

   private CharacterController hero;


    private void Awake()
    {
        hero = FindObjectOfType<CharacterController>();

        for (int i = 0; i < harts.Length; i++)
        {
            harts[i] = transform.GetChild(i);
            
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < harts.Length; i++)
        {
            if (i < hero.Heals) harts[i].gameObject.SetActive(true);
            else harts[i].gameObject.SetActive(false);
        }
    }
}
