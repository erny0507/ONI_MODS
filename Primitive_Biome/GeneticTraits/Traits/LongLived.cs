﻿using Klei.AI;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Primitive_Biome.GeneticTraits.Traits
{
    class LongLived: GeneticTraitBuilder
    {
        public override string ID => "CritterLonglived";
        public override string Name => "Long-lived";
        public override string Description => "Lives 25% longer than usual.";

        public override Group Group => Group.LifespanGroup;

        public override bool Positive => true;
        public override bool CustomDescription => false;
        protected override void ApplyTrait(GameObject go)
        {
            var modifiers = go.GetComponent<Modifiers>();
            if (modifiers != null)
            {
                modifiers.attributes.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 0.25f, Description, is_multiplier: true));
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