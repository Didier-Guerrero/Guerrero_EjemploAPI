using Guerrero_EjemploAPI.DG_Model;
using System.Text.Json;
using Newtonsoft.Json;

namespace Guerrero_EjemploAPI.DG_Views;

public partial class DG_ClimaActual : ContentPage
{
	public DG_ClimaActual()
	{
		InitializeComponent();
	}

    private async void DG_Consultar_Clicked(object sender, EventArgs e)
    {
		string DG_Lat = DG_lat.Text;
		string DG_Lon = DG_lon.Text;
		if(Connectivity.NetworkAccess == NetworkAccess.Internet)
		{
			using (var DG_client = new HttpClient())
			{
				string DG_url = $"https://api.openweathermap.org/data/2.5/weather?lat="+DG_Lat+"&lon="+DG_Lon+"&appid=3f9a3963399481445145bec4bc81750c";
				
				var DG_response = await DG_client.GetAsync(DG_url);
				if (DG_response.IsSuccessStatusCode)
				{
					var DG_json = await DG_response.Content.ReadAsStringAsync();
					var DG_Clima = JsonConvert.DeserializeObject<Rootobject>(DG_json);
					DG_ClimaLabel.Text = DG_Clima.weather[0].main;
                    DG_PaisLabel.Text = DG_Clima.sys.country;
                    DG_CiudadLabel.Text = DG_Clima.name;
					DG_Error.Text = "";
				}
				else
				{
                    DG_ClimaLabel.Text = "";
                    DG_PaisLabel.Text = "";
                    DG_CiudadLabel.Text = "";
                    DG_Error.Text = "No se ha podido acceder a la API revisar informacion";
				}
			}
		}
    }
}