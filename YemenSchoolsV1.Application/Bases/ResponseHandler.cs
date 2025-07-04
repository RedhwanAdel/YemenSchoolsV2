﻿using Microsoft.Extensions.Localization;
using System.Net;
using YemenSchoolsV1.Application.Resources;

namespace FinalProject.Application.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}
		public Response<T> Deleted<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = _stringLocalizer[SharedResourcesKeys.Delete]
			};
		}
		public Response<T> Success<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = _stringLocalizer[SharedResourcesKeys.Success],
				Meta = Meta
			};
		}
		public Response<T> Unauthorized<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeeded = true,
				Message = _stringLocalizer[SharedResourcesKeys.Unauthorized]
			};
		}
		public Response<T> BadRequest<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = Message == null ? _stringLocalizer[SharedResourcesKeys.BadRequest] : Message
			};
		}
		public Response<T> BadRequest<T>(List<string> errors, string message = null)
		{
			return new Response<T>()
			{
				StatusCode = HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = message ?? _stringLocalizer[SharedResourcesKeys.BadRequest],
				Errors = errors
			};
		}
		public Response<T> UnprocessableEntity<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
				Succeeded = false,
				Message = Message == null ? _stringLocalizer[SharedResourcesKeys.UnprocessableEntity] : Message
			};
		}
		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message
			};
		}

		public Response<T> Created<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = _stringLocalizer[SharedResourcesKeys.Created],
				Meta = Meta
			};
		}
		public Response<T> ValidationError<T>(Dictionary<string, string[]> errorDict)
		{
			return new Response<T>
			{
				StatusCode = HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = _stringLocalizer[SharedResourcesKeys.BadRequest],
				ErrorsBag = errorDict.ToDictionary(k => k.Key, v => v.Value.ToList())
			};
		}
	}
}
