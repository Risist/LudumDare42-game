using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{

    public GameObject[] anotherUI;

    private void Awake()
    {
        Time.timeScale = 0;
        foreach (var game in anotherUI)
        {
            game.SetActive(false);
        }
    }

   public void Accept()
    {
        foreach (var game in anotherUI)
        {
            game.SetActive(true);
        }
        Time.timeScale = 1;
        Destroy(gameObject,0.2f);
    }
}
