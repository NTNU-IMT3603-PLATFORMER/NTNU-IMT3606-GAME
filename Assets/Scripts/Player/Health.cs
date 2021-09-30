using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    int numOfHearts;

    [SerializeField]
    Image[] hearts;

    [SerializeField]
    Sprite heart_full;

    [SerializeField]
    Sprite heart_empty;

    void Update() {

        if(health > numOfHearts) {

            health = numOfHearts;

        }
        for (int i = 0; i < hearts.Length; i++) {

            if(i < health) {

                hearts[i].sprite = heart_full;

            } else {

                hearts[i].sprite = heart_empty;

            }

            if(i < numOfHearts) {

                hearts[i].enabled = true;

            } else {

                hearts[i].enabled = false;
            }
        }
    }
}
