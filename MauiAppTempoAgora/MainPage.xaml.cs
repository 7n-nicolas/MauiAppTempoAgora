using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

       
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_cidade.Text))
                {
                    lbl_res.Text = "Preencha a cidade.";
                    return;
                }

                Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                if (t != null)
                {
                    string dados_previsao = $"Latitude: {t.lat} \n" +
                                     $"Longitude: {t.lon}\n" +
                                     $"Nascer do Sol: {t.sunrise}\n" +
                                     $"Por do Sol: {t.sunset}\n" +
                                     $"Temp Máx: {t.temp_max}\n" +
                                     $"Temp Mín: {t.temp_min}\n" +
                                     $"Descrição: {t.description}\n" +
                                     $"Velocidade do Vento: {t.speed}\n" +
                                     $"Visibilidade: {t.visibility}\n";

                    lbl_res.Text = dados_previsao;
                }
                else
                {
                    lbl_res.Text = "cidade não encontrada.";
                }
            }
            
            catch (HttpRequestException)
            {
                // erros de rede
                await DisplayAlert("Erro de Conexão", "Verifique sua internet.", "OK");
                lbl_res.Text = "Falha na conexão.";

            }
            catch (Exception ex)
            {
                // Qualquer outro erro inesperado
                await DisplayAlert("Erro", ex.Message, "OK");
                lbl_res.Text = "Ocorreu um erro.";
            }
        }
    }
}
