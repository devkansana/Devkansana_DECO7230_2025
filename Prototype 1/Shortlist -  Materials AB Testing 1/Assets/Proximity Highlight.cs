using UnityEngine;

public class ProximityHighlighter : MonoBehaviour
{
    public Transform player;           // Assign player Transform in Inspector
    public float highlightDistance = 3f;
    public Color highlightColor = Color.yellow;
    public GameObject panel;           // UI button panel to show/hide

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        if (panel != null)
            panel.SetActive(false);   // Hide panel initially
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= highlightDistance)
        {
            rend.material.color = highlightColor;
            if (panel != null)
                panel.SetActive(true);  // Show panel when player near
        }
        else
        {
            rend.material.color = originalColor;
            if (panel != null)
                panel.SetActive(false); // Hide panel when player far
        }
    }
}
