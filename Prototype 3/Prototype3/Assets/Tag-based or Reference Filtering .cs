using UnityEngine;
using Oculus.Interaction;
using System.Linq;

public class SnapPairFilter : MonoBehaviour
{
    [Tooltip("Assign the only SnapInteractor allowed to connect to this SnapInteractable.")]
    public SnapInteractor allowedInteractor;

    private SnapInteractable snapInteractable;

    private void Awake()
    {
        snapInteractable = GetComponent<SnapInteractable>();

        if (snapInteractable == null)
        {
            Debug.LogError("SnapInteractable not found on this GameObject.");
            return;
        }

        // Subscribe to the event triggered when an interactor enters the interactable
        snapInteractable.WhenInteractorViewAdded += OnSnapAttempt;
    }

    private void OnDestroy()
    {
        if (snapInteractable != null)
        {
            // Always unsubscribe from events on destroy to prevent leaks
            snapInteractable.WhenInteractorViewAdded -= OnSnapAttempt;
        }
    }

    // Triggered each time an interactor tries to snap
    private void OnSnapAttempt(IInteractorView interactorView)
    {
        var interactor = interactorView as SnapInteractor;

        if (interactor == null)
        {
            Debug.LogWarning("An invalid or null interactor attempted to snap.");
            return;
        }

        if (interactor != allowedInteractor)
        {
            Debug.Log("Snap rejected: Unauthorized SnapInteractor attempted to connect.");
            RemoveUnallowedInteractor(interactorView);
        }
        else
        {
            Debug.Log("Snap accepted: Allowed SnapInteractor connected.");
        }
    }

    // Remove the interactor if it is not allowed
    private void RemoveUnallowedInteractor(IInteractorView interactorView)
    {
        var interactorList = snapInteractable.SelectingInteractorViews.ToList();
        if (interactorList.Contains(interactorView))
        {
            interactorList.Remove(interactorView);
            Debug.Log("Unallowed interactor removed from interaction list.");
        }
    }
}
