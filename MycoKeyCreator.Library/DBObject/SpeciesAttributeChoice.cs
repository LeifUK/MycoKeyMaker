﻿using System;
using PetaPoco.NetCore;

namespace MycoKeyCreator.Library.DBObject
{
    [TableName(Database.TableNames.SpeciesAttributeChoice)]
    public class SpeciesAttributeChoice : IObject
    {
        public Int64 id { get; set; }
        public Int64 key_id { get; set; }
        public Int64 attributechoice_id { get; set; }
        public Int64 species_id { get; set; }
    }
}
