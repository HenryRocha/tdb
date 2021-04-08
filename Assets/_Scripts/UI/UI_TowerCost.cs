using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TowerCost : MonoBehaviour
{
    GameManager gm;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Custo Torre: {gm.GetTowerCost()}";
    }
}
