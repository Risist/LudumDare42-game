using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventPhysicsDestruction : MonoBehaviour {

    public bool active = true;
    // explosion data
    public float forceBase;
    public float forceScale;
    public float forceMax = 4500;
    public float explosionRadius;
    // remove & fadeout data
    public float removeDelay;
    // physics data
    public float mass;
    public float linearDamping;
    public float angularDamping;
    public Vector3 dmgPointOffset;


    public void OnDeath(HealthController.DamageData data)
    {
        if (!active)
            return;

        Vector3 explosionPosition = transform.position + dmgPointOffset;
        if (data.causer)
            explosionPosition = data.causer.transform.position + dmgPointOffset;

        var sprites = GetComponentsInChildren<Renderer>();
        foreach (var it in sprites)
        {
            it.transform.parent = null;
            var rb = it.GetComponent<Rigidbody>();

            if (!rb)
                rb = it.gameObject.AddComponent<Rigidbody>();

            rb.mass = mass;
            rb.drag = linearDamping;
            rb.angularDrag = angularDamping;
            rb.AddExplosionForce(Mathf.Clamp(forceBase - forceScale * data.damage.toFloat(), -forceMax, forceMax), explosionPosition, explosionRadius);

            var remove = it.gameObject.AddComponent<RemoveAfterDelay>();
            remove.timer = new Timer(removeDelay);

            /*var fader = it.gameObject.AddComponent<SpriteFader>();
            fader.changeRate = -it.color.a / removeDelay;
            fader.reverseTimer = new Timer();
            fader.reverse = false;*/
        }
        Destroy(this);
    }
}
