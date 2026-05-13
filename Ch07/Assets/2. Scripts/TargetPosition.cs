using UnityEngine;

public class TargetPosition : MonoBehaviour
{
   private void OnDrawGizmos()
   {
       Gizmos.DrawSphere(transform.position, 1f);
    }
}
