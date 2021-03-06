﻿using NRaas.CommonSpace.Dialogs;
using NRaas.CommonSpace.Options;
using NRaas.CommonSpace.Selection;
using NRaas.MasterControllerSpace.SelectionCriteria;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Objects;
using Sims3.Gameplay.Skills;
using Sims3.SimIFace;
using Sims3.SimIFace.CAS;
using Sims3.UI;
using Sims3.UI.CAS;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRaas.MasterControllerSpace.Sims.Advanced.Books
{
    public class GeneralBooksRead : BooksRead
    {
        public override string GetTitlePrefix()
        {
            return "GeneralBooksRead";
        }

        protected override bool CanApplyAll()
        {
            return true;
        }

        protected override List<BooksRead.Item> GetOptions(SimDescription me, Dictionary<BookData,bool> lookup)
        {
            List<Item> allOptions = new List<Item>();

            allOptions.Add(null);

            foreach(BookGeneralData data in BookData.BookGeneralDataList.Values)
            {
                allOptions.Add(new Item(data, lookup.ContainsKey(data)));
            }

            foreach (BookWrittenData data in BookData.BookWrittenDataList.Values)
            {
                allOptions.Add(new Item(data, lookup.ContainsKey(data)));
            }

            return allOptions;
        }
    }
}
