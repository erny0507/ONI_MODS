﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HatchMorphs
{
    class FloralBabyHatchConfig : IEntityConfig
    {

        public GameObject CreatePrefab()
        {
            GameObject hatch = FloralHatchConfig.CreateHatch(
                FloralHatchConfig.BabyId,
                FloralHatchConfig.BabyName,
                FloralHatchConfig.BabyDescription,
                "baby_diamond_hatch_kanim",
                true);
            EntityTemplates.ExtendEntityToBeingABaby(hatch, DiamondHatchConfig.Id, null);
            return hatch;
        }

        public void OnPrefabInit(GameObject prefab)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }

}