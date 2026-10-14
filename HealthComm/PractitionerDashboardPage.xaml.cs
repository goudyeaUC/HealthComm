
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
//using System.Net.Http.Json;
using System.Text.Json;

namespace HealthComm;

public class PractitionerNode
{
    public string Id { get; set; }
    public string Name { get; set; }
}
public partial class PractitionerDashboardPage : ContentPage
{
    private List<PractitionerNode> practitionerNodes = new List<PractitionerNode>();

    private async System.Threading.Tasks.Task retrieveData()
    {
       
        try
        {
            using var client = new HttpClient();

            // developer needs to check port for the backend API and update the URL accordingly

            string url =
                "http://localhost:5167/api/practitioners";
          

            try
            {
                var response = await client.GetAsync(url);

                string str2 = await response.Content.ReadAsStringAsync();

                //following is left for debugging purposes, to check the response from the backend API

                await DisplayAlert(
                    "Response",
                    $"Status: {response.StatusCode}\n\n{str2}",
                    "OK");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                practitionerNodes =
                    JsonSerializer.Deserialize<List<PractitionerNode>>(str2, options) ?? new();
                //following is left for debugging purposes, to check the deserialized list of PractitionerNode objects

                string json = JsonSerializer.Serialize(practitionerNodes);

                await DisplayAlert(
                    "List obj",
                    $"{json}",
                    "OK");

            }
            catch (Exception ex)
            {
                //following is left for debugging purposes, to check the error message if the request fails or the response cannot be deserialized
                await DisplayAlert(
                    "Error",
                    ex.ToString(),
                    "OK");
            }
           

        } 
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        } 
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        try
        {
            await retrieveData();

            System.Diagnostics.Debug.WriteLine(
                $"Retrieved {practitionerNodes.Count} practitioners.");
            //following is left for debugging purposes, to show api call did not return any practitioners,
            //and to show the default list of practitioners is being used instead

            if (practitionerNodes.Count == 0)
            {
                practitionerNodes = new List<PractitionerNode>
            {
                new PractitionerNode
                {
                    Id = "14447",
                    Name = "Dr. John Smith"
                },
                new PractitionerNode
                {
                    Id = "14448",
                    Name = "Dr. Jane Doe"
                }
            };
            }

            LvPractitioners.ItemsSource = practitionerNodes;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error loading practitioners: {ex}");
        }
    }

    public PractitionerDashboardPage()
    {
        InitializeComponent();



    }
}