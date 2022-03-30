using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionZone : MonoBehaviour
{
    [SerializeField] private IRecipient _recipient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            IObjectForCollect objectCollect = player.Bag.GetItem();
            if (objectCollect != null) return;

            if (objectCollect.Type == _recipient.ObjectForCollectType)
            {
                Debug.Log("Collect");
            }

        }
    }

    

    private IEnumerator ReceptionCoroutine()
    {


        yield return null;
    }
}
