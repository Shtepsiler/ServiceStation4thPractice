using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PARTS.DAL.Seeders;

public class AISeeder<T> where T : new()
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiKey = "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";
    private const string apiEndpoint = "https://api.openai.com/v1/chat/completions";

    public AISeeder()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    // Method to get response from OpenAI API and map it to entity
    public async Task<T> GenerateEntityAsync(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "Generate information for entity fields based on the prompt provided by the user. Avoid introductory phrases, explanations, or conclusions.return json without extra text don`t spcify the type only retunt without ```json```" },
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiEndpoint, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var completionResponse = JsonSerializer.Deserialize<OpenAIResponse>(jsonResponse);

        var entity = new T();
        if (completionResponse?.choices?[0]?.message?.content != null)
        {
            var responseText = completionResponse.choices[0].message.content;
            entity = JsonSerializer.Deserialize<T>(responseText);
        }

        return entity;
    }
    // Response classes
    public class OpenAIResponse
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string content { get; set; }
    }
}