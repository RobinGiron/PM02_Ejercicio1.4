using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea1_4.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agrega : ContentPage
    {
        MediaFile archivoFoto = null;
        public Agrega()
        {
            InitializeComponent();
        }
        private Byte[] ConvertImageToByteArray()
        {
            if (archivoFoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = archivoFoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }
        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Nombre.Text.Trim()) || string.IsNullOrEmpty(Descripcion.Text.Trim()))
            {
                await DisplayAlert("Error", "Faltan Datos, Confirmar", "OK");
                return;
            }
            try
            {
                archivoFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Name = Nombre.Text,
                    Directory = "MisFotos",
                    CustomPhotoSize = 60
                });
                await DisplayAlert("Directorio:", archivoFoto.Path, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", ex.Message, "OK");
            }
            if (archivoFoto != null)
            {
                Foto.Source = ImageSource.FromStream(() => {

                    return archivoFoto.GetStream();
                });
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (archivoFoto == null)
            {
                await DisplayAlert("Alerta", "Toma una Foto!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Nombre.Text.Trim()) || string.IsNullOrEmpty(Descripcion.Text.Trim()))
            {
                await DisplayAlert("Alerta", "Faltan Datos", "OK");
                return;
            }
            IMG imagen = new IMG()
            {
                titulo = Nombre.Text,
                desc = Descripcion.Text,
                img = ConvertImageToByteArray()
            };
            var result = await App.dba.insertUpdateFoto(imagen);
            if (result > 0)
            {
                await DisplayAlert("INFO", "IMAGEN AGREGADA CORRECTAMENTE", "OK");
                limpiar();
            }
            else
            {
                await DisplayAlert("ERROR", "ERROR, IMAGEN NO AGREGADA", "OK");
                limpiar();
            }
        }
        private void limpiar()
        {
            Foto.Source = null;
            Nombre.Text = "";
            Descripcion.Text = "";
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string folderPath = "/storage/emulated/0/Android/data/com.grupo5.Tarea1_4/files/Pictures/MisFotos/";
            string[] files = Directory.GetFiles(folderPath, "*.jpg");
        }
    }
}