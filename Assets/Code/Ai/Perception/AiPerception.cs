using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Detects fraction objects.
 * Not designed to detect obstacles, only aims of attack, follow, cast spells ect
 */
public class AiPerception : MonoBehaviour {
    
    public float addictionalRotation = 0.0f;
    public Timer timerPerformSearch = new Timer();
	/// how far the field of view goes
	public float searchDistance = 5.0f;
	/// how long the target is remembered
	public float memoryTime = 5.0f;

    float scanTimeBase;

	public class MemoryItem
	{
		public Timer remainedTime;
		public AiPerceiveUnit unit;
		public float lastDistance;
	}
	[System.NonSerialized]
	public List<MemoryItem> memory = new List<MemoryItem>();

    private void Start()
    {
        scanTimeBase = timerPerformSearch.cd;
        timerPerformSearch.cd = scanTimeBase;
    }

    // Update is called once per frame
    void Update ()
	{
		if(timerPerformSearch.isReadyRestart())
		{
			PerformClear();
            PerformSearch();
        }
	}

	void PerformSearch()
	{
        var rays = Physics.SphereCastAll(transform.position, searchDistance,Vector3.up);

        foreach (var it in rays)
        {
            var unit = it.collider.GetComponent<AiPerceiveUnit>();
            if (unit && unit.gameObject != gameObject)
            {
                insertToMemory(unit, it.distance);
            }
        }

		memory.Sort(delegate(MemoryItem item1, MemoryItem item2) { return item1.lastDistance.CompareTo(item2.lastDistance); });
	}
	void PerformClear()
	{
		for (int i = 0; i < memory.Count; ++i)
			if (memory[i].remainedTime.isReady())
			{
				memory.RemoveAt(i);
				--i;
			}
	}

	public void insertToMemory(AiPerceiveUnit unit, float distance)
	{
		bool bFound = false;
		foreach (var itMemory in memory)
			if (itMemory.unit == unit)
			{
				itMemory.remainedTime.restart();
				itMemory.lastDistance = distance;
				bFound = true;
				break;
			}
		if (!bFound)
		{
			var memoryItem = new MemoryItem();
			memoryItem.unit = unit;
			memoryItem.remainedTime = new Timer(memoryTime);
			memoryItem.lastDistance = distance;

			memory.Add(memoryItem);
		}
	}
}
