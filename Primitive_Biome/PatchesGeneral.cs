﻿using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klei;
using Primitive_Biome.Elements;
using System.Collections;
using UnityEngine;
using Klei.AI;
using System.Diagnostics;
using Primitive_Biome.GeneticTraits;
using PeterHan;

namespace Primitive_Biome
{
    public class PatchesGeneral
    {

        public static void OnLoad()
        {
            PeterHan.PLib.PUtil.LogModInit();
        }
    }
}
