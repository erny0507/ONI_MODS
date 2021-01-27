﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using STRINGS;
using UnityEngine;
using TUNING;
using Klei.AI;
using Object = UnityEngine.Object;

namespace PipMorphs
{
    class SpringAdultPip : IEntityConfig
    {
        private static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.4f;
        private static float CALORIES_PER_DAY_OF_PLANT_EATEN = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;
        private static float KG_POOP_PER_DAY_OF_PLANT = 50f;
        private static float MIN_POOP_SIZE_KG = 40f;
        public static int EGG_SORT_ORDER = 2;
        public const string ID = "SquirrelSpring";
        public const string BASE_TRAIT_ID = "SquirrelBaseTrait";
        public const string EGG_ID = "SquirrelSpringEgg";
        public const float OXYGEN_RATE = 0.0234375f;
        public const float BABY_OXYGEN_RATE = 0.01171875f;
        private const SimHashes EMIT_ELEMENT = SimHashes.Fertilizer;
        public static string Name = UI.FormatAsLink("Spring Pip", ID);
        public const string Description = "The leaves on their bodies have bright colors.";
        public static string EggName = UI.FormatAsLink("Spring Pip Egg", EGG_ID);
        public const string BabyId = "SquirrelSpringBaby";
        public static string BabyName = UI.FormatAsLink("Spring Pipsqueak", BabyId);
        public const string BabyDescription = "The leaves on their bodies resemble their cousins.";
        public static EffectorValues decor = DECOR.BONUS.TIER5;
        public const string SymbolOverride = "";
        public const float FertilityCycles = 60.0f;
        public const float IncubationCycles = 20.0f;
        public const float MaxAge = 100f;
        public const float Hitpoints = 25f;


        public static Diet.Info[] Diet(Tag poopTag, float caloriesPerKg, float producedConversionRate, string diseaseId = null, float diseasePerKgProduced = 0.0f)
        {
            return new Diet.Info[1]
              {
      new Diet.Info(new HashSet<Tag>()
      {
        (Tag)PrickleFlowerConfig.ID,
        (Tag)BasicFabricMaterialPlantConfig.ID
      }, poopTag, caloriesPerKg, producedConversionRate, diseaseId, diseasePerKgProduced, false, true)
              };

        }
        public static Trait CreateTrait(string name)
        {
            Trait trait = Db.Get().CreateTrait(BASE_TRAIT_ID, name, name, (string)null, false, (ChoreGroup[])null, true, true);
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, (float)(-(double)SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600.0), name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, Hitpoints, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, MaxAge, name, false, false, true));
            return trait;
        }
        public static GameObject CreateCritter(
  string id,
  string name,
  string desc,
  string anim_file,
  bool is_baby)
        {
            GameObject wildCreature = EntityTemplates.ExtendEntityToWildCreature(BaseSquirrelConfig.BaseSquirrel(id, name, desc, anim_file, "SquirrelBaseTrait", is_baby, (string)null), SquirrelTuning.PEN_SIZE_PER_CREATURE, 100f);
            CreateTrait(name);
            Diet.Info[] diet_infos = Diet(
                EMIT_ELEMENT.CreateTag(), CALORIES_PER_DAY_OF_PLANT_EATEN,
                KG_POOP_PER_DAY_OF_PLANT,
                (string)null, 0.0f);
            double minPoopSizeKg = (double)MIN_POOP_SIZE_KG;
            wildCreature.AddOrGet<DecorProvider>()?.SetValues(decor);
            return BaseSquirrelConfig.SetupDiet(wildCreature, diet_infos, (float)minPoopSizeKg);
        }
        public static List<FertilityMonitor.BreedingChance> EggChances = new List<FertilityMonitor.BreedingChance>()
        {
            new FertilityMonitor.BreedingChance()
            {
                egg = EGG_ID.ToTag(),
                weight = 1f
            }
        };
        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[4]
           {
                new ComplexRecipe.RecipeElement((Tag)SquirrelConfig.EGG_ID, 2f ),
                new ComplexRecipe.RecipeElement((Tag)RawEggConfig.ID, (float) (5)),
                new ComplexRecipe.RecipeElement(PrickleFlowerConfig.SEED_ID.ToTag(), 10f),
                new ComplexRecipe.RecipeElement(SimHashes.Fertilizer.CreateTag(), 1000f),
           };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag)EGG_ID, 1f)
            };
            var r = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ID, (IList<ComplexRecipe.RecipeElement>)ingredients,
                (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = 80f / 8,
                description = BabyDescription,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result
            };
            r.fabricators = new List<Tag>()
            {
                TagManager.Create(SupermaterialRefineryConfig.ID)
            };
  

            return EntityTemplates.ExtendEntityToFertileCreature(
             CreateCritter(
                 id: ID,
                 name: Name,
                 desc: Description,
                 anim_file:
                 "squirrel_spring_kanim",
                 is_baby: false
             ),
             EGG_ID,
             EggName,
             Description,
             "egg_squirrel_spring_kanim",
             SquirrelTuning.EGG_MASS,
             BabyId,
             FertilityCycles,
             IncubationCycles,
             EggChances,
             EGG_SORT_ORDER);

        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
