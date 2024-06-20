using System.Collections.Generic;
using UnityEngine;

public class ArtifactModifiers : Artifact
{
    [SerializeField] List<CharacterStat> statsModifier = new List<CharacterStat>();
    protected override void OnGetItem(GameObject go)
    {
        Debug.Log("실행");
        CharacterStatHandler statHandler = go.GetComponent<CharacterStatHandler>();

        foreach (CharacterStat modifier in statsModifier)
        {
            statHandler.AddStatModifier(modifier);
        }
    }
}