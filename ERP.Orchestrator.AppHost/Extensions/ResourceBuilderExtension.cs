using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Orchestrator.AppHost.Extensions;

public static class ResourceBuilderExtension
{

	public static IResourceBuilder<T> WithSwaggerUI<T>(this IResourceBuilder<T> builder)
		where T : IResourceWithEndpoints
	{
		return builder.WithOpenApiDocs("swagger-ui-docs", "Swagger API Documentation", "swagger");
	}

	public static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
		where T : IResourceWithEndpoints
	{
		return builder.WithOpenApiDocs("scalar-docs", "Scalar API Documentation", "scalar/v1");
	}

	public static IResourceBuilder<T> WithReDoc<T>(this IResourceBuilder<T> builder)
		where T : IResourceWithEndpoints
	{
		return builder.WithOpenApiDocs("redoc-docs", "ReDoc API Documentation", "api-docs");
	}

	private static IResourceBuilder<T> WithOpenApiDocs<T>(this IResourceBuilder<T> builder, string name, string displayName, string openApiUIPath) where T : IResourceWithEndpoints
	{
		return builder.WithCommand(
			name: name,
			displayName: displayName,
			executeCommand: async (x) =>
			{
				try
				{
					var endpoint = builder.GetEndpoint("https");

					var url = $"{endpoint.Url}/{openApiUIPath}";

					Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

					return new ExecuteCommandResult { Success = true };

				}
				catch (Exception ex)
				{
					return new ExecuteCommandResult { Success = false, ErrorMessage = ex.ToString() };
				}
			},
			commandOptions: new CommandOptions
			{
				IconName = "Document",
				IconVariant = IconVariant.Filled,
				UpdateState = (context) =>
				{
					return context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ? ResourceCommandState.Enabled : ResourceCommandState.Disabled;
				}
			}
		);
	}
}
