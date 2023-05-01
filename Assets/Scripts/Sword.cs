using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 20.0f;

    private void OnCollisionEnter(Collision col)
    {
        col.rigidbody.AddForce( - col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
        col.rigidbody.useGravity = true;

    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    public void GrabSword(ObjectAnchor sword)
    {
        for (int i = 0; i < sword.collisionBoxes.Length; i++) { sword.collisionBoxes[i].enabled = true; }

    }
}
