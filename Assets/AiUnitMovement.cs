using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiUnitMovement : UnitMovement {

    new void Update () {
        if (resetAimCd.isReadyRestart())
        {
            resetAimCd.cd = Random.Range(resetAimCdMin, resetAimCdMax);

            if ( !animator || !AutoTargetSelection() )
            {
                if (!randomMovement())
                    ResetAim();
            }
        }

        AtackUpdate();
    }



}
