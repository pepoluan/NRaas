﻿using NRaas.CommonSpace.Helpers;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.MapTags;
using Sims3.SimIFace;
using Sims3.SimIFace.CAS;
using Sims3.UI;
using Sims3.UI.CAS;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRaas.MasterControllerSpace.SelectionCriteria
{
    public class HasDreamManager : SelectionOption, IDoesNotNeedSpeciesFilter
    {
        public override string GetTitlePrefix()
        {
            return "Criteria.HasDreamManager";
        }

        protected override bool Allow(SimDescription me, IMiniSimDescription actor)
        {
            if (me.CreatedSim == null) return false;

            return (me.CreatedSim.DreamsAndPromisesManager != null);
        }
        protected override bool Allow(MiniSimDescription me, IMiniSimDescription actor)
        {
            return false;
        }
    }
}
