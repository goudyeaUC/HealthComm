using HealthCommShared.DTOs;
using System.Net.Http.Json;
using System.Diagnostics;

namespace HealthComm;

public partial class MedicalConditionPage : ContentPage
{
	public MedicalConditionPage()
	{
		InitializeComponent();
	}
    private List<MedicalConditionDTO> medicationConditions = new List<MedicalConditionDTO>();

    private async System.Threading.Tasks.Task retrieveData()
    {

        try
        {
            using var client = new HttpClient();

            // developer needs to check port for the backend API and update the URL accordingly

            string url =
                "http://localhost:5167/api/MedicalCondition";


            try
            {
                var conditions =
                 await client.GetFromJsonAsync<List<MedicalConditionDTO>>(url);

                ConditionList.ItemsSource = conditions ?? new List<MedicalConditionDTO>();
                

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
            /*
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

            LvPractitioners.ItemsSource = practitionerNodes; */
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error loading practitioners: {ex}");
        }
    }
}