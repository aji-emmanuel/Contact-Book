/*using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;

namespace UserManagement.Hateoas
{
    public class PageMetaData: Controller
    {

        public static CreateMetaData

        private string PageUrl(UserActionParams userActionParams, PageLinkType type)
        {
            return type switch
            {
                PageLinkType.PreviousPage => Url.Link("GetAllUsers",
                new
                {
                    Page = userActionParams.Page - 1,
                    userActionParams.StateName,
                    userActionParams.SearchWord
                }),
                PageLinkType.NextPage => Url.Link("GetAllUsers",
                new
                {
                    Page = userActionParams.Page + 1,
                    userActionParams.StateName,
                    userActionParams.SearchWord
                }),
                _ => Url.Link("GetAllUsers",
                new
                {
                    userActionParams.Page,
                    userActionParams.StateName,
                    userActionParams.SearchWord
                }),
            };
        }
    }
}
*/