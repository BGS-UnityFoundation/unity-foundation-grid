﻿using System;
using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class GenericSelection<T>
    {
        public Optional<T> Current { get; private set; }
        public event Action<Optional<T>> OnValueChanged;

        public GenericSelection()
        {
            Current = Optional<T>.None();
        }

        public void Set(T value)
        {
            Current = Optional<T>.Some(value);
            OnValueChanged?.Invoke(Current);
        }

        public void Clear()
        {
            Current = Optional<T>.None();
            OnValueChanged?.Invoke(Current);
        }
    }
}
