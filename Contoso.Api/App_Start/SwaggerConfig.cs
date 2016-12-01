using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Contoso.Api;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Contoso.Api
{
	public class SwaggerConfig
	{
		public static void Register()
		{
			GlobalConfiguration.Configuration
				.EnableSwagger("{apiVersion}/contract", config =>
				{
					config.IgnoreObsoleteActions();
					config.IgnoreObsoleteProperties();

					config.MultipleApiVersions(SelectTargetApi,
					(vc) =>
					{
						vc.Version("v1", "Contoso API v1");
						vc.Version("v1.1", "Contoso API v1.1");
					});

					config.GroupActionsBy(GroupByControllerName);
					config.OperationFilter<RemoveControllerNameInOperationIdFilter>();
				})
				.EnableSwaggerUi("docs/{*assetPath}",
				config =>
				{
					config.EnableDiscoveryUrlSelector();
					config.DocExpansion(DocExpansion.List);
				});
		}

		private static bool SelectTargetApi(ApiDescription apiDesc, string targetApiVersion)
		{
			var attr = apiDesc.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<RoutePrefixAttribute>().FirstOrDefault();
			if (attr == null && targetApiVersion == "v1") return true;
			return attr != null && attr.Prefix == targetApiVersion;
		}

		private static string GroupByControllerName(ApiDescription desc)
		{
			return desc.ActionDescriptor.ControllerDescriptor.ControllerName.Contains("_") ? desc.ActionDescriptor.ControllerDescriptor.ControllerName.Split('_').First() : desc.ActionDescriptor.ControllerDescriptor.ControllerName;
		}

		private class RemoveControllerNameInOperationIdFilter : IOperationFilter
		{
			public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
			{
				if (!string.IsNullOrEmpty(operation.operationId))
				{
					operation.operationId = operation.operationId.Split('_').Last();
				}
			}
		}
	}
}
