using LaserCatEyes.HttpClientListener;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using OpenAI.GPT3;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.Playground.TestHelpers;
Console.WriteLine("Please enter your API Key:");
var openAiService = new OpenAIService(new OpenAiOptions()
{
    ApiKey = Console.ReadLine()
});;;

var builder = new ConfigurationBuilder()
    .AddJsonFile("ApiSettings.json")
    .AddUserSecrets<Program>();

IConfiguration configuration = builder.Build();
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped(_ => configuration);

// Laser cat eyes is a tool that shows your requests and responses between OpenAI server and your client.
// Get your app key from https://lasercateyes.com for FREE and put it under ApiSettings.json or secrets.json.
// It is in Beta version, if you don't want to use it just comment out below line.
serviceCollection.AddLaserCatEyesHttpClientListener();

serviceCollection.AddOpenAIService();
//serviceCollection.AddOpenAIService(settings => { settings.ApiKey = "TEST"; });

var serviceProvider = serviceCollection.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<IOpenAIService>();

//await ModelTestHelper.FetchModelsTest(sdk);
//await EditTestHelper.RunSimpleEditCreateTest(sdk);
//await ImageTestHelper.RunSimpleCreateImageTest(sdk);
//await ImageTestHelper.RunSimpleCreateImageEditTest(sdk);
//await ImageTestHelper.RunSimpleCreateImageVariationTest(sdk);
//await ModerationTestHelper.CreateModerationTest(sdk);
//await CompletionTestHelper.RunSimpleCompletionTest(sdk);
//await EmbeddingTestHelper.RunSimpleEmbeddingTest(sdk);
//await FileTestHelper.RunSimpleFileTest(sdk);
////await FineTuningTestHelper.CleanUpAllFineTunings(sdk); //!!!!! will delete all fine-tunings
//await FineTuningTestHelper.RunCaseStudyIsTheModelMakingUntrueStatements(sdk);
var continueProgram = false;
Console.WriteLine("\nWelcome! Give me a topic and I'll explain it simply to you.");
do
{
    Console.WriteLine("\nWhat would you like to learn about?");
    var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
    {
        Prompt = $"Can you explain {Console.ReadLine()} in simple terms? 50 words or less.",
        MaxTokens = 90
    }, Models.TextDavinciV3);

    if (completionResult.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(completionResult.Choices.FirstOrDefault().Text);
        Console.ResetColor();
    }
    else
    {
        if (completionResult.Error == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            throw new Exception("Unknown Error");
        }
        Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
    }
    Console.WriteLine("\nWould you like to ask another question? True/False");
    continueProgram = bool.Parse(Console.ReadLine());
} 
while (continueProgram == true);