﻿using NRaas.CommonSpace.Booters;
using NRaas.CommonSpace.Helpers;
using NRaas.CommonSpace.ScoringMethods;
using NRaas.VectorSpace.Helpers;
using Sims3.Gameplay;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.Careers;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.EventSystem;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Objects;
using Sims3.Gameplay.Objects.RabbitHoles;
using Sims3.Gameplay.Opportunities;
using Sims3.Gameplay.Rewards;
using Sims3.Gameplay.Roles;
using Sims3.Gameplay.Skills;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace NRaas.VectorSpace.Booters
{
    public class InstigatorBooter : BooterHelper.ByRowListingBooter
    {
        static Dictionary<string, Data> sInstigators = new Dictionary<string, Data>();

        public InstigatorBooter(string reference)
            : base("Instigator", "InstigatorsFile", "File", reference, false)
        { }

        public static IEnumerable<Data> Instigators
        {
            get { return sInstigators.Values; }
        }

        protected override void Perform(BooterHelper.BootFile file, XmlDbRow row)
        {
            string guid = row.GetString("GUID");
            if (string.IsNullOrEmpty(guid))
            {
                BooterLogger.AddError("Invalid GUID: " + guid);
                return;
            }
            else if (sInstigators.ContainsKey(guid))
            {
                BooterLogger.AddError("Duplicate GUID: " + guid);
                return;
            }

            Type type = row.GetClassType("FullClassName");
            if (type == null)
            {
                BooterLogger.AddError(guid + " Invalid FullClassName: " + row.GetString("FullClassName"));
                return;
            }

            Data symptom = null;

            try
            {
                symptom = type.GetConstructor(new Type[] { typeof(XmlDbRow) }).Invoke(new object[] { row }) as Data;
            }
            catch (Exception e)
            {
                BooterLogger.AddError("Contructor Fail: " + row.GetString("FullClassName"));

                Common.Exception(guid + Common.NewLine + row.GetString("FullClassName") + " Fail", e);
            }

            if (symptom != null)
            {
                sInstigators.Add(guid, symptom);
            }
        }

        public static Data GetInstigator(string guid)
        {
            Data vector;
            if (!sInstigators.TryGetValue(guid, out vector)) return null;

            return vector;
        }

        public abstract class Data
        {
            string mGuid;

            string mStory;

            VectorBooter.Test mTest;

            public Data(XmlDbRow row, VectorBooter.Test test)
            {
                mGuid = row.GetString("GUID");

                mStory = row.GetString("Story");

                mTest = test;
            }

            public string Guid
            {
                get { return mGuid; }
            }

            public virtual void GetEvents(Dictionary<EventTypeId, bool> events)
            {
                mTest.GetEvents(events);
            }

            public virtual bool Perform(Event e)
            {
                return mTest.IsSuccess(e);
            }

            public void ShowStory(SimDescription sim)
            {
                if ((!string.IsNullOrEmpty(mStory)) && (SimTypes.IsSelectable(sim)))
                {
                    Common.Notify(Common.Localize("InstigatorStory:" + mStory, sim.IsFemale, new object[] { sim }));
                }
            }

            public void Register(VectorBooter.Data vector)
            {
                Dictionary<EventTypeId, bool> events = new Dictionary<EventTypeId, bool>();
                GetEvents(events);

                foreach (EventTypeId e in events.Keys)
                {
                    new OutbreakControl.InstigatorListener(e, vector, this);
                }
            }

            public string GetLocalizedName(bool isFemale)
            {
                return Common.Localize("InstigatorName:" + mGuid, isFemale);
            }

            public override string ToString()
            {
                string result = mGuid;

                result += Common.NewLine + " Story: " + mStory;
                result += Common.NewLine + mTest;

                return result;
            }
        }
    }
}
