﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandVR
{
    
    public interface IHand
    {
        bool IsStarted { get; }
        string Label { get; }
        IEnumerator StartEffect();
        IEnumerator Reset();
    }
}