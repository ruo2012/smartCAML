﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoS.Apps.SharePoint.SmartCAML.Model
{
    public class SharePointList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public string Group { get; set; }

        public List<Field> Fields { get; set; } = new List<Field>();

    }
}