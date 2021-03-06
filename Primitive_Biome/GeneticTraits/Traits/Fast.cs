﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Primitive_Biome.GeneticTraits.Traits
{
    class Fast : GeneticTraitBuilder
    {
        public override string ID => "CritterFast";
        public override string Name => "Fast";
        public override string Description => "Is twice as fast as its peers.";

        public override Group Group => Group.SpeedGroup;
        public override bool Positive => true;
        public override bool CustomDescription => false;

        protected override void ApplyTrait(GameObject go)
        {
            var navigator = go.GetComponent<Navigator>();
            if (navigator != null)
            {
                navigator.defaultSpeed *= 2f;
            }
        }

        protected override void Init()
        {
            UtilPB.CreateTrait(ID, Name, Description,
              on_add: delegate (GameObject go)
              {
                  ChooseTarget(go);
              },
              positiveTrait: Positive
            );
        }
        public override void SetConfiguration(GameObject to, GameObject from)
        {
          
        }
    }
}