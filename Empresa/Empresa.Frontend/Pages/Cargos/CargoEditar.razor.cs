using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;

namespace Empresa.Frontend.Pages.Cargos
{
    public partial class CargoEditar
    {
        private Cargo? cargo;
        private CargosForm? cargosForm;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int Id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var responseHTTP = await Repository.GetAsync<Cargo>($"api/cargos/{Id}");
            if (responseHTTP.Error)
            {
                if (responseHTTP.Httpresponsem.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("cargos");
                }
                else
                {
                    var messageError = await responseHTTP.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                
            }
            else
            {
                cargo = responseHTTP.Response;
            }
        }
        private async Task EditAsync()
        {
            var responseHTTP = await Repository.PutAsync("api/cargos", cargo);
            if (responseHTTP.Error)
            {
                var mensajeError = await responseHTTP.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }
        private void Return()
        {
            cargosForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("cargos");
        }

    }
}
