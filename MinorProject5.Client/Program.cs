using System.Net.Http.Json;
using System.Text.Json;

using HttpClient client = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5199/")
};

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("====================================");
    Console.WriteLine("       FITNESS MANAGER SYSTEM");
    Console.WriteLine("====================================");
    Console.WriteLine("1. View All Clients");
    Console.WriteLine("2. View All Trainers");
    Console.WriteLine("3. View All Exercises");
    Console.WriteLine("4. View Client by ID");
    Console.WriteLine("5. Add New Client");
    Console.WriteLine("6. Update Client");
    Console.WriteLine("7. Delete Client");
    Console.WriteLine("0. Exit");
    Console.WriteLine("====================================");
    Console.Write("Enter your choice: ");

    string? choice = Console.ReadLine();

    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                await DisplayData("api/clients", "CLIENTS");
                break;

            case "2":
                await DisplayData("api/trainers", "TRAINERS");
                break;

            case "3":
                await DisplayData("api/exercises", "EXERCISES");
                break;

            case "4":
                await ViewClientById();
                break;

            case "5":
                await AddClient();
                break;

            case "6":
                await UpdateClient();
                break;

            case "7":
                await DeleteClient();
                break;

            case "0":
                running = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"API connection error: {ex.Message}");
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"Invalid API response: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// DISPLAY ALL RECORDS
async Task DisplayData(string endpoint, string title)
{
    Console.WriteLine($"========== {title} ==========");

    using var response = await client.GetAsync(endpoint);

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Request failed: {response.StatusCode}");
        return;
    }

    string json = await response.Content.ReadAsStringAsync();

    using var document = JsonDocument.Parse(json);

    foreach (var item in document.RootElement.EnumerateArray())
    {
        Console.WriteLine();

        foreach (var property in item.EnumerateObject())
        {
            Console.WriteLine($"{property.Name}: {property.Value}");
        }

        Console.WriteLine("----------------------------");
    }
}

// VIEW CLIENT BY ID
async Task ViewClientById()
{
    int? id = ReadClientId();

    if (id == null)
        return;

    using var response = await client.GetAsync($"api/clients/{id}");

    await DisplayResponse(response);
}

// ADD CLIENT
async Task AddClient()
{
    Console.WriteLine("========== ADD CLIENT ==========");

    var newClient = ReadClientDetails();

    using var response = await client.PostAsJsonAsync(
        "api/clients",
        newClient
    );

    await DisplayResponse(response);
}

// UPDATE CLIENT
async Task UpdateClient()
{
    Console.WriteLine("========== UPDATE CLIENT ==========");

    int? id = ReadClientId();

    if (id == null)
        return;

    Console.WriteLine("Enter the updated client information:");

    var updatedClient = ReadClientDetails();

    using var response = await client.PostAsJsonAsync(
        $"api/clients/{id}",
        updatedClient
    );

    await DisplayResponse(response);
}

// DELETE CLIENT
async Task DeleteClient()
{
    Console.WriteLine("========== DELETE CLIENT ==========");

    int? id = ReadClientId();

    if (id == null)
        return;

    Console.Write($"Are you sure you want to delete client {id}? (yes/no): ");

    string? confirmation = Console.ReadLine();

    if (!string.Equals(
        confirmation,
        "yes",
        StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Deletion cancelled.");
        return;
    }

    using var response = await client.DeleteAsync($"api/clients/{id}");

    await DisplayResponse(response);
}

// READ CLIENT ID
int? ReadClientId()
{
    Console.Write("Enter Client ID: ");

    if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
    {
        Console.WriteLine("Invalid Client ID.");
        return null;
    }

    return id;
}

// READ CLIENT DETAILS
object ReadClientDetails()
{
    Console.Write("First Name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Last Name: ");
    string lastName = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Phone: ");
    string phone = Console.ReadLine() ?? "";

    return new
    {
        firstName,
        lastName,
        email,
        phone
    };
}

// DISPLAY API RESPONSE
async Task DisplayResponse(HttpResponseMessage response)
{
    string content = await response.Content.ReadAsStringAsync();

    Console.WriteLine();
    Console.WriteLine($"HTTP Status: {(int)response.StatusCode} {response.StatusCode}");

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Operation successful!");
    }
    else
    {
        Console.WriteLine("Operation failed.");
    }

    if (!string.IsNullOrWhiteSpace(content))
    {
        Console.WriteLine(content);
    }
}