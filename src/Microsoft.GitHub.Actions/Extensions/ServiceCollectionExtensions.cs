﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all the services required to interact with GitHub Action workflows.
    /// </summary>
    public static IServiceCollection AddGitHubActions(this IServiceCollection services)
    {
        services.AddSingleton<IConsole, DefaultConsole>();
        services.AddTransient<ICommandIssuer, DefaultCommandIssuer>();
        services.AddTransient<IFileCommandIssuer>(
            _ => new DefaultFileCommandIssuer(
                (filePath, message) =>
                {
                    using var writer = new StreamWriter(filePath, append: true, Encoding.UTF8);
                    return writer.WriteLineAsync(message);
                }));
        services.AddTransient<IWorkflowStepService, DefaultWorkflowStepService>();

        return services;
    }
}
