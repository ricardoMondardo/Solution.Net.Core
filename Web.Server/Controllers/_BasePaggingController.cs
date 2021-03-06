﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Server.Dtos.Commons;
using Web.Server.Helpers.Pagging;

namespace Web.Server.Controllers
{
    public class _BaseApiPaggingController<T> : ControllerBase where T : class   
    {
        private readonly IUrlHelper _urlHelper;

        public _BaseApiPaggingController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public IActionResult PaggingListResult(PagingParams pagingParams, string routeName, IQueryable<T> query)
        {   

            var paggingModel = Pagging(pagingParams, query);

            Response.Headers.Add("X-Pagination", paggingModel.GetHeader().ToJson());

            var outputModel = new PagingDTO<T>()
            {
                Paging = paggingModel.GetHeader(),
                Links = GetLinks(paggingModel, routeName),
                Items = paggingModel.List.ToList(),
            };
            return Ok(outputModel);
        }

        public PagedList<T> Pagging(PagingParams pagingParams, IQueryable<T> query)
        {
            return new PagedList<T>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public List<LinkInfo> GetLinks(PagedList<T> list, string routeName)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLink(routeName, list.PreviousPageNumber, list.PageSize, "previousPage", "GET"));

            links.Add(CreateLink(routeName, list.PageNumber, list.PageSize, "self", "GET"));

            if (list.HasNextPage)
                links.Add(CreateLink(routeName, list.NextPageNumber, list.PageSize, "nextPage", "GET"));

            return links;
        }

        private LinkInfo CreateLink(
            string routeName, int pageNumber, int pageSize,
            string rel, string method)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }
    }
}
