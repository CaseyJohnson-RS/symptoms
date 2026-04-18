using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpriteData", menuName = "Scriptable Objects/SpriteData")]
public class SpriteData : ScriptableObject
{

    [Serializable]
    public struct BodyPart
    {
        public string name;
        public List<Sprite> ill;
        public List<Sprite> healthy;
    }

    public List<BodyPart> bodyParts;

    public Sprite GetSprite(string bodyPartName, bool ill)
    {
        System.Random rnd = new System.Random();
        foreach(var bp in bodyParts)
        {
            if (bodyPartName == bp.name)
            {
                if (ill)
                {
                    int index = rnd.Next(bp.ill.Count);
                    return bp.ill[index];
                }
                else
                {
                    int index = rnd.Next(bp.healthy.Count);
                    return bp.healthy[index];
                }
            } 
        }
        return null;
    }
}
