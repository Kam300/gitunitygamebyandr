using UnityEngine;
using System.Collections.Generic;

public class MapRevealer : MonoBehaviour
{
    public SpriteRenderer mapRenderer;
    public List<GameObject> regions;
    public Material uncoveredMaterial;

    void Start()
    {
        foreach (GameObject region in regions)
        {
            region.AddComponent<BoxCollider2D>();
            region.GetComponent<SpriteRenderer>().material = uncoveredMaterial;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject region = other.gameObject;
            region.GetComponent<SpriteRenderer>().material = uncoveredMaterial;
            // Optionally remove Collider:
            // Destroy(region.GetComponent<Collider2D>());
        }
    }
}
