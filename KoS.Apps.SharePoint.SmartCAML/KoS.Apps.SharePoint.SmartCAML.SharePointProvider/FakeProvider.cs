﻿using KoS.Apps.SharePoint.SmartCAML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoS.Apps.SharePoint.SmartCAML.SharePointProvider
{
    public class FakeProvider : ISharePointProvider
    {
        private class Internals
        {
            public const string Text = "TextInternal";
            public const string User = "UserInternal";
            public const string Date = "DateInternal";
            public const string DateTime = "DateTimeInternal";
            public const string Lookup = "LookupInternal";
            public const string MultiLookup = "MultiLookupInternal";
            public const string Url = "UrlInternal";
            public const string Integer = "IntegerInternal";
            public const string Note = "NoteInternal";
            public const string Choice = "ChoiceInternal";
            public const string Boolean = "BooleanInternal";
            public const string Readonly = "ReadonlyInternal";
            public const string Hidden = "HiddenInternal";
            public const string Number = "NumberInternal";
            public const string Currency = "CurrencyInternal";
            public const string Computed = "ComputedInternal";
            public const string MultiChoice = "MultiChoiceInternal";
            public const string Guid = "GuidInternal";
            public const string Calculated = "CalculatedInternal";
            public const string File = "FileInternal";
            public const string Attachments = "AttachmentsInternal";
            public const string ContentTypeId = "ContentTypeIdInternal";
            public const string Geolocation = "GeolocationInternal";
        }

        public Web Web { get; set; }

        public Web Connect(string url)
        {
            if (!url.Contains("test")) return null;

            Web = new Web(this)
            {
                Id = Guid.Empty,
                Title = "Test",
                Url = url,
            };

            Web.Lists = new List<SList>
            {
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1), Title = "List1", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2), Title = "List2", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3), Title = "List3", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4), Title = "List4", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5), Title = "List5", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6), Title = "List6", Web = Web},
                new SList {Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7), Title = "List7", Web = Web},
            };

            return Web;
        }

        public List<ListItem> ExecuteQuery(ListQuery query)
        {
            return new List<ListItem>
            {
                new ListItem { Id = 1, Columns = new Dictionary<string, string>
                    {
                        { Internals.Text        , "Test1" },
                        { Internals.User        , "Owner1" },
                        { Internals.Date        , "Date1" },
                        { Internals.DateTime    , "DateTime1" },
                        { Internals.Lookup      , "Lookup1" },
                        { Internals.MultiLookup , "MultiLookup1" },
                        { Internals.Integer     , "Integer1" },
                        { Internals.Note        , "Note1" },
                        { Internals.Url         , "Url1" },
                        { Internals.Choice      , "Choice1" },
                        { Internals.Boolean     , "Boolean1" },
                        { Internals.Readonly    , "Readonly1" },
                        { Internals.Hidden      , "Hidden1" },
                        { Internals.Number      , "Number1" },
                        { Internals.Currency    , "Currency1" },
                        { Internals.Computed    , "Computed1" },
                        { Internals.MultiChoice , "MultiChoice1" },
                        { Internals.Guid        , "Guid1" },
                        { Internals.Calculated  , "Calculated1" },
                        { Internals.File        , "File1" },
                        { Internals.Attachments , "Attachments1" },
                        { Internals.ContentTypeId, "ContentTypeId1" },
                        { Internals.Geolocation , "Geolocation1" },
                    }
                },
                new ListItem { Id = 1, Columns = new Dictionary<string, string>
                    {
                        { Internals.Text, "Test2" },
                        { Internals.User, "Owner2" },
                        { Internals.Date        , "" },
                        { Internals.Url         , null },
                    }
                },
                new ListItem { Id = 1, Columns = new Dictionary<string, string>
                    {
                        { Internals.Text, "Test3" },
                        { Internals.User, "Owner3" }
                    }
                },
            };
        }

        public void FillListFields(SList list)
        {
            var fields = new List<Field>
            {
                new Field { Group = "1", Title = "Text",            Type=FieldType.Text,            InternalName = Internals.Text       },
                new Field { Group = "1", Title = "User",            Type=FieldType.User,            InternalName = Internals.User       },
                new FieldDateTime { Group = "1", Title = "Date",    Type=FieldType.DateTime,        InternalName = Internals.Date       , DateOnly = true},
                new FieldDateTime { Group = "1", Title = "DateTime",Type=FieldType.DateTime,        InternalName = Internals.DateTime   , DateOnly = false},
                new Field { Group = "2", Title = "Lookup",          Type=FieldType.Lookup,          InternalName = Internals.Lookup     },
                new Field { Group = "2", Title = "MultiLookup",     Type=FieldType.Lookup,          InternalName = Internals.MultiLookup },
                new Field { Group = "2", Title = "Integer",         Type=FieldType.Integer,         InternalName = Internals.Integer    },
                new Field { Group = "3", Title = "Note",            Type=FieldType.Note,            InternalName = Internals.Note       },
                new Field { Group = "3", Title = "Url",             Type=FieldType.Url,             InternalName = Internals.Url        },
                new FieldChoice { Group = "3", Title = "Choice",    Type=FieldType.Choice,          InternalName = Internals.Choice     , Choices = new[] {"Choice1", "Choice2", "Choice3"}},
                new Field { Group = "3", Title = "Boolean",         Type=FieldType.Boolean,         InternalName = Internals.Boolean    },
                new Field { Group = "4", Title = "Readonly",        Type=FieldType.Text,            InternalName = Internals.Readonly   , IsReadonly = true},
                new Field { Group = "4", Title = "Hidden",          Type=FieldType.Text,            InternalName = Internals.Hidden     , IsHidden = true},
                new Field { Group = "4", Title = "Number",          Type=FieldType.Number,          InternalName = Internals.Number     },
                new Field { Group = "4", Title = "Computed",        Type=FieldType.Computed,        InternalName = Internals.Computed   },
                new Field { Group = "4", Title = "Currency",        Type=FieldType.Currency,        InternalName = Internals.Currency   },
                new Field { Group = "4", Title = "MultiChoice",     Type=FieldType.MultiChoice,     InternalName = Internals.MultiChoice },
                new Field { Group = "5", Title = "Guid",            Type=FieldType.Guid,            InternalName = Internals.Guid       },
                new Field { Group = "5", Title = "Calculated",      Type=FieldType.Calculated,      InternalName = Internals.Calculated },
                new Field { Group = "5", Title = "File",            Type=FieldType.File,            InternalName = Internals.File       },
                new Field { Group = "5", Title = "Attachments",     Type=FieldType.Attachments,     InternalName = Internals.Attachments },
                new Field { Group = "5", Title = "ContentTypeId",   Type=FieldType.ContentTypeId,   InternalName = Internals.ContentTypeId },
                new Field { Group = "5", Title = "Geolocation",     Type=FieldType.Geolocation,     InternalName = Internals.Geolocation },
            };

            list.Fields = fields;
        }
    }
}
