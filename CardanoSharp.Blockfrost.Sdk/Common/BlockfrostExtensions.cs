﻿using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CardanoSharp.Blockfrost.Sdk.Common;

public static class BlockfrostExtensions
{
	public static void AddBlockfrost(this IServiceCollection services, AuthHeaderConfiguration authHeaderConfiguration)
	{
		var uri = new Uri(authHeaderConfiguration.BaseUrl);

		// set the auth delegate handler
		services.AddSingleton(authHeaderConfiguration);
		services.AddTransient<AuthHeaderHandler>();

		// add all the clients
		services.AddRefitClient<INetworkClient>()
			.ConfigureHttpClient(c => c.BaseAddress = uri)
			.AddHttpMessageHandler<AuthHeaderHandler>();
		services.AddRefitClient<ITransactionsClient>()
			.ConfigureHttpClient(c => c.BaseAddress = uri)
			.AddHttpMessageHandler<AuthHeaderHandler>();
	}
}