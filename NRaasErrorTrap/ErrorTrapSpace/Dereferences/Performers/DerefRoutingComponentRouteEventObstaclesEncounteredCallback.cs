﻿using NRaas.CommonSpace.Booters;
using NRaas.CommonSpace.Helpers;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.ObjectComponents;
using Sims3.Gameplay.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NRaas.ErrorTrapSpace.Dereferences
{
    public class DerefRoutingComponentRouteEventObstaclesEncounteredCallback : Dereference<RoutingComponent.RouteEventObstaclesEncounteredCallback>
    {
        protected override DereferenceResult Perform(RoutingComponent.RouteEventObstaclesEncounteredCallback reference, FieldInfo field, List<ReferenceWrapper> objects)
        {
            return DereferenceResult.Continue;
        }
    }
}
