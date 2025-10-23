using UnityEngine;

public class GhostCubeActivator : MonoBehaviour
{
    public GameObject ghostCube; // Assign this in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzlePiece") || other.CompareTag("Hand"))
        {
            ghostCube.SetActive(true); // Show the ghost cube
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzlePiece") || other.CompareTag("Hand"))
        {
            ghostCube.SetActive(false);
        }
    }
}
