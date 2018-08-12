using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Only GameObjects with this component can be perceived by Ai
 * composes all the data agents need to react to the perceived object
 */
public class AiPerceiveUnit : MonoBehaviour
{
    public bool land;
    public bool water;
    public bool port;

    /// <summary>
    ///  references to useful data
    /// </summary>
    public AiFraction fraction;
	public HealthController health;
	public AiUnitMind mind;
	public AiMovement movement;

	protected void Start()
	{
		if (!fraction)
			fraction = GetComponent<AiFraction>();

		if (!health)
			health = GetComponent<HealthController>();

		if (!mind)
			mind = GetComponent<AiUnitMind>();

		if (!movement)
			movement = GetComponent<AiMovement>();
	}
}
