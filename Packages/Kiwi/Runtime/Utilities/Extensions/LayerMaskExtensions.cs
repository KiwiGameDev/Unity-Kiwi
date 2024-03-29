﻿using UnityEngine;
using UnityEngine.Assertions;

namespace Kiwi.Utilities.Extensions
{
    public static class LayerMaskExtensions
    {
        public static int MaskToLayer(this LayerMask mask)
        {
            int bitmask = mask.value;

            Assert.IsFalse((bitmask & (bitmask - 1)) != 0,
                "MaskToLayer() was passed an invalid mask containing multiple layers.");

            int result = bitmask > 0 ? 0 : 31;

            while (bitmask > 1)
            {
                bitmask >>= 1;
                result++;
            }

            return result;
        }
    }
}