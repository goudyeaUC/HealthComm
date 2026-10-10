using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        string fhirBaseUrl =
            "https://hapi.fhir.org/baseR4";

        var client = new FhirClient(fhirBaseUrl);


        try
        {

            Hl7.Fhir.Model.Bundle results = await client.SearchAsync<Practitioner>(
                new string[] { "_count=10" }
            );
            if (results != null)
            {

                Debug.WriteLine(results.Entry.Count);
            }

            foreach (var entry in results.Entry)
            {
                string? fullUrl = entry.FullUrl;

                if (!string.IsNullOrEmpty(fullUrl))
                {
                    string practitionerId =
                        fullUrl.Split('/').Last();

                    Debug.WriteLine($"Practitioner ID: {practitionerId}");
                    try
                    {
                        Practitioner? practitioner =
                                    await client.ReadAsync<Practitioner>(
                                        $"Practitioner/{practitionerId}"
                                    );

                        string name = practitioner?.Name?.FirstOrDefault()?.ToString();
                        Debug.WriteLine($"Practitioner Name: {name}");
                        PractitionerNode practitionerNode = new PractitionerNode
                        {
                            Id = practitionerId,
                            Name = name ?? "Unknown"
                        };
                        practitionerNodes.Add(practitionerNode);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
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