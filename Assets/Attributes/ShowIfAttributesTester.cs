using Kiwi.Editor.Attributes;
using UnityEngine;

namespace Kiwi.Data
{
    [DisallowMultipleComponent]
    public class ShowIfAttributesTester : MonoBehaviour
    {
        enum TestEnum
        {
            ValueA,
            ValueB,
            ValueC
        }

        [Header("Conditional Values")]
        [SerializeField] TestEnum testEnum;
        [SerializeField] int testInt;

        [Header("Tests")]
        [ShowIf(nameof(AlwaysTrue))]
        [SerializeField] float alwaysShow;
        [ShowIf(nameof(testEnum), TestEnum.ValueA)]
        [SerializeField] int showIfValueA;
        [ShowIf(nameof(testEnum), TestEnum.ValueB)]
        [SerializeField] float showIfValueB;
        [ShowIf(nameof(ShowIfLessThan10))]
        [SerializeField] float showIfLessThan10;
        [ShowIf(nameof(AlwaysTrue), nameof(ShowIfLessThan20))]
        [SerializeField] float showIfLessThan20;

        bool AlwaysTrue()
        {
            return true;
        }

        bool ShowIfLessThan10()
        {
            return testInt < 10;
        }

        bool ShowIfLessThan20()
        {
            return testInt < 20;
        }
    }
}