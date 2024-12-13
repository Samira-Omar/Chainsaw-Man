// Edited by Fadumo
using UnityEngine;

public class LanternPickup : MonoBehaviour
{
    private GameObject currentItem;
    public GameObject handInteractionUI;
    public GameObject lanternObject;

    private bool isPlayerNearby;

    void Start()
    {
        currentItem = gameObject;

        handInteractionUI.SetActive(false);
        lanternObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isPlayerNearby = true;
            handInteractionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isPlayerNearby = false;
            handInteractionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetButtonDown("Interact"))
        {
            HandleLanternPickup();
        }
    }

    private void HandleLanternPickup()
    {
        handInteractionUI.SetActive(false);
        lanternObject.SetActive(true);
        StartCoroutine(RemoveItemAfterDelay());
    }

    private IEnumerator RemoveItemAfterDelay()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(currentItem);
    }

