using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWrestlerController
{
    void Delegate(Wrestler wrestler, Bounds boundary);
}