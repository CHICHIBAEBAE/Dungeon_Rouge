using UnityEngine;

public abstract class Artifact : MonoBehaviour
{
    public void UseArtifact(GameObject go)
    {
        OnGetItem(go);
    }

    protected abstract void OnGetItem(GameObject gameObject);
}
