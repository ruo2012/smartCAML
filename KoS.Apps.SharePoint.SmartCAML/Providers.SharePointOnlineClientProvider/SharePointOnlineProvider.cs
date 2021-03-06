﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers.SharePointOnlineClientProvider
{
    public class SharePointOnlineProvider : ISharePointProvider
    {
        private string _userName;
        private string _password;

        public Web Web { get; private set; }

        private SPSite CreateSite(string url)
        {
            var site = new SPSite(url);

            if (!String.IsNullOrEmpty(_userName))
            {
                using (site)
                {
                    using (var web = site.OpenWeb())
                    {
                        var userToken = web.AllUsers[_userName].UserToken;
                        return new SPSite(url, userToken);
                    }
                }
            }

            return site;
        }

        public Web Connect(string url)
        {
            using (var site = CreateSite(url))
            {
                using (var web = site.OpenWeb())
                {
                    Web = new Web(this)
                    {
                        Id = web.ID,
                        Url = url,
                        Title = web.Title
                    };

                    Web.Lists = web.Lists.Cast<SPList>().Select(l => new SList
                    {
                        Web = Web,
                        Title = l.Title,
                        Id = l.ID,
                        IsHidden = l.Hidden
                    }).ToList();

                    return Web;
                }
            }
        }

        public Web Connect(string url, string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) != String.IsNullOrEmpty(password))
                throw new ArgumentException("The user or password is null.");

            _userName = userName;
            _password = password;

            return Connect(url);
        }

        public List<ListItem> ExecuteQuery(ListQuery query)
        {
            using (var site = CreateSite(query.List.Web.Url))
            {
                using (var web = site.OpenWeb())
                {
                    var serverList = web.Lists.TryGetList(query.List.Title);
                    var listQuery = new SPQuery { Query = query.Query };

                    return serverList.GetItems(listQuery).Cast<SPListItem>()
                        .Select(i => new ListItem(query.List)
                        {
                            Id = i.ID,
                            Columns =
                                query.List
                                    .Fields
                                    .ToDictionary(
                                        f => f.InternalName,
                                        f => i.GetFormattedValue(f.InternalName))
                        })
                        .ToList();
                }
            }
        }

        public void FillListFields(SList list)
        {
            using (var site = CreateSite(list.Web.Url))
            {
                using (var web = site.OpenWeb())
                {
                    var serverList = web.Lists.TryGetList(list.Title);
                    list.Fields = serverList.Fields.Cast<SPField>().Select(f => new Field
                    {
                        Id = f.Id,
                        IsHidden = f.Hidden,
                        IsReadonly = f.ReadOnlyField,
                        Title = f.Title,
                        InternalName = f.InternalName,
                        Group = f.Group,
                        Type = (FieldType)f.Type
                    }).ToList();
                }
            }
        }

        public void SaveItem(ListItem item)
        {
            using (var site = CreateSite(item.List.Web.Url))
            {
                using (var web = site.OpenWeb())
                {
                    var serverList = web.Lists[item.List.Id];
                    var serverItem = serverList.GetItemById(item.Id);

                    foreach (var change in item.Changes)
                    {
                        serverItem[change.Key] = change.Value;
                    }

                    serverItem.Update();
                }
            }
        }
    }
}
