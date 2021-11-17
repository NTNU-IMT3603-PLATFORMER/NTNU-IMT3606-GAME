using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class amountOfSpirits : MonoBehaviour
{
    GameObject _player;
    public Text spirits;


    private void Start()
    {
        _player = PlayerEntity.INSTANCE.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        spirits.text = _player.GetComponent<Entity>().spirits.ToString();
    }
}
