﻿using System.Collections.Generic;
using System;

namespace MycoKeyCreator.WebApplication.Model
{
    public class KeyMatchViewModel
    {
        public KeyMatchViewModel()
        {
            Literature = new List<Library.DBObject.Literature>();
            AttributeSelections = new List<AttributeSelection>();
            Species = new List<SpeciesMatchData>();
        }

        public class AttributeSelection
        {
            public bool IsSelected { get; set; }
            public MycoKeyCreator.Library.DBObject.Attribute Attribute { get; set; }
        }

        public class AttributeSize : AttributeSelection
        {
            public float Value { get; set; }
        }

        public class AttributeChoice : AttributeSelection
        {
            public Int64 SelectedAttributeChoiceId { get; set; }
            public List<MycoKeyCreator.Library.DBObject.AttributeChoice> AttributeChoices { get; set; }
        }

        public string KeyName { get; set; }
        public string KeyTitle { get; set; }
        public string KeyDescription { get; set; }
        public string KeyNotes { get; set; }
        public List<Library.DBObject.Literature> Literature { get; private set; }
        public string Copyright { get; set; }
        public List<AttributeSelection> AttributeSelections { get; private set; }
        public List<SpeciesMatchData> Species { get; set; }
    }
}
