using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public Text WoodText;

    private void Update()
    {
        WoodText.text ="Wood: " + PickContainer.istance.Wood;
    }
}
