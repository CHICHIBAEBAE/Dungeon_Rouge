using UnityEngine;

public class MapConnect : MonoBehaviour
{
    public GameObject content;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (content != null)
        {
            Transform[] childTransforms = content.GetComponentsInChildren<Transform>();

            lineRenderer.positionCount = childTransforms.Length - 1;

            for (int i = 1; i < childTransforms.Length; i++)
            {
                lineRenderer.SetPosition(i - 1, childTransforms[i].position);
            }
        }
    }
}