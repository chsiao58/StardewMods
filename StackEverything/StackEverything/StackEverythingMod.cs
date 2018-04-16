﻿using Harmony;
using StackEverything.Patches;
using StardewModdingAPI;
using System.Linq;
using System.Reflection;
using SObject = StardewValley.Object;

namespace StackEverything
{
    public class StackEverythingMod : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var harmony = HarmonyInstance.Create("cat.stackeverything");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            //drawInMenu is ambigous and needs to be patched manually by getting the method
            MethodInfo drawInMenu = typeof(SObject).GetMethods(BindingFlags.Instance | BindingFlags.Public).ToList().Find(m => m.Name == "drawInMenu");
            MethodInfo postfix = typeof(DrawInMenuPatch).GetMethods(BindingFlags.Static | BindingFlags.Public).ToList().Find(m => m.Name == "Postfix");

            harmony.Patch(drawInMenu, null, new HarmonyMethod(postfix));
        }
    }
}