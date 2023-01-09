﻿namespace OpenAI.GPT3
{
    public class OpenAiOptions
    {
        public static readonly string SettingKey = "OpenAIServiceOptions";

        /// <summary>
        ///     For users who belong to multiple organizations, you can pass a header to specify which organization is used for an
        ///     API request. Usage from these API requests will count against the specified organization's subscription quota.
        ///     Organization IDs can be found on your
        ///     <a href="https://beta.openai.com/account/org-settings">Organization settings</a> page.
        /// </summary>
        public string? Organization { get; set; }

        /// <summary>
        ///     The OpenAI API uses API keys for authentication. Visit your
        ///     <a href="https://beta.openai.com/account/api-keys">API Keys page</a> to retrieve the API key you'll use in
        ///     your requests.
        ///     Remember that your API key is a secret! Do not share it with others or expose it in any client-side code(browsers,
        ///     apps). Production requests must be routed through your own backend server where your API key can be securely loaded
        ///     from an environment variable or key management service.
        /// </summary>

        public string ApiKey { get; set; } = "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";

        public string ApiVersion { get; set; } = "v1";

        public string BaseDomain { get; set; } = "https://api.openai.com/";

        /// <summary>
        ///     Default engine id. If you are working with only one engine, this will save you from few line extra code.
        /// </summary>
        public static string? DefaultEngineId { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new ArgumentNullException(nameof(ApiKey));
            }

            if (string.IsNullOrEmpty(ApiVersion))
            {
                throw new ArgumentNullException(nameof(ApiVersion));
            }

            if (string.IsNullOrEmpty(BaseDomain))
            {
                throw new ArgumentNullException(nameof(BaseDomain));
            }
        }
    }
}