using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Empresa.Frontend.Pages.Cargos
{
    public partial class CargosIndex
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        public List<Cargo>? Cargos { get; set; } = new List<Cargo>();

        protected  override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }
        private async Task LoadAsync()
        {
            var responseHppt = await Repository.GetAsync<List<Cargo>>("api/cargos");
            if (responseHppt.Error)
            {
                var message = await responseHppt.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Cargos = responseHppt.Response!;
        }
        private async Task DeleteAsync(Cargo cargo)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro que quieres borrar el cargo: {cargo.Nombre}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }
            var responseHTTP = await Repository.DeleteAsync($"api/cargos/{cargo.Id_Cargo}");
            if (responseHTTP.Error)
            {
                if (responseHTTP.Httpresponsem.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var mensajeError = await responseHTTP.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }
            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                
            ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }

    }
}
