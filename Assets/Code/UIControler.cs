using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public Text WoodText;
    public Text GoldText;

    private void Update()
    {
        WoodText.text ="Wood: " + PickContainer.istance.Wood;
        GoldText.text ="Gold: " + PickContainer.istance.Gold;
    }
}
