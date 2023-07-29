using Kiwi.Editor.Attributes;
using UnityEngine;

namespace Kiwi.Data
{
    [DisallowMultipleComponent]
    public class ExpandableAttributesTester : MonoBehaviour
    {
        [SerializeField] int sampleInt;
        [SerializeField, Expandable] float sampleFloat;
        [SerializeField, Expandable] ShowIfAttributesTester showIfAttributesTester;
        [SerializeField, Expandable] EnableIfAttributesTester enableIfAttributesTester;
    }
}