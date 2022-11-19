using System;
using UnityEngine;

namespace Kiwi.Data
{
	[Serializable]
	public struct Optional<T>
	{
		[field: SerializeField] public bool Enabled { get; private set; }
		[field: SerializeField] public T Value { get; private set; }

		public Optional(T value)
		{
			Enabled = true;
			Value = value;
		}
	}
}