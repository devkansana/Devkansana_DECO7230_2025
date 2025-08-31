using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class Outline : MonoBehaviour
{
    public enum Mode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    public Mode OutlineMode;
    public Color OutlineColor = Color.white;
    public float OutlineWidth = 2f;

    private Renderer[] renderers;
    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;

    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
    private static readonly int OutlineWidthID = Shader.PropertyToID("_OutlineWidth");

    private void Awake()
    {
        // Load materials
        outlineMaskMaterial = new Material(Shader.Find("Custom/OutlineMask"));
        outlineFillMaterial = new Material(Shader.Find("Custom/OutlineFill"));

        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnEnable()
    {
        foreach (var rend in renderers)
        {
            // Add outline materials
            var materials = new List<Material>(rend.sharedMaterials);
            materials.Add(outlineMaskMaterial);
            materials.Add(outlineFillMaterial);
            rend.materials = materials.ToArray();
        }
    }

    private void OnDisable()
    {
        foreach (var rend in renderers)
        {
            // Remove outline materials
            var materials = new List<Material>(rend.sharedMaterials);
            materials.Remove(outlineMaskMaterial);
            materials.Remove(outlineFillMaterial);
            rend.materials = materials.ToArray();
        }
    }

    private void Update()
    {
        outlineFillMaterial.SetColor(OutlineColorID, OutlineColor);
        outlineFillMaterial.SetFloat(OutlineWidthID, OutlineWidth);
    }
}
