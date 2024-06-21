using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void SkillDelegate(Character target);

public class Skill
{
    public string Name { get; set; }
    public float Power { get; set; }
    public SkillDelegate Execute { get; set; }

    public Skill(string name, float power, SkillDelegate execute)
    {
        Name = name;
        Power = power;
        Execute = execute;
    }
}

