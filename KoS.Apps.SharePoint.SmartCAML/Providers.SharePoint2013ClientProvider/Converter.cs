﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client;

namespace KoS.Apps.SharePoint.SmartCAML.Providers.SharePoint2013ClientProvider
{
    static class Converter
    {
        public static List<Model.ContentType> ToContentTypes(ContentTypeCollection contentTypes)
        {
            return contentTypes.Cast<ContentType>().Select(ct => new Model.ContentType
            {
                Id = ct.StringId,
                Name = ct.Name
            }).ToList();
        }

        public static string UrlValueToString(FieldUrlValue value)
        {
            return value.Description + "#;" + value.Url;
        }

        public static string LookupCollectionValueToString(FieldLookupValue[] value)
        {
            if (value.Length == 0) return string.Empty;

            return value.Select(lv => LookupValueToString(lv))
                            .Aggregate((all, current) => all + "|" + current);
        }

        public static string LookupValueToString(FieldLookupValue value)
        {
            return value.LookupId + "#;" + value.LookupValue;
        }

        public static string ChoiceMultiValueToString(string[] value)
        {
            if(value.Length == 0) return string.Empty;

           return value.Aggregate((all, current) => all + "|" + current);
        }

        public static FieldLookupValue ToLookupValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            var lookupSplit = value.Split(new[] {"#;"}, StringSplitOptions.None);

            int lookupId;
            return int.TryParse(lookupSplit[0], out lookupId)
                ? new FieldLookupValue {LookupId = lookupId}
                : null;
        }

        public static FieldLookupValue[] ToLookupCollectionValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            return value
                .Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(lv => ToLookupValue(lv))
                .Where(lv => lv != null)
                .ToArray();
        }
    }
}
