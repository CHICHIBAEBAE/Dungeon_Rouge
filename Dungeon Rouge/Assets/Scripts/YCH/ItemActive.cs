using UnityEngine;

public class ItemActive : MonoBehaviour
{
    public Artifact artifact;
    public GameObject player;

    private void Start()
    {
        artifact = GetComponent<Artifact>();
    }

    private void OnMouseDown()
    {
        Debug.Log("온마우스");
        artifact.UseArtifact(player);
    }
}