using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int CoinCount;
    public TextMeshProUGUI coinText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       coinText.text = CoinCount.ToString();
    }
}
