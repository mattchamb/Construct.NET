﻿using System;
using System.IO;

namespace Construct.NET
{
    class ConstructOutputter : IConstructOutputter
    {
        public void Output(object obj, BinaryWriter binWriter, ConstructPlan constructPlan)
        {
            foreach (var planAction in constructPlan.PlanActions)
            {
                planAction.Output(binWriter, obj);
            }
        }
    }
}