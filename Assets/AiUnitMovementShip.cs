using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiUnitMovementShip : UnitMovement {

    

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

    new protected bool AutoTargetSelection()
    {
        if (perception)
            foreach (var it in perception.memory)
            {
                bool hasHealth = it.unit.health;
                bool validFraction = !it.unit.fraction ||
                    (it.unit.fraction.gameObject != gameObject &&
                    fraction.GetAttitude(it.unit.fraction.fractionName) == AiFraction.Attitude.enemy);

                bool canMove = canMoveOnLand && it.unit.land;
                canMove |= canMoveOnWater && it.unit.water;
                canMove |= canAttackOnLand && hasHealth && it.unit.land;
                canMove |= it.unit.port;

                if (canMove && hasHealth && validFraction)
                {

                    hpAim = it.unit.health;
                    aim = it.unit.transform.position;
                    aim.y = body.position.y;
                    movingToAim = true;
                    return true;
                }
            }
        return false;
    }


}
