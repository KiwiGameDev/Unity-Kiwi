using Kiwi.Editor.Attributes;
using UnityEngine;

namespace Kiwi.Data
{
    [DisallowMultipleComponent]
    public class EnableIfAttributesTester : MonoBehaviour
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
        [EnableIf(nameof(AlwaysTrue))]
        [SerializeField] float alwaysShow;
        [EnableIf(nameof(testEnum), TestEnum.ValueA)]
        [SerializeField] int showIfValueA;
        [EnableIf(nameof(testEnum), TestEnum.ValueB)]
        [SerializeField] float showIfValueB;
        [EnableIf(nameof(ShowIfLessThan10))]
        [SerializeField] float showIfLessThan10;
        [EnableIf(nameof(AlwaysTrue), nameof(ShowIfLessThan20))]
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