using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    TypeOfPowerUp Type { get; }

    void Use();
}

public enum TypeOfPowerUp
{
    Jump,
    Safe
}
