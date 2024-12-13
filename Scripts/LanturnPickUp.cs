// Winta Edited

using UnityEngine;
using System.Collections;

public class LanternPickup : MonoBehaviour
{
    private GameObject heldItem;
    public GameObject interactionUI;
    public GameObject lanternPrefab;

    private bool playerIsNearby;

    void Start()
    {
        heldItem = gameObject;

        interactionUI.SetActive(false);
        lanternPrefab.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Reach"))
        {
            playerIsNearby = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Reach"))
        {
            playerIsNearby = false;
            interactionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerIsNearby && Input.GetButtonDown("Interact"))
        {
            PickupLantern();
        }
    }

    private void PickupLantern()
    {
        interactionUI.SetActive(false);
        lanternPrefab.SetActive(true);
        StartCoroutine(DestroyItemAfterDelay());
    }

    private IEnumerator DestroyItemAfterDelay()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(heldItem);
    }
}
