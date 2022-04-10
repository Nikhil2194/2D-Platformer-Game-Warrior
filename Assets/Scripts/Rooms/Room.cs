using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;  //cotains all the enemy
    private Vector3[] initialPosition;  //contain intial position of enemies


    private void Awake()
    {
        //save initila position of the enemies
        initialPosition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }

    public void ActivateRoom (bool _status)
    {
        //Actiave / deactive enemies

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i]  != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
