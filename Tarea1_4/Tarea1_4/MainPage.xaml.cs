using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea1_4.Models;
using Tarea1_4.Views;
using Xamarin.Forms;

namespace Tarea1_4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            string folderPath = "/storage/emulated/0/Android/data/com.grupo5.Tarea1_4/files/Pictures/MisFotos/";

            var filePathDir = Path.Combine(folderPath, "folder");
            if (!File.Exists(filePathDir))
            {
                Directory.CreateDirectory(filePathDir);
            }

            string[] files = Directory.GetFiles(folderPath, "*.jpg");

            List<IMG> imagenes = new List<IMG>();

            IMG temp = null;
            foreach (var item in files)
            {
                temp = new IMG();
                temp.titulo = item.Remove(0, 77);//obtener el nombre del archivo foto
                temp.desc = item;
                imagenes.Add(temp);
            }
            listaImagen.ItemsSource = imagenes;
        }
        private async void toolMenu1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agrega());
        }

        private async void listaImagen_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedimg = e.Item as IMG;
            var image = new Image
            {
                Source = ImageSource.FromFile(selectedimg.desc)
            };
            Viewer view = new Viewer();
            view.BindingContext = image.Source;
            view.Title = ""+selectedimg.titulo; //--PONER EL TITULO DEL TOOLBAR SEGUN EL NOMBRE DE LA FOTO
            await Navigation.PushAsync(view);
        }

        private async void toolMenu2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadoSQL());
        }
        private async void listaImagen_Refreshing(object sender, EventArgs e)
        {
            listaImagen.BackgroundColor = Color.Blue;
            listaImagen.Opacity = 0.2;
            string folderPath = "/storage/emulated/0/Android/data/com.grupo5.Tarea1_4/files/Pictures/MisFotos/";

            var filePathDir = Path.Combine(folderPath, "folder");
            if (!File.Exists(filePathDir))
            {
                Directory.CreateDirectory(filePathDir);
            }

            string[] files = Directory.GetFiles(folderPath, "*.jpg");

            List<IMG> imagenes = new List<IMG>();

            IMG temp = null;
            foreach (var item in files)
            {
                temp = new IMG();
                temp.titulo = item.Remove(0, 77);//nombre del archivo foto
                temp.desc = item;
                imagenes.Add(temp);
            }
            listaImagen.ItemsSource = imagenes;
            await Task.Delay(2000);
            listaImagen.IsRefreshing = false;
            listaImagen.BackgroundColor = Color.Transparent;
            listaImagen.Opacity = 1;
        }
    }
}
