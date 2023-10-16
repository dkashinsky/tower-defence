using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> GetChildrenByTag(this Transform parent, string tag)
        {
            foreach (Transform child in parent)
            {
                if (child.gameObject.CompareTag(tag))
                    yield return child;
            }
        }
    }
}