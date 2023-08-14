using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPlate : MonoBehaviour
{
    [Serializable]
    public struct Craft
    {
        public List<string> craftObjects;
        public GameObject craftObject;
        //public int craftCount;
        //public int craftMaxCount;
    }

    public Craft[] crafts;

    public GameObject Check(List<GameObject> objs)
    {
        List<string> objTags = new List<string>();

        foreach (var obj in objs)
        {
            objTags.Add(obj.tag);
        }

        foreach (var craft in crafts)
        {
            // print(craft.craftObjects);
            //if (craft.craftObjects == objTags)
            //{
            //    return craft.craftObject;
            //}

            if (craft.craftObjects.Count != objTags.Count)
            {
                continue;
            }

            bool isMatching = true;
            for (int i = 0; i < craft.craftObjects.Count; i++)
            {
                if (craft.craftObjects[i] != objTags[i])
                {
                    isMatching = false;
                    break;
                }
            }

            if (isMatching)
            {
                return craft.craftObject;
            }
        }

        return null;
    }
}
