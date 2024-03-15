using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;
using System.Diagnostics.Metrics;



namespace Empresa.Frontend.Pages.Cargos
{
    public partial class CargoCreate
    {
        private CargosForm? cargosForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        private Cargo cargo = new();
        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/cargos", cargo);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }
        private void Return()
        {
            cargosForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/cargos");
        }


    }
}
